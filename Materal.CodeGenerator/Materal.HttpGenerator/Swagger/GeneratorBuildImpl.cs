using Materal.Common;
using Materal.ConvertHelper;
using Materal.HttpGenerator.Swagger.Models;
using Materal.NetworkHelper;
using Materal.StringHelper;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Materal.HttpGenerator.Swagger
{
    public class GeneratorBuildImpl : IGeneratorBuild
    {
        /// <summary>
        /// Swgger内容
        /// </summary>
        private SwaggerContentModel? _swaggerContent;
        public event Action<string>? OnMessage;
        public string OutputPath { get; set; }
        public string ProjectName { get; set; } = "Materal.HttpProject";
        private string? _prefixName;
        public string PrefixName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_prefixName)) return _prefixName;
                int dotIndex = ProjectName.IndexOf(".");
                if(dotIndex > 0)
                {
                    _prefixName = ProjectName[0..dotIndex];
                }
                else
                {
                    _prefixName = ProjectName;
                }
                return _prefixName;
            }
            set
            {
                _prefixName = value;
            }
        }
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
            _swaggerContent = new SwaggerContentModel(jObj);
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
            CreateModelFiles();
            CreateHttpClientFiles();
            OnMessage?.Invoke("创建文件完毕");
            return Task.CompletedTask;
        }
        /// <summary>
        /// 创建HttpClient
        /// </summary>
        private void CreateHttpClientFiles()
        {
            if (_swaggerContent == null || _swaggerContent.Paths == null) return;
            var paths = _swaggerContent.Paths.GroupBy(m => m.ControllerName).ToList();
            foreach (IGrouping<string?, PathModel> path in paths)
            {
                if (path.Key == null) continue;
                StringBuilder codeContent = new();
                codeContent.AppendLine($"using Materal.Model;");
                codeContent.AppendLine($"using Materal.HttpClient.Base;");
                codeContent.AppendLine($"using {PrefixName}.HttpClient.Models;");
                codeContent.AppendLine($"");
                codeContent.AppendLine($"namespace {PrefixName}.HttpClient");
                codeContent.AppendLine($"{{");
                codeContent.AppendLine($"    public class {path.Key}HttpClient : HttpClientBase");
                codeContent.AppendLine($"    {{");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 构造方法");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        public {path.Key}HttpClient() : base(\"{ProjectName}\") {{ }}");
                int actionCount = 0;
                foreach (PathModel item in path)
                {
                    if (string.IsNullOrWhiteSpace(item.Response) || string.IsNullOrWhiteSpace(item.ActionName) || item.ControllerName == item.ActionName) continue;
                    if (item.Description != null)
                    {
                        codeContent.AppendLine($"        /// <summary>");
                        codeContent.AppendLine($"        /// {item.Description}");
                        codeContent.AppendLine($"        /// </summary>");
                    }
                    #region 解析对象
                    string resultType;
                    string baseFuncType;
                    string baseFuncName = "GetResultModelBy";
                    if (item.Response.EndsWith("PageResultModel"))
                    {
                        baseFuncType = item.Response[0..^"PageResultModel".Length];
                        resultType = $"async Task<(List<{baseFuncType}> data, PageModel pageInfo)>";
                        baseFuncName = "GetPageResultModelBy";
                    }
                    else if (item.Response.EndsWith("ListResultModel"))
                    {
                        baseFuncType = $"List<{item.Response[0..^"ListResultModel".Length]}>";
                        resultType = $"async Task<{baseFuncType}>";
                    }
                    else if (item.Response == "ResultModel")
                    {
                        baseFuncType = string.Empty;
                        resultType = $"async Task";
                    }
                    else if (item.Response.EndsWith("ResultModel"))
                    {
                        baseFuncType = item.Response[0..^"ResultModel".Length];
                        resultType = $"async Task<{baseFuncType}>";
                    }
                    else
                    {
                        baseFuncType = item.Response;
                        resultType = $"async Task<{baseFuncType}>";
                    }
                    if (!string.IsNullOrWhiteSpace(baseFuncType))
                    {
                        baseFuncType = $"<{baseFuncType}>";
                    }
                    #region 替换大写类型
                    resultType = resultType.Replace("String", "string");
                    #endregion
                    string baseFunc = $"await {baseFuncName}{item.HttpMethod}Async{baseFuncType}";
                    #endregion
                    #region 解析传入参数
                    List<string> paramCodes = new();
                    List<string> baseFuncArgs = new()
                    {
                        $"\"{item.ControllerName}/{item.ActionName}\""
                    };
                    #region Body参数
                    if (!string.IsNullOrWhiteSpace(item.BodyParam))
                    {
                        paramCodes.Add($"{item.BodyParam} requestModel");
                        baseFuncArgs.Add("requestModel");
                    }
                    #endregion
                    #region Query参数
                    if (item.QueryParams != null)
                    {
                        List<string> queryArgs = new();
                        foreach (QueryParamModel queryParam in item.QueryParams)
                        {
                            paramCodes.Add($"{queryParam.CSharpType} {queryParam.Name}");
                            if (queryParam.CSharpType.EndsWith("?"))
                            {
                                if (queryParam.CSharpType == "string?")
                                {
                                    queryArgs.Add($"[nameof({queryParam.Name})]={queryParam.Name} ?? string.Empty");
                                }
                                else
                                {
                                    queryArgs.Add($"[nameof({queryParam.Name})]={queryParam.Name}?.ToString() ?? string.Empty");
                                }
                            }
                            else
                            {
                                if (queryParam.CSharpType == "string")
                                {
                                    queryArgs.Add($"[nameof({queryParam.Name})]={queryParam.Name}");
                                }
                                else
                                {
                                    queryArgs.Add($"[nameof({queryParam.Name})]={queryParam.Name}.ToString()");
                                }
                            }
                        }
                        if(queryArgs.Count > 0)
                        {
                            baseFuncArgs.Add("new Dictionary<string, string>{" + string.Join(", ", queryArgs) + "}");
                        }
                    }
                    #endregion
                    string paramsCode = string.Join(", ", paramCodes);
                    string baseFuncCode = string.Join(", ", baseFuncArgs);
                    #endregion
                    codeContent.AppendLine($"        public {resultType} {item.ActionName}Async({paramsCode}) => {baseFunc}({baseFuncCode});");
                    actionCount++;
                }
                codeContent.AppendLine($"    }}");
                codeContent.AppendLine($"}}");
                if (actionCount > 0)
                {
                    WriteFile("", $"{path.Key}HttpClient.cs", codeContent.ToString());
                }
            }
        }
        /// <summary>
        /// 创建默认模型
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        private string CreateDefaultModel(SchemaModel schema)
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {PrefixName}.HttpClient.Models");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    public class {schema.Name}");
            codeContent.AppendLine($"    {{");
            foreach (PropertyModel property in schema.Properties)
            {
                SchemaModel? targetSchema = _swaggerContent?.Schemas?.FirstOrDefault(m => m.Name == property.Type);
                if (targetSchema != null)
                {
                    codeContent.AppendLine($"        /// <summary>");
                    codeContent.AppendLine($"        /// {targetSchema.Description}");
                    codeContent.AppendLine($"        /// </summary>");
                }
                else if (!string.IsNullOrWhiteSpace(property.Description))
                {
                    codeContent.AppendLine($"        /// <summary>");
                    codeContent.AppendLine($"        /// {property.Description}");
                    codeContent.AppendLine($"        /// </summary>");
                }
                string attributeContent = string.Empty;
                if (!property.IsNull)
                {
                    attributeContent += "Required";
                }
                if (property.MaxLength != null)
                {
                    if (!property.IsNull)
                    {
                        attributeContent += ", ";
                    }
                    attributeContent += "StringLength(100";
                    if (property.MinLength != null)
                    {
                        attributeContent += ", MinimumLength = 0";
                    }
                    attributeContent += ")";
                }
                if (!string.IsNullOrWhiteSpace(attributeContent))
                {
                    codeContent.AppendLine($"        [{attributeContent}]");
                }
                string getset = property.ReadOnly ? "{ get; }" : "{ get; set; }";
                if (targetSchema != null && targetSchema.IsEnum)
                {
                    codeContent.AppendLine($"        public {targetSchema.Type.GetCSharpType(targetSchema.Format, property.IsNull)} {property.Name} {getset}");
                }
                else
                {
                    codeContent.AppendLine($"        public {property.CSharpType} {property.Name} {getset}{property.DefaultValue}");
                }
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            return codeContent.ToString();
        }
        /// <summary>
        /// 创建模型文件
        /// </summary>
        private void CreateModelFiles()
        {
            string[] blackList = new string[]
            {
                "PageModel"
            };
            if (_swaggerContent == null || _swaggerContent.Schemas == null) return;
            foreach (SchemaModel schema in _swaggerContent.Schemas)
            {
                if (blackList.Contains(schema.Name)) continue;
                if (schema.IsEnum) continue;
                if (schema.Properties == null || schema.Properties.Count <= 0) continue;
                if (schema.Name.EndsWith("ResultModel")) continue;
                string codeContent = CreateDefaultModel(schema);
                WriteFile("Models", $"{schema.Name}.cs", codeContent);
            }
        }
        /// <summary>
        /// 创建HttpClientBase
        /// </summary>
        private void CreateHttpClientBaseFile()
        {
            string templeteContent = GetTempleteContent("HttpClientBase");
            WriteFile("Base", "HttpClientBase.cs", templeteContent);
        }
        /// <summary>
        /// 创建CSharpProject文件
        /// </summary>
        private void CreateCSharpProjectFile()
        {
            string templeteContent = GetTempleteContent("CSharpProject");
            WriteFile("", $"{ProjectName}.HttpClient.csproj", templeteContent);
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
        private void WriteFile(string path, string fileName, string content)
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
