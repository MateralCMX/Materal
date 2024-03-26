using Materal.EventBus.Abstraction;
using Materal.EventBus.RabbitMQ;
using Materal.MergeBlock.EventBusTest.Abstractions;
using Materal.Utils;

namespace Materal.MergeBlock.EventBusTest1.EventHandlers
{
    /// <summary>
    /// 我的事件处理器
    /// </summary>
    [QueueName("MergeBlockEventBusTest1Queue")]
    public class MyEventHandler : BaseEventHandler<MyEvent>, IEventHandler<MyEvent>
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public override async Task HandleAsync(MyEvent @event)
        {
            ConsoleQueue.WriteLine("------------------Test1------------------");
            ConsoleQueue.WriteLine(@event.Message);
            ConsoleQueue.WriteLine("------------------Test1------------------");
            await Task.CompletedTask;
        }
    }
}
