using Materal.Logger.ConsoleTest.Tests;
using Materal.Utils;

namespace Materal.Logger.ConsoleTest
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Type? type;
            while (true)
            {
                ConsoleQueue.WriteLine("ConfigFile Console File Http WebSocket Mongo Sqlite SqlServer");
                ConsoleQueue.WriteLine("请输入测试类型：");
                string? inputArg = Console.ReadLine();
                if (inputArg == null || string.IsNullOrEmpty(inputArg))
                {
                    inputArg = "ConfigFile";
                }
                inputArg += "Test";
                type = inputArg.GetTypeByTypeName<ITest>();
                if (type is not null && type.IsAssignableTo<ITest>()) break;
            }
            ITest test = type.Instantiation<ITest>();
            await test.TestAsync(args);
        }
    }
}
