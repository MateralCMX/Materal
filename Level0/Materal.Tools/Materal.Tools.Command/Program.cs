using System.CommandLine;

namespace Materal.Tools.Command
{
    public class Program
    {
        public static void Main()
        {
            RootCommand rootCommand = new("Materal工具箱");
            Console.WriteLine("Hello, World!");
        }
    }
}
