using Materal.MergeBlock.Abstractions;

[assembly: MergeBlockAssembly]
namespace Materal.MergeBlock.EventBusTest2
{
    public class EventBusTest2Module : MergeBlockModule
    {
        public EventBusTest2Module() : base("EventBus测试模块2", "EventBusTest2", ["EventBusTest", "EventBus"])
        {
        }
    }
}
