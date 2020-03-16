using DotNetty.Transport.Channels;
using System;
using System.Threading.Tasks;

namespace Materal.DotNetty.EventBus
{
    public abstract class BaseEventHandler<T> : IEventHandler
    {
        public Type EventType => typeof(T);
        public abstract Task HandlerAsync(IEvent @event, IChannel channel);
    }
}
