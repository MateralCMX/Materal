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
        ///// <summary>
        ///// 创建HttpClient
        ///// </summary>
        //private void CreateHttpClientFiles()
        //{
        //    if (SwaggerContent == null || SwaggerContent.Paths == null || SwaggerContent.Schemas == null) return;
        //    var paths = SwaggerContent.Paths.GroupBy(m => m.ControllerName).ToList();
        //    foreach (IGrouping<string?, PathModel> path in paths)
        //    {
        //        if (path.Key == null) continue;
        //        StringBuilder codeContent = new();
        //        codeContent.AppendLine($"using Materal.Model;");
        //        codeContent.AppendLine($"using Materal.HttpClient.Base;");
        //        codeContent.AppendLine($"using {ProjectName}.HttpClient.Models;");
        //        codeContent.AppendLine($"");
        //        codeContent.AppendLine($"namespace {ProjectName}.HttpClient");
        //        codeContent.AppendLine($"{{");
        //        codeContent.AppendLine($"    public class {path.Key}HttpClient : HttpClientBase");
        //        codeContent.AppendLine($"    {{");
        //        codeContent.AppendLine($"        /// <summary>");
        //        codeContent.AppendLine($"        /// 构造方法");
        //        codeContent.AppendLine($"        /// </summary>");
        //        codeContent.AppendLine($"        public {path.Key}HttpClient() : base(\"{ProjectName}\") {{ }}");
        //        int actionCount = 0;
        //        foreach (PathModel item in path)
        //        {
        //            if (string.IsNullOrWhiteSpace(item.ActionName) || item.ControllerName == item.ActionName) continue;
        //            if (item.Description != null)
        //            {
        //                codeContent.AppendLine($"        /// <summary>");
        //                codeContent.AppendLine($"        /// {item.Description}");
        //                codeContent.AppendLine($"        /// </summary>");
        //            }
        //            #region 解析对象
        //            string resultType;
        //            string baseFuncName = "GetResultModelBy";
        //            SchemaModel? targetSchema = SwaggerContent.Schemas.FirstOrDefault(m => m.Name == $"{SuffixName}{item.ResponseType}");
        //            if (!string.IsNullOrWhiteSpace(item.Response) && item.Response.StartsWith("PageResultModel"))
        //            {
        //                if(targetSchema != null)
        //                {
        //                    resultType = $"async Task<(List<{SuffixName}{item.ResponseType}> data, PageModel pageInfo)>";
        //                }
        //                else
        //                {
        //                    resultType = $"async Task<({item.InnerResponse} data, PageModel pageInfo)>";
        //                }
        //                baseFuncName = "GetPageResultModelBy";
        //            }
        //            else if (!string.IsNullOrWhiteSpace(item.InnerResponse))
        //            {
        //                if (targetSchema != null)
        //                {
        //                    resultType = $"async Task<List<{SuffixName}{item.ResponseType}>>";
        //                }
        //                else
        //                {
        //                    resultType = $"async Task<{item.InnerResponse}>";
        //                }
        //            }
        //            else
        //            {
        //                resultType = $"async Task";
        //            }
        //            #region 替换大写类型
        //            resultType = resultType.Replace("String", "string");
        //            #endregion
        //            string baseFunc = $"await {baseFuncName}{item.HttpMethod}Async";
        //            if (!string.IsNullOrWhiteSpace(item.InnerResponse))
        //            {
        //                if (targetSchema != null)
        //                {
        //                    baseFunc += $"<{SuffixName}{item.ResponseType}>";
        //                }
        //                else
        //                {
        //                    resultType = $"async Task<{item.InnerResponse}>";
        //                }
        //            }
        //            #endregion
        //            #region 解析传入参数
        //            List<string> paramCodes = new();
        //            List<string> baseFuncArgs = new()
        //            {
        //                $"\"{item.ControllerName}/{item.ActionName}\""
        //            };
        //            #region Body参数
        //            if (!string.IsNullOrWhiteSpace(item.BodyParam))
        //            {
        //                targetSchema = SwaggerContent.Schemas.FirstOrDefault(m => m.Name == $"{SuffixName}{item.BodyParam}");
        //                if (targetSchema != null)
        //                {
        //                    paramCodes.Add($"{SuffixName}{item.BodyParam} requestModel");
        //                }
        //                else
        //                {
        //                    paramCodes.Add($"{item.BodyParam} requestModel");
        //                }
        //                baseFuncArgs.Add("requestModel");
        //            }
        //            #endregion
        //            #region Query参数
        //            if (item.QueryParams != null)
        //            {
        //                List<string> queryArgs = new();
        //                foreach (QueryParamModel queryParam in item.QueryParams)
        //                {
        //                    paramCodes.Add($"{queryParam.CSharpType} {queryParam.Name}");
        //                    if (queryParam.CSharpType.EndsWith("?"))
        //                    {
        //                        if (queryParam.CSharpType == "string?")
        //                        {
        //                            queryArgs.Add($"[nameof({queryParam.Name})]={queryParam.Name} ?? string.Empty");
        //                        }
        //                        else
        //                        {
        //                            queryArgs.Add($"[nameof({queryParam.Name})]={queryParam.Name}?.ToString() ?? string.Empty");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (queryParam.CSharpType == "string")
        //                        {
        //                            queryArgs.Add($"[nameof({queryParam.Name})]={queryParam.Name}");
        //                        }
        //                        else
        //                        {
        //                            queryArgs.Add($"[nameof({queryParam.Name})]={queryParam.Name}.ToString()");
        //                        }
        //                    }
        //                }
        //                if(queryArgs.Count > 0)
        //                {
        //                    baseFuncArgs.Add("new Dictionary<string, string>{" + string.Join(", ", queryArgs) + "}");
        //                }
        //            }
        //            #endregion
        //            string paramsCode = string.Join(", ", paramCodes);
        //            string baseFuncCode = string.Join(", ", baseFuncArgs);
        //            #endregion
        //            codeContent.AppendLine($"        public {resultType} {item.ActionName}Async({paramsCode}) => {baseFunc}({baseFuncCode});");
        //            actionCount++;
        //        }
        //        codeContent.AppendLine($"    }}");
        //        codeContent.AppendLine($"}}");
        //        if (actionCount > 0)
        //        {
        //            WriteFile("", $"{path.Key}HttpClient.cs", codeContent.ToString());
        //        }
        //    }
        //}
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
