﻿using Materal.TFMS.EventBus;
using RC.Core.Common;
using RC.ServerCenter.Domain;
using RC.ServerCenter.Domain.Repositories;
using RC.ServerCenter.Services.Models.Namespace;
using XMJ.Authority.IntegrationEvents;

namespace RC.ServerCenter.ServiceImpl
{
    public partial class NamespaceServiceImpl
    {
        private readonly IEventBus _eventBus;
        private readonly IProjectRepository _projectRepository;
        public NamespaceServiceImpl(IEventBus eventBus, IProjectRepository projectRepository)
        {
            _eventBus = eventBus;
            _projectRepository = projectRepository;
        }
        public override async Task<Guid> AddAsync(AddNamespaceModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.Name == model.Name)) throw new RCException("名称重复");
            return await base.AddAsync(model);
        }
        public override async Task EditAsync(EditNamespaceModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.ID != model.ID && m.Name == model.Name)) throw new RCException("名称重复");
            await base.EditAsync(model);
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