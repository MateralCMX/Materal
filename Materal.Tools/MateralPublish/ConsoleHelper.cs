namespace MateralPublish
{
    public static class ConsoleHelper
    {
        public static void WriteLine(string? message)
        {
            Console.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}:{message}");
        }
    }
}
