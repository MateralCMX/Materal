namespace MMB.Demo.Appllication.EventHandlers
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
            Console.WriteLine(@event.Message);
            await Task.CompletedTask;
        }
    }
}
