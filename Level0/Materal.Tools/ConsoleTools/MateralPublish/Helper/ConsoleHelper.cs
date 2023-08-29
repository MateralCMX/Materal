using System.Threading.Tasks.Dataflow;

namespace Materal.Tools.Helper
{
    public static class ConsoleHelper
    {
        private readonly static ActionBlock<ConsoleMessageModel> _actionBlock = new(Hanlder);
        static ConsoleHelper()
        {
            AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
            {
                _actionBlock.Complete();
                _actionBlock.Completion.Wait();
            };
        }
        public static void WriteLine(string? message, ConsoleColor? consoleColor = null)
        {
            _actionBlock.Post(new ConsoleMessageModel
            {
                Message = message,
                Color = consoleColor ?? ConsoleColor.Gray
            });
        }
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
