using AutoMapper;
using Deploy.Common;
using Deploy.Domain;
using Deploy.Domain.Repositories;
using Deploy.Enums;
using Deploy.ServiceImpl.Models;
using Materal.ConvertHelper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Deploy.ServiceImpl.Manage
{
    public class ApplicationManageImpl : IApplicationManage
    {
        private readonly ConcurrentDictionary<Guid, ApplicationRuntimeModel> _allApplicationInfos = new ConcurrentDictionary<Guid, ApplicationRuntimeModel>();
        private readonly ApplicationHandlerContext _applicationHandlerContext;
        public ApplicationManageImpl(IMapper mapper, IApplicationInfoRepository applicationInfoRepository)
        {
            _applicationHandlerContext = new DotNetCoreApplicationHandlerContext();
            var pm2ApplicationHandlerContext = new PM2ApplicationHandlerContext();
            _applicationHandlerContext.SetNext(pm2ApplicationHandlerContext);
            var exeApplicationHandlerContext = new ExeApplicationHandlerContext();
            pm2ApplicationHandlerContext.SetNext(exeApplicationHandlerContext);
            var staticDocumentApplicationHandlerContext = new StaticDocumentApplicationHandlerContext();
            exeApplicationHandlerContext.SetNext(staticDocumentApplicationHandlerContext);
            Task initTask = Task.Run(async () =>
            {
                List<ApplicationInfo> applicationInfos = await applicationInfoRepository.FindAsync(m => true);
                var applicationInfoModels = mapper.Map<List<ApplicationInfoModel>>(applicationInfos);
                foreach (ApplicationInfoModel applicationInfoModel in applicationInfoModels)
                {
                    applicationInfoModel.ApplicationStatus = ApplicationStatusEnum.Stop;
                    Add(applicationInfoModel);
                }
                foreach (KeyValuePair<Guid, ApplicationRuntimeModel> item in _allApplicationInfos)
                {
                    _applicationHandlerContext.KillProcess(item.Value);
                }
            });
            Task.WaitAll(initTask);
        }
        public void Add(ApplicationInfoModel model)
        {
            if (_allApplicationInfos.ContainsKey(model.ID)) throw new DeployException("应用程序已存在");
            model.ApplicationStatus = ApplicationStatusEnum.Stop;
            var runtimeModel = new ApplicationRuntimeModel(model, _applicationHandlerContext);
            if (!_allApplicationInfos.TryAdd(model.ID, runtimeModel)) throw new DeployException("添加失败");
        }

        public void Edit(ApplicationInfoModel model)
        {
            if (!_allApplicationInfos.ContainsKey(model.ID)) throw new DeployException("应用程序不存在");
            ApplicationRuntimeModel runtimeModel = _allApplicationInfos[model.ID];
            if (runtimeModel.ApplicationStatus != ApplicationStatusEnum.Stop) throw new DeployException("应用程序尚未停止");
            model.ApplicationStatus = ApplicationStatusEnum.Stop;
            model.CopyProperties(runtimeModel.ApplicationInfo);
        }

        public ApplicationInfoModel Delete(Guid id)
        {
            if (!_allApplicationInfos.ContainsKey(id)) throw new DeployException("应用程序不存在");
            ApplicationRuntimeModel runtimeModel = _allApplicationInfos[id];
            if (runtimeModel.ApplicationStatus != ApplicationStatusEnum.Stop) throw new DeployException("应用程序尚未停止");
            if (!_allApplicationInfos.TryRemove(id, out ApplicationRuntimeModel applicationRuntimeModel)) throw new DeployException("删除失败");
            return applicationRuntimeModel.ApplicationInfo;
        }

        public void Start(Guid id)
        {
            if (!CanRun()) throw new InvalidOperationException("正在发布中,请稍后");
            if (!_allApplicationInfos.ContainsKey(id)) throw new DeployException("应用程序不存在");
            ApplicationRuntimeModel runtimeModel = _allApplicationInfos[id];
            if (runtimeModel.ApplicationStatus != ApplicationStatusEnum.Stop) throw new DeployException("应用程序尚未停止");
            runtimeModel.Start();
        }

        public void Stop(Guid id)
        {
            if (!_allApplicationInfos.ContainsKey(id)) throw new DeployException("应用程序不存在");
            ApplicationRuntimeModel runtimeModel = _allApplicationInfos[id];
            if (runtimeModel.ApplicationStatus != ApplicationStatusEnum.Runing) throw new DeployException("应用程序尚运行");
            runtimeModel.Stop();
        }

        public ICollection<string> GetConsoleMessage(Guid id)
        {
            if (!_allApplicationInfos.ContainsKey(id)) throw new DeployException("应用程序不存在");
            ApplicationRuntimeModel runtimeModel = _allApplicationInfos[id];
            if (runtimeModel.ApplicationStatus != ApplicationStatusEnum.Runing) throw new DeployException("应用程序尚运行");
            ICollection<string> result = runtimeModel.ConsoleMessage;
            if(result.Count == 0) throw new DeployException("没有任何消息");
            return result;
        }

        public void StartAll()
        {
            if (!CanRun()) throw new InvalidOperationException("正在发布中,请稍后");
            foreach (KeyValuePair<Guid, ApplicationRuntimeModel> item in _allApplicationInfos)
            {
                try
                {
                    Start(item.Key);
                }
                catch (DeployException)
                {
                }
            }
        }

        public void StopAll()
        {
            foreach (KeyValuePair<Guid, ApplicationRuntimeModel> item in _allApplicationInfos)
            {
                try
                {
                    Stop(item.Key);
                }
                catch (DeployException)
                {
                }
            }
        }

        public List<ApplicationInfoModel> GetAllList()
        {
            return _allApplicationInfos.Select(m => m.Value.ApplicationInfo).OrderBy(m => m.MainModule).ToList();
        }

        public ApplicationRuntimeModel GetRuntimeModelByPath(string path)
        {
            return _allApplicationInfos.Select(m=>m.Value).FirstOrDefault(m => m.ApplicationInfo.Path.Equals(path, StringComparison.OrdinalIgnoreCase));
        }

        #region 私有方法
        /// <summary>
        /// 是否可以运行
        /// </summary>
        /// <returns></returns>
        private bool CanRun()
        {
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}UploadFiles//Temp";
            if (!Directory.Exists(path)) return true;
            var directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetDirectories().Length == 0;
        }
        #endregion
    }
}
