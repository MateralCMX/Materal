using Materal.EventBus.Abstraction;
using Materal.EventBus.RabbitMQ;
using Materal.MergeBlock.EventBusTest.Abstractions;
using Materal.Utils;

namespace Materal.MergeBlock.EventBusTest2.EventHandlers
{
    /// <summary>
    /// 我的事件处理器
    /// </summary>
    [QueueName("MergeBlockEventBusTest2Queue")]
    public class MyEventHandler : BaseEventHandler<MyEvent>, IEventHandler<MyEvent>
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public override async Task HandleAsync(MyEvent @event)
        {
            ConsoleQueue.WriteLine("------------------Test2-1------------------");
            ConsoleQueue.WriteLine(@event.Message);
            ConsoleQueue.WriteLine("------------------Test2-1------------------");
            await Task.CompletedTask;
        }
    }
}
