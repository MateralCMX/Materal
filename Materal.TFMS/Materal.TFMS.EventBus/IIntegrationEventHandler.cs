using System.Threading.Tasks;

namespace Materal.TFMS.EventBus
{
    public interface IIntegrationEventHandler
    {
    }
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task HandleAsync(TIntegrationEvent @event);
    }
}
