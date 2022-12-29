using Materal.Common;
using Materal.ConvertHelper;
using Materal.HttpGenerator.Swagger.Models;
using Materal.NetworkHelper;
using Materal.StringHelper;
using Newtonsoft.Json.Linq;

namespace Materal.HttpGenerator.Swagger
{
    public class GeneratorBuildImpl : IGeneratorBuild
    {
        /// <summary>
        /// Swgger内容
        /// </summary>
        public SwaggerContentModel? SwaggerContent { get; private set; }
        public event Action<string>? OnMessage;
        public string OutputPath { get; set; }
        public string ProjectName { get; set; } = "Materal.HttpProject";
        public string? PrefixName { get; set; }
        public GeneratorBuildImpl()
        {
            OutputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HttpClientOutput");
        }
        public async Task SetSourceAsync(string soucre)
        {
            OnMessage?.Invoke("获取swagger....");
            string swaggerJson;
            if (soucre.IsUrl() && soucre.EndsWith(".json"))
            {
                swaggerJson = await HttpManager.SendGetAsync(soucre);
            }
            else
            {
                swaggerJson = soucre;
            }
            OnMessage?.Invoke("开始解析swagger....");
            JObject? jObj = swaggerJson.JsonToDeserializeObject<JObject>();
            if (jObj == null) return;
            SwaggerContent = new SwaggerContentModel(jObj);
            SwaggerContent.Init(PrefixName);
            OnMessage?.Invoke("swagger解析完毕");
        }
        public Task BuildAsync()
        {
            if (Directory.Exists(OutputPath))
            {
                OnMessage?.Invoke($"删除目录:{OutputPath}");
                Directory.Delete(OutputPath, true);
            }
            OnMessage?.Invoke("开始创建文件...");
            CreateCSharpProjectFile();
            CreateHttpClientBaseFile();
            SwaggerContent?.CreateModelFiles(this);
            SwaggerContent?.CreateHttpClientFiles(this);
            OnMessage?.Invoke("创建文件完毕");
            return Task.CompletedTask;
        }
        /// <summary>
        /// 创建HttpClientBase
        /// </summary>
        private void CreateHttpClientBaseFile()
        {
            string templeteContent = GetTempleteContent("HttpClientBase");
            SaveFile("Base", "HttpClientBase.cs", templeteContent);
        }
        /// <summary>
        /// 创建CSharpProject文件
        /// </summary>
        private void CreateCSharpProjectFile()
        {
            string templeteContent = GetTempleteContent("CSharpProject");
            SaveFile("", $"{ProjectName}.HttpClient.csproj", templeteContent);
        }
        /// <summary>
        /// 获得模版内容
        /// </summary>
        /// <param name="templeteName"></param>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        private string GetTempleteContent(string templeteName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templetes", $"{templeteName}.txt");
            if (!File.Exists(filePath)) throw new MateralException("模版文件丢失");
            string fileContent = File.ReadAllText(filePath);
            fileContent = fileContent.Replace("{{ProjectName}}", ProjectName);
            return fileContent;
        }
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public void SaveFile(string path, string fileName, string content)
        {
            string outputPath = OutputPath;
            if (!string.IsNullOrWhiteSpace(path))
            {
                outputPath = Path.Combine(outputPath, path);
            }
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            outputPath = Path.Combine(outputPath, fileName);
            OnMessage?.Invoke($"创建文件:{outputPath}");
            File.WriteAllText(outputPath, content);
        }
    }
}
