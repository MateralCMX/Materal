using System.Threading.Tasks.Dataflow;

namespace Materal.Tools.Core.Helper
{
    /// <summary>
    /// 控制台队列
    /// </summary>
    public static class ConsoleQueue
    {
        private readonly static ActionBlock<ConsoleMessageModel> _actionBlock = new(Hanlder);
        static ConsoleQueue()
        {
            AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
            {
                _actionBlock.Complete();
                _actionBlock.Completion.Wait();
            };
        }
        /// <summary>
        /// 写入一行
        /// </summary>
        /// <param name="message"></param>
        /// <param name="consoleColor"></param>
        public static void WriteLine(string? message, ConsoleColor? consoleColor = null)
        {
            _actionBlock.Post(new ConsoleMessageModel
            {
                Message = message,
                Color = consoleColor ?? ConsoleColor.Gray
            });
        }
        /// <summary>
        /// 等待
        /// </summary>
        public static void Wait()
        {
            _actionBlock.Complete();
        }
        private static void Hanlder(ConsoleMessageModel model)
        {
            Console.ForegroundColor = model.Color;
            Console.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}:{model.Message}");
        }
        private class ConsoleMessageModel
        {
            public string? Message { get; set; }
            public ConsoleColor Color { get; set; }
        }
    }
}
