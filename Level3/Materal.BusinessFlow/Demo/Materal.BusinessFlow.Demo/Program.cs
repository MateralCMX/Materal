using Materal.Abstractions;
using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.BusinessFlow.Abstractions.Services.Models.DataModel;
using Materal.BusinessFlow.Abstractions.Services.Models.DataModelField;
using Materal.BusinessFlow.Abstractions.Services.Models.FlowTemplate;
using Materal.BusinessFlow.Abstractions.Services.Models.Node;
using Materal.BusinessFlow.Abstractions.Services.Models.Step;
using Materal.BusinessFlow.Abstractions.Services.Models.User;
using Materal.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Materal.BusinessFlow.Demo
{
    public class Program
    {
        private static readonly IServiceProvider _serviceProvider;
        private static User _userCMX = new();
        private static User _userWRD = new();
        private static User _userFBW = new();
        private static readonly ILogger<Program>? _logger;
        private static Guid _flowTemplateID = Guid.NewGuid();
        private static readonly List<Guid> _stepIDs = new();
        static Program()
        {
            MateralConfig.PageStartNumber = 1;
            IServiceCollection services = new ServiceCollection();
            services.AddMateralLogger();
            services.AddBusinessFlow();
            IRepositoryHelper repositoryHelper = new SqliteRepositoryHelper();
            //IRepositoryHelper repositoryHelper = new SqlServerRepositoryHelper();
            repositoryHelper.AddRepository(services);
            _serviceProvider = services.BuildServiceProvider();
            LoggerManager.Init(option =>
            {
                option.AddConsoleTarget("LifeConsole", null, new Dictionary<LogLevel, ConsoleColor>
                {
                    [LogLevel.Error] = ConsoleColor.DarkRed
                });
                option.AddAllTargetRule(LogLevel.Information);
            });
            _logger = _serviceProvider.GetService<ILogger<Program>>();
            repositoryHelper.Init(_serviceProvider);
        }
        public static async Task Main()
        {
            try
            {
                await InitDataAsync();
                await BusinessFlowExcuteAsync();
            }
            catch (BusinessFlowException ex)
            {
                _logger?.LogError(ex, "业务流出错");
            }
            WriteTestInfo("测试程序执行完毕，按任意键退出...");
            Console.ReadKey();
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        private static async Task InitDataAsync()
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            IUserRepository userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            List<User> users = await userRepository.FindAsync(m => true);
            _userCMX = users.First(m => m.Name == "陈明旭");
            _userWRD = users.First(m => m.Name == "王任达");
            _userFBW = users.First(m => m.Name == "方宝文");
            IFlowTemplateRepository flowTemplateRepository = scope.ServiceProvider.GetRequiredService<IFlowTemplateRepository>();
            _flowTemplateID = (await flowTemplateRepository.FirstAsync(m => m.Name == "请假流程模版")).ID;
            IStepRepository stepRepository = scope.ServiceProvider.GetRequiredService<IStepRepository>();
            List<Step> steps = await stepRepository.FindAsync(m => m.FlowTemplateID == _flowTemplateID);
            Step step = steps.First(m => m.UpID == null);
            Guid? nextID = step.ID;
            do
            {
                step = steps.First(m => m.ID == nextID);
                _stepIDs.Add(step.ID);
                nextID = step.NextID;
            } while (nextID != null);
        }
        /// <summary>
        /// 业务流执行
        /// </summary>
        /// <returns></returns>
        private static async Task BusinessFlowExcuteAsync()
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider services = serviceScope.ServiceProvider;
            IBusinessFlowHost host = services.GetService<IBusinessFlowHost>() ?? throw new BusinessFlowException("获取服务失败");
            User initiatorUser = _userCMX;
            Guid flowID = Guid.Parse("161A38C6-8C02-4A28-9A52-D22CB10FE636");
            #region 执行自动流程
            WriteTestInfo("执行未完成的自动节点....");
            await host.RunAutoNodeAsync(_flowTemplateID);
            #endregion
            #region 启动一个流程
            {
                //WriteTestInfo("启动一个新流程....");
                flowID = await host.StartNewFlowAsync(_flowTemplateID, initiatorUser.ID);
            }
            #endregion
            #region 保存数据
            {
                //WriteTestInfo("保存节点数据....");
                //List<FlowRecordDTO> flowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, initiatorUser.ID);
                //if (flowRecords.Count <= 0) return;
                //string jsonData = "{\"Name\":\"测试数据\",\"Type\":\"病假\",\"StartDateTime\":\"2023-04-12 09:00:00\",\"EndDateTime\":\"2023-04-12 18:00:00\",\"Reason\":\"测试数据\"}";
                //await host.SaveFlowDataAsync(flowRecords[0].FlowTemplateID, flowRecords[0].ID, jsonData);
            }
            #endregion
            #region 完成节点
            {
                //List<FlowRecordDTO> flowRecords = await host.GetBacklogByUserIDAsync(initiatorUser.ID);
                //List<FlowTemplate> flowTemplates = await host.GetBacklogFlowTemplatesByUserIDAsync(initiatorUser.ID);
                string jsonData;
                List<FlowRecordDTO> cmxflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, initiatorUser.ID);
                List<FlowRecordDTO> wrdflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userWRD.ID);
                List<FlowRecordDTO> fbwflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userFBW.ID);
                FlowRecordDTO? flowRecord;
                flowRecord = cmxflowRecords.FirstOrDefault(m => m.FlowID == flowID && m.StepID == _stepIDs[0]);
                if (flowRecord != null)
                {
                    WriteTestInfo("----------------------------------");
                    WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
                    WriteTestInfo("回车执行流程:提交请假信息");
                    Console.ReadKey();
                    jsonData = $"{{\"Name\":\"{initiatorUser.Name}\",\"Type\":\"病假\",\"StartDateTime\":\"2023-04-12 09:00:00\",\"EndDateTime\":\"2023-04-12 18:00:00\",\"Reason\":\"生病了\"}}";
                    await host.ComplateFlowNodeAsync(flowRecord.FlowTemplateID, flowRecord.ID, initiatorUser.ID, jsonData);
                    cmxflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, initiatorUser.ID);
                    wrdflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userWRD.ID);
                    fbwflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userFBW.ID);
                }
                flowRecord = wrdflowRecords.FirstOrDefault(m => m.FlowID == flowID && m.StepID == _stepIDs[1]);
                if (flowRecord != null)
                {
                    WriteTestInfo("----------------------------------");
                    WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
                    WriteTestInfo("回车执行流程:打回请假");
                    Console.ReadKey();
                    jsonData = "{\"Result1\":\"不同意\"}";
                    await host.RepulseFlowNodeAsync(flowRecord.FlowTemplateID, flowRecord.ID, initiatorUser.ID, jsonData);//打回
                    cmxflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, initiatorUser.ID);
                    wrdflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userWRD.ID);
                    fbwflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userFBW.ID);
                }
                flowRecord = cmxflowRecords.FirstOrDefault(m => m.FlowID == flowID && m.StepID == _stepIDs[0]);
                if (flowRecord != null)
                {
                    WriteTestInfo("----------------------------------");
                    WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
                    WriteTestInfo("回车执行流程:重新提交请假信息");
                    Console.ReadKey();
                    jsonData = $"{{\"Name\":\"{initiatorUser.Name}\",\"Type\":\"病假\",\"Reason\":\"生病了,再次提交\"}}";
                    await host.ComplateFlowNodeAsync(flowRecord.FlowTemplateID, flowRecord.ID, initiatorUser.ID, jsonData);
                    cmxflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, initiatorUser.ID);
                    wrdflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userWRD.ID);
                    fbwflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userFBW.ID);
                }
                flowRecord = wrdflowRecords.FirstOrDefault(m => m.FlowID == flowID && m.StepID == _stepIDs[1]);
                if (flowRecord != null)
                {
                    WriteTestInfo("----------------------------------");
                    WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
                    WriteTestInfo("回车执行流程:同意请假");
                    Console.ReadKey();
                    jsonData = "{\"Result1\":\"同意\"}";
                    await host.ComplateFlowNodeAsync(flowRecord.FlowTemplateID, flowRecord.ID, initiatorUser.ID, jsonData);
                    cmxflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, initiatorUser.ID);
                    wrdflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userWRD.ID);
                    fbwflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userFBW.ID);
                }
                flowRecord = fbwflowRecords.FirstOrDefault(m => m.FlowID == flowID && m.StepID == _stepIDs[2]);
                if (flowRecord != null)
                {
                    WriteTestInfo("----------------------------------");
                    WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
                    WriteTestInfo("回车执行流程:上级同意请假");
                    Console.ReadKey();
                    jsonData = "{\"Result2\":\"同意\"}";
                    await host.ComplateFlowNodeAsync(flowRecord.FlowTemplateID, flowRecord.ID, initiatorUser.ID, jsonData);
                    cmxflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, initiatorUser.ID);
                    wrdflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userWRD.ID);
                    fbwflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userFBW.ID);
                }
                WriteTestInfo("----------------------------------");
                WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
            }
            #endregion
            #region 修改数据模型
            {
                //WriteTestInfo("新增字段....");
                //DataModelField fieldResult3 = new() { Name = "Result3", Description = "审批结果3", DataType = DataTypeEnum.Enum, DataModelID = dataModelID, Data = "[\"同意\",\"不同意\"]", ID = Guid.Parse("62BBEF3F-89FB-47ED-B875-04F74C852694") };
                //await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldResult3);
                //_testDataModelFields.Add(fieldResult3);
                ////修改字段
                //WriteTestInfo("修改字段....");
                //DataModelField fieldResult4 = new() { Name = "Result4", Description = "审批结果4", DataType = DataTypeEnum.Enum, DataModelID = dataModelID, Data = "[\"同意\",\"不同意\"]", ID = Guid.Parse("62BBEF3F-89FB-47ED-B875-04F74C852695") };
                //await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldResult4);
                //fieldResult4.Name = "Result5";
                //fieldResult4.Description = "审批结果5";
                //await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldResult4, true);
                ////删除字段
                //WriteTestInfo("删除字段....");
                //await services.GetRequiredService<IDataModelFieldService>().DeleteAsync(fieldResult4.ID);
            }
            #endregion
        }
        /// <summary>
        /// 写测试信息
        /// </summary>
        /// <param name="message"></param>
        private static void WriteTestInfo(string message)
        {
            //_logger?.LogInformation(message);
            Console.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}->{message}");
        }
    }
}