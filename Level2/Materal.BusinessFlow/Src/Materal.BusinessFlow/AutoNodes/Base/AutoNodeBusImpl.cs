using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.AutoNodes;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Enums;
using Materal.BusinessFlow.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Materal.BusinessFlow.AutoNodes.Base
{
    public class AutoNodeBusImpl : IAutoNodeBus
    {
#if DEBUG
        private const int clearTaskTimerInterval = 10;
#else
        private const int clearTaskTimerInterval = 60;
#endif
        private readonly IServiceProvider _serviceProvider;
        private readonly List<Task> _tasks = new();
        private readonly System.Timers.Timer _clearTaskTimer = new(clearTaskTimerInterval * 1000);
        private static readonly Dictionary<string, Type> _autoNodes = new();
        public AutoNodeBusImpl(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _clearTaskTimer.Elapsed += ClearTaskTimer_Elapsed;
            _clearTaskTimer.Start();
        }
        /// <summary>
        /// 清理任务定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearTaskTimer_Elapsed(object sender, EventArgs e)
        {
            _clearTaskTimer.Stop();
            for (int i = 0; i < _tasks.Count; i++)
            {
                if (_tasks[i].IsCompleted)
                {
                    _tasks.RemoveAt(i--);
                    continue;
                }
            }
            _clearTaskTimer.Start();
        }
        /// <summary>
        /// 执行自动节点
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <returns></returns>
        public void ExcuteAutoNode(Guid flowTemplateID, Guid flowRecordID)
        {
            Task task = Task.Factory.StartNew(async () =>
            {
                using IServiceScope serviceScope = _serviceProvider.CreateScope();
                IServiceProvider service = serviceScope.ServiceProvider;
                await ExcuteAutoNodeAsync(flowTemplateID, flowRecordID, service);
            });
            _tasks.Add(task);
        }
        /// <summary>
        /// 执行自动节点
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <returns></returns>
        private async Task ExcuteAutoNodeAsync(Guid flowTemplateID, Guid flowRecordID, IServiceProvider service)
        {
            IUnitOfWork? unitOfWork = service.GetService<IUnitOfWork>();
            if (unitOfWork == null) return;
            try
            {
                IFlowRecordRepository flowRecordRepository = unitOfWork.GetRepository<IFlowRecordRepository>();
                FlowRecord flowRecord = await flowRecordRepository.FirstAsync(flowTemplateID, flowRecordID);
                INodeRepository nodeRepository = unitOfWork.GetRepository<INodeRepository>();
                Node node = await nodeRepository.FirstAsync(flowRecord.NodeID);
                if (node.HandleType != NodeHandleTypeEnum.Auto) return;
                if (node.HandleData == null || string.IsNullOrWhiteSpace(node.HandleData)) return;
                if (!_autoNodes.ContainsKey(node.HandleData)) throw new BusinessFlowException($"未找到自动节点处理器{node.HandleData}");
                object? autoNodeObj = service.GetService(_autoNodes[node.HandleData]);
                if (autoNodeObj == null || autoNodeObj is not IAutoNode autoNode) throw new BusinessFlowException($"获取自动节点处理器{node.HandleData}失败");
                await autoNode.ExcuteAsync(flowTemplateID, flowRecordID);
                string jsonData = new { flowTemplateID, flowRecordID }.ToJson();
                await SuccessNodeAsync(service, flowTemplateID, flowRecordID, jsonData, unitOfWork);
            }
            catch (Exception ex)
            {
                string jsonData = new { flowTemplateID, flowRecordID }.ToJson();
                await ErrorNodeAsync(service, flowTemplateID, flowRecordID, jsonData, ex, unitOfWork);
                ILogger? logger = service.GetService<ILogger<AutoNodeBusImpl>>();
                logger?.LogError(ex, "自动节点运行出错");
            }
        }
        /// <summary>
        /// 节点完成
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <param name="jsonData"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        private async Task SuccessNodeAsync(IServiceProvider service, Guid flowTemplateID, Guid flowRecordID, string jsonData, IUnitOfWork unitOfWork)
        {
            BusinessFlowHelper businessFlowHelper = service.GetService<BusinessFlowHelper>() ?? throw new BusinessFlowException("获取服务失败");
            List<Guid> autoNodeIDs = await businessFlowHelper.ComplateAutoNodeAsync(flowTemplateID, flowRecordID, jsonData);
            await unitOfWork.CommitAsync();
            RunAutoNodes(flowTemplateID, autoNodeIDs);
        }
        /// <summary>
        /// 运行自动节点
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordIDs"></param>
        private void RunAutoNodes(Guid flowTemplateID, List<Guid> flowRecordIDs)
        {
            foreach (Guid flowRecordID in flowRecordIDs)
            {
                ExcuteAutoNode(flowTemplateID, flowRecordID);
            }
        }
        /// <summary>
        /// 节点发生错误
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task ErrorNodeAsync(IServiceProvider service, Guid flowTemplateID, Guid flowRecordID, string jsonData, Exception exception, IUnitOfWork unitOfWork)
        {
            BusinessFlowHelper businessFlowHelper = service.GetService<BusinessFlowHelper>() ?? throw new BusinessFlowException("获取服务失败");
            await businessFlowHelper.EditSameBatchFlowRecordAsync(flowTemplateID, flowRecordID, null, jsonData, FlowRecordStateEnum.Fail, exception.GetErrorMessage());
            await unitOfWork.CommitAsync();
        }
        /// <summary>
        /// 注册自动节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        public static void RegisterAutoNode<T>(IServiceCollection services) where T : IAutoNode => RegisterAutoNode(services, typeof(T));
        /// <summary>
        /// 注册自动节点
        /// </summary>
        /// <param name="services"></param>
        /// <param name="autoNodeType"></param>
        public static void RegisterAutoNode(IServiceCollection services, Type autoNodeType)
        {
            if (!autoNodeType.IsAssignableTo(typeof(IAutoNode)) && autoNodeType.IsAbstract && !autoNodeType.IsClass) return;
            if (!_autoNodes.ContainsKey(autoNodeType.Name))
            {
                _autoNodes.Add(autoNodeType.Name, autoNodeType);
            }
            else
            {
                _autoNodes[autoNodeType.Name] = autoNodeType;
            }
            services.TryAddScoped(autoNodeType);
        }
        /// <summary>
        /// 注册自动节点
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void RegisterAutoNodes(IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                RegisterAutoNodes(services, assembly);
            }
        }
        /// <summary>
        /// 注册自动节点
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblys"></param>
        public static void RegisterAutoNodes(IServiceCollection services, Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes().Where(m => m.IsAssignableTo(typeof(IAutoNode)) && !m.IsAbstract && m.IsClass))
            {
                RegisterAutoNode(services, type);
            }
        }
    }
}
