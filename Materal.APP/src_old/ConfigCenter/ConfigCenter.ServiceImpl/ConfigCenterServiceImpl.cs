using System;
using ConfigCenter.Common;
using ConfigCenter.DataTransmitModel.ConfigCenter;
using ConfigCenter.Services;
using ConfigCenter.Services.Models.ConfigCenter;
using Materal.APP.Core;
using Materal.ConvertHelper;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigCenter.Domain;
using ConfigCenter.Environment.PresentationModel.ConfigurationItem;
using ConfigCenter.Domain.Repositories;

namespace ConfigCenter.ServiceImpl
{
    public class ConfigCenterServiceImpl : IConfigCenterService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly INamespaceRepository _namespaceRepository;
        private readonly ConcurrentDictionary<string, RegisterEnvironmentModel> _registers = new ConcurrentDictionary<string, RegisterEnvironmentModel>();

        public ConfigCenterServiceImpl(IProjectRepository projectRepository, INamespaceRepository namespaceRepository)
        {
            _projectRepository = projectRepository;
            _namespaceRepository = namespaceRepository;
        }

        public void RegisterEnvironment(string key, RegisterEnvironmentModel model)
        {
            (bool canRegister, string errorMessage) = CanRegister(key, model);
            if (!canRegister) throw new ConfigCenterException(errorMessage);
            if (_registers.TryAdd(key, model))
            {
                ConfigCenterConsoleHelper.WriteLine($"新的环境[{model.Name}]注册:{model.Url}");
            }
            else
            {
                throw new ConfigCenterException("注册服务失败");
            }
        }

        public void UnRegisterEnvironment(string key)
        {
            if (!_registers.ContainsKey(key)) return;
            while (true)
            {
                if (!_registers.TryRemove(key, out RegisterEnvironmentModel register)) continue;
                if (register != null)
                {
                    ConfigCenterConsoleHelper.WriteLine($"环境[{register.Name}]已断开:{register.Url}");
                }
                break;
            }
        }

        public List<EnvironmentListDTO> GetEnvironmentList()
        {
            return _registers.Select(m => m.Value.CopyProperties<EnvironmentListDTO>()).ToList();
        }

        public async Task<List<AddConfigurationItemRequestModel>> GetSyncConfigurationItemModelAsync(SyncModel model)
        {
            Guid[] projectIDs = model.Projects.Select(m => m.ID).ToArray();
            List<Project> projects = await _projectRepository.FindAsync(m => projectIDs.Contains(m.ID));
            var namespaceIDs = new List<Guid>();
            foreach (SyncProjectModel project in model.Projects)
            {
                namespaceIDs.AddRange(project.Namespaces.Select(m => m.ID));
            }
            List<Namespace> namespaces = await _namespaceRepository.FindAsync(m => namespaceIDs.Contains(m.ID));
            var result = new List<AddConfigurationItemRequestModel>();
            foreach (SyncProjectModel modelProject in model.Projects)
            {
                Project tempProject = projects.FirstOrDefault(m => m.ID == modelProject.ID);
                if (tempProject == null) continue;
                foreach (SyncNamespaceModel modelNamespace in modelProject.Namespaces)
                {
                    Namespace tempNamespace = namespaces.FirstOrDefault(m => m.ID == modelNamespace.ID);
                    if (tempNamespace == null) continue;
                    foreach (SyncConfigurationItemModel modelConfigurationItem in modelNamespace.ConfigurationItems)
                    {
                        var temp = modelConfigurationItem.CopyProperties<AddConfigurationItemRequestModel>();
                        temp.ProjectID = tempProject.ID;
                        temp.ProjectName = tempProject.Name;
                        temp.NamespaceID = tempNamespace.ID;
                        temp.NamespaceName = tempNamespace.Name;
                        result.Add(temp);
                    }
                }
            }
            return result;
        }

        #region 私有方法
        /// <summary>
        /// 是否可以注册
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private (bool canRegister, string errorMessage) CanRegister(string key, RegisterEnvironmentModel model)
        {
            try
            {
                if (model.Key != ApplicationConfig.ServerInfo.Key) throw new ConfigCenterException("密钥错误");
                if (_registers.ContainsKey(key)) throw new ConfigCenterException("同一连接不可以重复注册");
                if (_registers.Any(m => m.Value.Name.Equals(model.Name, StringComparison.Ordinal)))
                {
                    throw new ConfigCenterException("该服务名称已被注册");
                }
                if (_registers.Any(m => m.Value.Url.Equals(model.Url, StringComparison.Ordinal)))
                {
                    throw new ConfigCenterException("该服务连接已被注册");
                }
                return (true, string.Empty);
            }
            catch (ConfigCenterException exception)
            {
                return (false, exception.Message);
            }
        }

        #endregion
    }
}
