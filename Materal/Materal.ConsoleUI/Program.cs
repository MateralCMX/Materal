using Materal.Common;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            ConsoleQueue.WriteLine("1234", ConsoleColor.DarkGreen);
            ConsoleQueue.WriteLine("2234");
            ConsoleQueue.WriteLine(new Exception("Test", new Exception("InnerTest")));
            try
            {
                int a = 0;
                int b = 1;
                int c = b / a;
            }
            catch (Exception ex)
            {
                ConsoleQueue.WriteLine(new Exception("Test", ex));
            }
            ConsoleQueue.WriteLine("3234");
            Console.ReadKey();
        }
    }
}
