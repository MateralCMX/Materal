using Materal.MergeBlock.Abstractions;

[assembly: MergeBlockAssembly]
namespace Materal.MergeBlock.EventBusTest1
{
    /// <summary>
    /// EventBus测试模块1
    /// </summary>
    public class EventBusTest1Module : MergeBlockModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public EventBusTest1Module() : base("EventBus测试模块1", "EventBusTest1", ["EventBusTest", "EventBus"])
        {
        }
    }
}
