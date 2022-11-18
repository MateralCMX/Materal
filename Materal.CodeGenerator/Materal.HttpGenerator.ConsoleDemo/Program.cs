using Materal.HttpGenerator.Swagger;

namespace Materal.HttpGenerator.ConsoleDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IGeneratorBuild generator = new GeneratorBuildImpl();
            string jsonContentPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonContent.json");
            string jsonContent = await File.ReadAllTextAsync(jsonContentPath);
            //await generator.SetSourceAsync("http://175.27.254.187:8800/swagger/v1/swagger.json");
            await generator.SetSourceAsync(jsonContent);
            generator.ProjectName = "Test";
            await generator.BuildAsync();
        }
    }
}