using Materal.Logger.Message;

namespace Materal.Core.Logger.CommandHandlers
{
    public abstract class BaseEventHandler<T> : IEventHandler<T>
        where T : IEvent
    {
        public void Handler(IEvent @event)
        {
            if (@event is T tEvent)
            {
                Handler(tEvent);
            }
        }
        public abstract void Handler(T @event);
    }
}
