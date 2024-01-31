using Materal.MergeBlock.Abstractions;

[assembly: MergeBlockAssembly]
namespace Materal.MergeBlock.EventBusTest1
{
    public class EventBusTest1Module : MergeBlockModule
    {
        public EventBusTest1Module() : base("EventBus测试模块1", "EventBusTest1", ["EventBusTest", "EventBus"])
        {
        }
    }
}
