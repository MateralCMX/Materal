using Materal.TFMS.EventBus;
using RC.Core.Common;
using RC.ServerCenter.Domain;
using RC.ServerCenter.Services.Models.Namespace;
using XMJ.Authority.IntegrationEvents;

namespace RC.ServerCenter.ServiceImpl
{
    public partial class NamespaceServiceImpl
    {
        private readonly IEventBus _eventBus;
        public NamespaceServiceImpl(IServiceProvider serviceProvider, IEventBus eventBus) : this(serviceProvider)
        {
            _eventBus = eventBus;
        }
        public override async Task<Guid> AddAsync(AddNamespaceModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.ProjectID == model.ProjectID && m.Name == model.Name)) throw new RCException("名称重复");
            return await base.AddAsync(model);
        }
        protected override async Task DeleteAsync(Namespace domain)
        {
            await base.DeleteAsync(domain);
            await _eventBus.PublishAsync(new NamespaceDeleteEvent
            {
                NamespaceID = domain.ID
            });
        }
    }
}
