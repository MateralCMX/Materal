using AutoMapper;
using Deploy.Common;
using Deploy.DataTransmitModel.ApplicationInfo;
using Deploy.Domain;
using Deploy.Domain.Repositories;
using Deploy.Enums;
using Deploy.ServiceImpl.Models;
using Materal.ConvertHelper;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Deploy.ServiceImpl.Manage
{
    public class ApplicationManage
    {
        private static Logger _logger;
        private readonly IMapper _mapper;
        private static ConcurrentDictionary<Guid, ApplicationRuntimeModel> _allApplicationInfos = new ConcurrentDictionary<Guid, ApplicationRuntimeModel>();
        public ApplicationManage(IMapper mapper, IApplicationInfoRepository applicationInfoRepository)
        {
            _mapper = mapper;
            _logger ??= LogManager.GetCurrentClassLogger();
            if (_allApplicationInfos == null || _allApplicationInfos.Count == 0)
            {
                List<ApplicationInfo> applicationInfos = applicationInfoRepository.Find(m => true);
                foreach (ApplicationInfo applicationInfo in applicationInfos)
                {
                    Add(applicationInfo);
                }
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        public void Add(ApplicationInfo model)
        {
            if (_allApplicationInfos.ContainsKey(model.ID)) throw new DeployException("应用程序已存在");
            var runtimeModel = _mapper.Map<ApplicationRuntimeModel>(model);
            runtimeModel.Status = ApplicationStatusEnum.Stop;
            if (!_allApplicationInfos.TryAdd(model.ID, runtimeModel)) throw new DeployException("添加失败");
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        public void Edit(ApplicationInfo model)
        {
            if (!_allApplicationInfos.ContainsKey(model.ID)) throw new DeployException("应用程序不存在");
            ApplicationRuntimeModel runtimeModel = _allApplicationInfos[model.ID];
            if (runtimeModel.Status != ApplicationStatusEnum.Stop) throw new DeployException("应用程序尚未停止");
            model.CopyProperties(runtimeModel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            if (!_allApplicationInfos.ContainsKey(id)) throw new DeployException("应用程序不存在");
            ApplicationRuntimeModel runtimeModel = _allApplicationInfos[id];
            if (runtimeModel.Status != ApplicationStatusEnum.Stop) throw new DeployException("应用程序尚未停止");
            if (!_allApplicationInfos.TryRemove(id, out ApplicationRuntimeModel _)) throw new DeployException("删除失败");
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="id"></param>
        public void Start(Guid id)
        {
            if (!_allApplicationInfos.ContainsKey(id)) throw new DeployException("应用程序不存在");
            ApplicationRuntimeModel runtimeModel = _allApplicationInfos[id];
            if (runtimeModel.Status == ApplicationStatusEnum.Release) throw new DeployException("应用程序正在发布中");
            if (runtimeModel.Status != ApplicationStatusEnum.Stop) throw new DeployException("应用程序尚未停止");
            runtimeModel.Start();
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="id"></param>
        public void Stop(Guid id)
        {
            StopApplication(id);
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="id"></param>
        public static void StopApplication(Guid id)
        {
            if (!_allApplicationInfos.ContainsKey(id)) throw new DeployException("应用程序不存在");
            ApplicationRuntimeModel runtimeModel = _allApplicationInfos[id];
            if (runtimeModel.Status != ApplicationStatusEnum.Running) throw new DeployException("应用程序尚未运行");
            runtimeModel.Stop();
        }
        /// <summary>
        /// 获得控制台消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICollection<string> GetConsoleMessage(Guid id)
        {
            if (!_allApplicationInfos.ContainsKey(id)) throw new DeployException("应用程序不存在");
            ApplicationRuntimeModel runtimeModel = _allApplicationInfos[id];
            if (runtimeModel.Status != ApplicationStatusEnum.Running) throw new DeployException("应用程序尚运行");
            ICollection<string> result = runtimeModel.ConsoleMessage;
            if (result.Count == 0) throw new DeployException("没有任何消息");
            return result;
        }
        /// <summary>
        /// 开启所有
        /// </summary>
        public void StartAll()
        {
            List<Exception> exceptions = new List<Exception>();
            foreach (KeyValuePair<Guid, ApplicationRuntimeModel> item in _allApplicationInfos)
            {
                try
                {
                    Start(item.Key);
                }
                catch (DeployException exception)
                {
                    exceptions.Add(new DeployException($"[{item.Value.Name}]{exception.Message}"));
                }
            }
            if (exceptions.Count <= 0) return;
            string message = exceptions.Aggregate(string.Empty, (current, exception) => current + exception.Message);
            throw new DeployException($"部分应用程序启动失败\r\n{message}");
        }
        /// <summary>
        /// 停止所有
        /// </summary>
        public void StopAll()
        {
            StopAllApplication();
        }
        /// <summary>
        /// 停止所有
        /// </summary>
        public static void StopAllApplication()
        {
            _logger.Info("正在停止所有程序....");
            List<Exception> exceptions = new List<Exception>();
            foreach (KeyValuePair<Guid, ApplicationRuntimeModel> item in _allApplicationInfos)
            {
                try
                {
                    if (item.Value.Status == ApplicationStatusEnum.Running)
                    {
                        StopApplication(item.Key);
                    }
                }
                catch (DeployException exception)
                {
                    exceptions.Add(new DeployException($"[{item.Value.Name}]{exception.Message}"));
                }
            }

            if (exceptions.Count <= 0)
            {
                _logger.Info("已停止有所程序");
                return;
            }
            string message = exceptions.Aggregate(string.Empty, (current, exception) => current + exception.Message);
            message = $"部分应用程序关闭失败\r\n{message}";
            _logger.Warn(message);
            throw new DeployException(message);
        }
        /// <summary>
        /// 获得所有列表
        /// </summary>
        /// <returns></returns>
        public List<ApplicationInfoDTO> GetAllList()
        {
            return _allApplicationInfos.Select(m => _mapper.Map<ApplicationInfoDTO>(m.Value)).OrderBy(m => m.MainModule).ToList();
        }
        /// <summary>
        /// 根据路径获得运行模型
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ApplicationRuntimeModel GetRuntimeModelByPath(string path)
        {
            return _allApplicationInfos.Select(m => m.Value).FirstOrDefault(m => m.Path.Equals(path, StringComparison.OrdinalIgnoreCase));
        }
    }
}
