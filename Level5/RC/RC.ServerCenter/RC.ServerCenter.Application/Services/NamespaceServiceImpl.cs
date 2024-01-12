using Materal.TFMS.EventBus;
using RC.ServerCenter.Abstractions.Events;
using RC.ServerCenter.Abstractions.Services.Models.Namespace;

namespace RC.ServerCenter.Application.Services
{
    /// <summary>
    /// Namespace服务
    /// </summary>
    public partial class NamespaceServiceImpl(IEventBus eventBus)
    {
        /// <summary>
        /// 添加Namespace
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        public override async Task<Guid> AddAsync(AddNamespaceModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.ProjectID == model.ProjectID && m.Name == model.Name)) throw new RCException("名称重复");
            return await base.AddAsync(model);
        }
        /// <summary>
        /// 删除Namespace
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        protected override async Task DeleteAsync(Namespace domain)
        {
            await base.DeleteAsync(domain);
            await eventBus.PublishAsync(new NamespaceDeleteEvent
            {
                NamespaceID = domain.ID
            });
        }
    }
}
