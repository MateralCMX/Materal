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
            Console.WriteLine("正在获取Swagger数据......");
            await generator.SetSourceAsync(path);
            //generator.ProjectName = "Educational";
            //await generator.SetSourceAsync("https://oaapi.xmjriyu.com/swagger/v1/swagger.json");
            Console.WriteLine("开始构建......");
            await generator.BuildAsync();
            Console.WriteLine("构建完毕,按任意键退出。");
            Console.Read();
        }
    }
}