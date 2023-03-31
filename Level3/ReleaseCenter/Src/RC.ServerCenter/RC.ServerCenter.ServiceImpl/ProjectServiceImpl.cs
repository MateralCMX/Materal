using Materal.TFMS.EventBus;
using RC.Core.Common;
using RC.ServerCenter.Domain;
using RC.ServerCenter.Domain.Repositories;
using RC.ServerCenter.Services.Models.Project;
using XMJ.Authority.IntegrationEvents;

namespace RC.ServerCenter.ServiceImpl
{
    public partial class ProjectServiceImpl
    {
        private readonly IEventBus _eventBus;
        private readonly INamespaceRepository _namespaceRepository;
        public ProjectServiceImpl(IServiceProvider serviceProvider, IEventBus eventBus) : this(serviceProvider)
        {
            _eventBus = eventBus;
            _namespaceRepository = UnitOfWork.GetRepository<INamespaceRepository>();
        }
        public override async Task<Guid> AddAsync(AddProjectModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.Name == model.Name)) throw new RCException("名称重复");
            return await base.AddAsync(model);
        }
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
        protected override async Task DeleteAsync(Project domain)
        {
            List<Namespace> namespaces = await _namespaceRepository.FindAsync(m => m.ProjectID == domain.ID);
            foreach (Namespace @namespace in namespaces)
            {
                UnitOfWork.RegisterDelete(@namespace);
            }
            await base.DeleteAsync(domain);
            await _eventBus.PublishAsync(new ProjectDeleteEvent
            {
                ProjectID = domain.ID
            });
        }
    }
}
