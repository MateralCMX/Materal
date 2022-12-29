using Materal.Common;
using Materal.HttpGenerator.Swagger;

namespace Materal.HttpGenerator.ConsoleDemo
{
    public class Program
    {
        public static async Task Main()
        {
            IGeneratorBuild generator = new GeneratorBuildImpl();
            generator.OnMessage += Generator_OnMessage;
#if DEBUG
            generator.ProjectName = "XMJ.Authority";
            //generator.PrefixName = "Authority";
            await generator.SetSourceAsync("http://175.27.254.187:18800/swagger/v1/swagger.json");
#else
            ConsoleQueue.WriteLine("请输入SwaggerJson地址");
            string? path = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(path)) return;
            ConsoleQueue.WriteLine("请输入项目名称");
            generator.ProjectName = Console.ReadLine() ?? "Materal.HttpClient";
            ConsoleQueue.WriteLine("请输入特征前缀");
            generator.PrefixName = Console.ReadLine();
            generator.OutputPath = Path.Combine(Environment.CurrentDirectory, "HttpClientOutput", $"{generator.ProjectName}.HttpClient");
            await generator.SetSourceAsync(path);
#endif
            await generator.BuildAsync();
            ConsoleQueue.WriteLine("按任意键退出");
            Console.ReadKey();
        }
        private static void Generator_OnMessage(string message) => ConsoleQueue.WriteLine(message);
    }
}