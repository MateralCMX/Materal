using Materal.HttpGenerator.Swagger;

namespace Materal.HttpGenerator.ConsoleDemo
{
    public class Program
    {
        public static async Task Main()
        {
            IGeneratorBuild generator = new GeneratorBuildImpl();
            Console.WriteLine("请输入SwaggerJson地址");
            string? path = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(path)) return;
            Console.WriteLine("请输入项目名称");
            generator.ProjectName = Console.ReadLine() ?? "Test";
            //generator.ProjectName = "Educational";
            //await generator.SetSourceAsync("https://oaapi.xmjriyu.com/swagger/v1/swagger.json");
            await generator.BuildAsync();
        }
    }
}