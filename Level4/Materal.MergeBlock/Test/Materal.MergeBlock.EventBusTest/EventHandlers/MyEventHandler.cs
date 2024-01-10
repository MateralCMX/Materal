using Materal.MergeBlock.EventBusTest.Events;
using Materal.TFMS.EventBus;
using Materal.Utils;

namespace Materal.MergeBlock.EventBusTest.EventHandlers
{
    /// <summary>
    /// 我的事件处理器
    /// </summary>
    public class MyEventHandler : IIntegrationEventHandler<MyEvent>
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task HandleAsync(MyEvent @event)
        {
            ConsoleQueue.WriteLine("------------------------------------");
            ConsoleQueue.WriteLine(@event.Message);
            ConsoleQueue.WriteLine("------------------------------------");
            await Task.CompletedTask;
        }
    }
}
