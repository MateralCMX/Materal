using Materal.DotNetty.EventBus;

namespace Demo.Common
{
    public abstract class DemoEvent: BaseEvent
    {
        protected DemoEvent()
        {
            Event = GetType().Name;
        }
    }
}
