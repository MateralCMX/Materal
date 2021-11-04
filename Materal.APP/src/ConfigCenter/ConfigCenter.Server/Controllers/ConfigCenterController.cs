using ConfigCenter.DataTransmitModel.Namespace;
using ConfigCenter.DataTransmitModel.Project;
using ConfigCenter.Environment.DataTransmitModel.ConfigurationItem;
using ConfigCenter.Environment.PresentationModel.ConfigurationItem;
using ConfigCenter.IntegrationEvents;
using ConfigCenter.PresentationModel.ConfigCenter;
using ConfigCenter.Services;
using ConfigCenter.Services.Models.Namespace;
using ConfigCenter.Services.Models.Project;
using Materal.APP.Core;
using Materal.APP.NetworkCore;
using Materal.APP.WebAPICore;
using Materal.APP.WebAPICore.Controllers;
using Materal.APP.WebAPICore.Models;
using Materal.Model;
using Materal.TFMS.EventBus;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigCenter.DataTransmitModel.ConfigCenter;

namespace ConfigCenter.Server.Controllers
{
    /// <summary>
    /// 配置中心控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class ConfigCenterController : WebAPIControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly INamespaceService _namespaceService;
        private readonly IEventBus _eventBus;
        /// <summary>
        /// 配置中心控制器
        /// </summary>
        public ConfigCenterController(IProjectService projectService, INamespaceService namespaceService, IEventBus eventBus)
        {
            _projectService = projectService;
            _namespaceService = namespaceService;
            _eventBus = eventBus;
        }
        /// <summary>
        /// 获得环境列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<List<EnvironmentDTO>>> GetEnvironmentListAsync()
        {
            string tagText = ServiceType.ConfigCenterEnvironment.ToString();
            List<ConsulServiceModel> consulServices = await ConsulManage.GetServicesAsync(m => m.Tags.Contains(tagText));
            List<EnvironmentDTO> result = consulServices.Select(m=>new EnvironmentDTO
            {
                Name = m.Tags.LastOrDefault(),
                Key = m.Service
            }).ToList();
            return ResultModel<List<EnvironmentDTO>>.Success(result, "获取成功");
        }
        /// <summary>
        /// 同步配置
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> SyncConfigAsync(SyncConfigRequestModel requestModel)
        {
            List<ProjectListDTO> allProject = await _projectService.GetProjectListAsync(new QueryProjectFilterModel());
            List<NamespaceListDTO> allNamespace = await _namespaceService.GetNamespaceListAsync(new QueryNamespaceFilterModel());
            ConsulServiceModel serviceModel = await ConsulManage.GetServiceAsync(m => m.Service == requestModel.SourceAPI);
            List<ConfigurationItemListDTO> allConfigurationItems = await GetAllConfigurationItemsAsync(serviceModel);
            return await SyncConfigAsync(requestModel, allProject, allNamespace, allConfigurationItems);
        }
        /// <summary>
        /// 同步所有配置
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> SyncAllConfigAsync(SyncAllConfigRequestModel requestModel)
        {
            SyncConfigRequestModel syncModel = new SyncConfigRequestModel
            {
                SourceAPI = requestModel.SourceAPI,
                TargetsAPI = requestModel.TargetsAPI,
                Projects = new List<SyncProjectRequestModel>()
            };
            List<ProjectListDTO> allProject = await _projectService.GetProjectListAsync(new QueryProjectFilterModel());
            List<NamespaceListDTO> allNamespace = await _namespaceService.GetNamespaceListAsync(new QueryNamespaceFilterModel());
            ConsulServiceModel serviceModel = await ConsulManage.GetServiceAsync(m => m.Service == requestModel.SourceAPI);
            List<ConfigurationItemListDTO> allConfigurationItems = await GetAllConfigurationItemsAsync(serviceModel);
            foreach (ProjectListDTO project in allProject)
            {
                SyncProjectRequestModel syncProject = new SyncProjectRequestModel
                {
                    ProjectID = project.ID,
                    Namespaces = new List<SyncNamespaceRequestModel>()
                };
                List<NamespaceListDTO> targetNamespaces = allNamespace.Where(m => m.ProjectID == project.ID).ToList();
                foreach (NamespaceListDTO targetNamespace in targetNamespaces)
                {
                    SyncNamespaceRequestModel syncNamespace = new SyncNamespaceRequestModel
                    {
                        NamespacesID = targetNamespace.ID,
                        Keys = new List<string>()
                    };
                    List<ConfigurationItemListDTO> items = allConfigurationItems.Where(m => m.ProjectName == project.Name && m.NamespaceName == targetNamespace.Name).ToList();
                    foreach (ConfigurationItemListDTO item in items)
                    {
                        syncNamespace.Keys.Add(item.Key);
                    }
                    if (syncNamespace.Keys.Count > 0)
                    {
                        syncProject.Namespaces.Add(syncNamespace);
                    }
                }
                if (syncProject.Namespaces.Count > 0)
                {
                    syncModel.Projects.Add(syncProject);
                }
            }
            if (syncModel.Projects.Count > 0)
            {
                return await SyncConfigAsync(syncModel, allProject, allNamespace, allConfigurationItems);
            }
            return ResultModel.Success("同步完成");
        }
        #region 私有方法
        /// <summary>
        /// 同步配置
        /// </summary>
        /// <returns></returns>
        private async Task<ResultModel> SyncConfigAsync(SyncConfigRequestModel requestModel, List<ProjectListDTO> allProject, List<NamespaceListDTO> allNamespace, List<ConfigurationItemListDTO> allConfigurationItems)
        {
            SyncConfigurationItemEvent @event = new SyncConfigurationItemEvent
            {
                TargetsAPI = requestModel.TargetsAPI,
                ConfigurationItem = SyncConfigModelToAddConfigurationItemModels(requestModel.Projects, allProject, allNamespace, allConfigurationItems)
            };
            await _eventBus.PublishAsync(@event);
            return ResultModel.Success("同步任务以启动,请稍后查看同步结果");
        }
        /// <summary>
        /// 获得配置项
        /// </summary>
        /// <param name="serviceModel"></param>
        /// <returns></returns>
        private async Task<List<ConfigurationItemListDTO>> GetAllConfigurationItemsAsync(ConsulServiceModel serviceModel)
        {
            string url = $"{serviceModel.Service}/ConfigurationItem/GetConfigurationItemList";
            QueryConfigurationItemFilterRequestModel filterModel = new QueryConfigurationItemFilterRequestModel
            {
                Description=null,
                Key = null,
                NamespaceNames = null,
                ProjectName = null
            };
            List<ConfigurationItemListDTO> result =  await HttpHelper.SendPostAsync<List<ConfigurationItemListDTO>>(url, filterModel);
            return result;
        }
        /// <summary>
        /// 同步模型转换为配置项模型
        /// </summary>
        /// <param name="projects"></param>
        /// <param name="allNamespace"></param>
        /// <param name="allConfigurationItems"></param>
        /// <param name="allProject"></param>
        /// <returns></returns>
        private List<AddConfigurationItemRequestModel> SyncConfigModelToAddConfigurationItemModels(IEnumerable<SyncProjectRequestModel> projects, List<ProjectListDTO> allProject, List<NamespaceListDTO> allNamespace, List<ConfigurationItemListDTO> allConfigurationItems)
        {
            var result = new List<AddConfigurationItemRequestModel>();
            foreach (SyncProjectRequestModel projectModel in projects)
            {
                ProjectListDTO targetProject = allProject.FirstOrDefault(m => m.ID == projectModel.ProjectID);
                if (targetProject == null) continue;
                foreach (SyncNamespaceRequestModel namespaceModel in projectModel.Namespaces)
                {
                    NamespaceListDTO targetNamespace = allNamespace.FirstOrDefault(m => m.ID == namespaceModel.NamespacesID);
                    if (targetNamespace == null) continue;
                    foreach (string key in namespaceModel.Keys)
                    {
                        ConfigurationItemListDTO targetItem = allConfigurationItems.FirstOrDefault(m =>
                            m.Key == key && 
                            m.NamespaceName == targetNamespace.Name &&
                            m.ProjectName == targetProject.Name);
                        if (targetItem == null) continue;
                        AddConfigurationItemRequestModel temp = new AddConfigurationItemRequestModel
                        {
                            Description = targetItem.Description,
                            Key = key,
                            NamespaceName = targetItem.NamespaceName,
                            ProjectName = targetItem.ProjectName,
                            Value = targetItem.Value
                        };
                        result.Add(temp);
                    }
                }
            }
            return result;
        }
        #endregion
    }
}
