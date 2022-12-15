using Materal.Logger.Message;

namespace Materal.Core.Logger.CommandHandlers
{
    public interface IEventHandler
    {
        public void Handler(IEvent @event);
    }
    public interface IEventHandler<T> : IEventHandler
        where T : IEvent
    {
        public void Handler(T @event);
    }
}
