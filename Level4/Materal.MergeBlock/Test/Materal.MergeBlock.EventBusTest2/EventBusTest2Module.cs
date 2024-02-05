using Materal.MergeBlock.Abstractions;

[assembly: MergeBlockAssembly]
namespace Materal.MergeBlock.EventBusTest2
{
    /// <summary>
    /// EventBus测试模块2
    /// </summary>
    public class EventBusTest2Module : MergeBlockModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public EventBusTest2Module() : base("EventBus测试模块2", "EventBusTest2", ["EventBusTest", "EventBus"])
        {
        }
    }
}
