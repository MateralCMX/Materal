using Materal.MergeBlock.Abstractions;

namespace Materal.MergeBlock.EventBusTest
{
    public class EventBusTestModule : MergeBlockModule
    {
        public EventBusTestModule() : base("EventBus测试模块", "EventBusTest", ["EventBus"])
        {
        }
    }
}
