using Materal.TFMS.EventBus;
using RC.ServerCenter.Abstractions.Events;
using RC.ServerCenter.Abstractions.Services.Models.Project;

namespace RC.ServerCenter.Application.Services
{
    /// <summary>
    /// Project服务
    /// </summary>
    /// <param name="eventBus"></param>
    /// <param name="namespaceRepository"></param>
    public partial class ProjectServiceImpl(IEventBus eventBus, INamespaceRepository namespaceRepository)
    {
        /// <summary>
        /// 添加Project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        public override async Task<Guid> AddAsync(AddProjectModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.Name == model.Name)) throw new RCException("名称重复");
            return await base.AddAsync(model);
        }
        /// <summary>
        /// 添加Project
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override async Task<Guid> AddAsync(Project domain, AddProjectModel model)
        {
            UnitOfWork.RegisterAdd(new Namespace
            {
                Description = "默认命名空间",
                Name = "Application",
                ProjectID = domain.ID
            });
            return await base.AddAsync(domain, model);
        }
        /// <summary>
        /// 删除Project
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        protected override async Task DeleteAsync(Project domain)
        {
            List<Namespace> namespaces = await namespaceRepository.FindAsync(m => m.ProjectID == domain.ID);
            foreach (Namespace @namespace in namespaces)
            {
                UnitOfWork.RegisterDelete(@namespace);
            }
            await base.DeleteAsync(domain);
            await eventBus.PublishAsync(new ProjectDeleteEvent
            {
                ProjectID = domain.ID
            });
        }
    }
}
