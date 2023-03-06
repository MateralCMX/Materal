using Materal.BaseCore.Common.Utils;
using Materal.TFMS.EventBus;
using Materal.Utils.Model;
using RC.Core.Common;
using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.Domain;
using RC.EnvironmentServer.Services.Models.ConfigurationItem;
using RC.ServerCenter.DataTransmitModel.Namespace;
using RC.ServerCenter.DataTransmitModel.Project;
using RC.ServerCenter.HttpClient;
using System.Data.SqlClient;
using System.Linq.Expressions;
using XMJ.Authority.IntegrationEvents;

namespace RC.EnvironmentServer.ServiceImpl
{
    public partial class ConfigurationItemServiceImpl
    {
        private readonly ProjectHttpClient _projectHttpClient;
        private readonly NamespaceHttpClient _namespaceHttpClient;
        private readonly IEventBus _eventBus;
        public ConfigurationItemServiceImpl(ProjectHttpClient projectHttpClient, NamespaceHttpClient namespaceHttpClient, IEventBus eventBus)
        {
            _projectHttpClient = projectHttpClient;
            _namespaceHttpClient = namespaceHttpClient;
            _eventBus = eventBus;
        }
        public override async Task<Guid> AddAsync(AddConfigurationItemModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.NamespaceID == model.NamespaceID && m.ProjectID == model.ProjectID && m.Key == model.Key)) throw new RCException("键重复");
            return await base.AddAsync(model);
        }
        protected override async Task<Guid> AddAsync(ConfigurationItem domain, AddConfigurationItemModel model)
        {
            ProjectListDTO? project = await _projectHttpClient.FirstDataAsync(model.ProjectID);
            if (project == null) throw new RCException("项目不存在");
            NamespaceListDTO? @namespace = await _namespaceHttpClient.FirstDataAsync(model.NamespaceID);
            if (@namespace == null) throw new RCException("命名空间不存在");
            domain.NamespaceName = @namespace.Name;
            domain.ProjectName = project.Name;
            return await base.AddAsync(domain, model);
        }
        protected override async Task EditAsync(ConfigurationItem domainFromDB, EditConfigurationItemModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.ID != model.ID && m.NamespaceID == domainFromDB.NamespaceID && m.ProjectID == domainFromDB.ProjectID && m.Key == model.Key)) throw new RCException("键重复");
            await base.EditAsync(domainFromDB, model);
        }
        protected override async Task<(List<ConfigurationItemListDTO> data, PageModel pageInfo)> GetListAsync(Expression<Func<ConfigurationItem, bool>> expression, QueryConfigurationItemModel model, Expression<Func<ConfigurationItem, object>>? orderExpression = null, SortOrder sortOrder = SortOrder.Descending)
        {
            sortOrder = SortOrder.Ascending;
            orderExpression = m => m.Key;
            return await base.GetListAsync(expression, model, orderExpression, sortOrder);
        }
        public async Task InitAsync()
        {
            List<ConfigurationItem> configurationItems = await DefaultRepository.GetAllInfoFromCacheAsync();
            List<ConfigurationItem> removeItems = new();
            Guid[] hastIDs;
            Guid[] removeIDs;
            #region 移除项目不存在的
            Guid[] allProjectIDs = configurationItems.Select(m => m.ProjectID).Distinct().ToArray();
            (List<ProjectListDTO> ? allProjectInfo, _) = await _projectHttpClient.GetListAsync(new()
            {
                PageIndex = 1,
                PageSize = allProjectIDs.Length,
                IDs = allProjectIDs.ToList()
            });
            if (allProjectInfo != null)
            {
                hastIDs = allProjectInfo.Select(m => m.ID).ToArray();
                removeIDs = allProjectIDs.Except(hastIDs).ToArray();
                if (removeIDs.Length > 0)
                {
                    removeItems.AddRange(configurationItems.Where(m => removeIDs.Contains(m.ProjectID)).ToList());
                }
            }
            #endregion
            #region 移除命名空间不存在的
            Guid[] allNamespaceIDs = configurationItems.Select(m => m.NamespaceID).Distinct().ToArray();
            (List<NamespaceListDTO>? allNamespaceInfo, _) = await _namespaceHttpClient.GetListAsync(new()
            {
                PageIndex = 1,
                PageSize = allNamespaceIDs.Length,
                IDs = allNamespaceIDs.ToList()
            });
            if (allNamespaceInfo != null)
            {
                hastIDs = allNamespaceInfo.Select(m => m.ID).ToArray();
                removeIDs = allProjectIDs.Except(hastIDs).ToArray();
                if (removeIDs.Length > 0)
                {
                    removeItems.AddRange(configurationItems.Where(m => removeIDs.Contains(m.NamespaceID)).ToList());
                }
            }
            #endregion
            removeItems = removeItems.Distinct().ToList();
            foreach (ConfigurationItem item in removeItems)
            {
                UnitOfWork.RegisterDelete(item);
            }
            await UnitOfWork.CommitAsync();
            await ClearCacheAsync();
        }
        /// <summary>
        /// 同步配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SyncConfigAsync(SyncConfigModel model)
        {
            if (model.TargetEnvironments.Length <= 0) throw new RCException("同步目标为0");
            List<ConfigurationItem> configurationItems = await DefaultRepository.FindAsync(model);
            SyncConfigEvent @event = new()
            {
                Mode = model.Mode,
                TargetEnvironments = model.TargetEnvironments,
                ConfigurationItems = Mapper.Map<List<ConfigurationItemListDTO>>(configurationItems)
            };
            await _eventBus.PublishAsync(@event);
        }
    }
}
