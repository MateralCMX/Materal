using Materal.Common;
using Materal.ConvertHelper;
using Materal.HttpGenerator.Swagger.Models;
using Materal.NetworkHelper;
using Materal.StringHelper;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;

namespace Materal.HttpGenerator.Swagger
{
    public class GeneratorBuildImpl : IGeneratorBuild
    {
        /// <summary>
        /// Swgger内容
        /// </summary>
        public SwaggerContentModel? SwaggerContent { get; set; }
        public string OutputPath { get; set; } = string.Empty;
        public string ProjectName { get; set; } = "MateralProject";
        public string BaseUrl { get; set; } = "http://localhost:5000/api/";
        public GeneratorBuildImpl()
        {
            OutputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HttpClientOutput");
        }
        public async Task SetSourceAsync(string soucre)
        {
            string swaggerJson;
            if (soucre.IsUrl() && soucre.EndsWith(".json"))
            {
                swaggerJson = await HttpManager.SendGetAsync(soucre);
            }
            else
            {
                swaggerJson = soucre;
            }
            JObject jObj = swaggerJson.JsonToDeserializeObject<JObject>();
            SwaggerContent = new SwaggerContentModel(jObj);
        }
        public Task BuildAsync()
        {
            if (Directory.Exists(OutputPath))
            {
                Directory.Delete(OutputPath, true);
            }
            CreateCSharpProjectFile();
            CreateHttpClientBaseFile();
            CreateModelFiles();
            CreateHttpClientFiles();
            return Task.CompletedTask;
        }
        /// <summary>
        /// 创建HttpClient
        /// </summary>
        private void CreateHttpClientFiles()
        {
            if (SwaggerContent == null || SwaggerContent.Paths == null) return;
            var paths = SwaggerContent.Paths.GroupBy(m => m.ControllerName).ToList();
            foreach (IGrouping<string?, PathModel> path in paths)
            {
                if (path.Key == null) continue;
                StringBuilder codeConent = new();
                codeConent.AppendLine($"using Materal.Model;");
                codeConent.AppendLine($"using {ProjectName}.HttpClient.Base;");
                codeConent.AppendLine($"using {ProjectName}.HttpClient.Models;");
                codeConent.AppendLine($"");
                codeConent.AppendLine($"namespace {ProjectName}.HttpClient");
                codeConent.AppendLine($"{{");
                codeConent.AppendLine($"    public class {path.Key}HttpClient : HttpClientBase");
                codeConent.AppendLine($"    {{");
                int actionCount = 0;
                foreach (PathModel item in path)
                {
                    if (string.IsNullOrWhiteSpace(item.ActionName) || item.ControllerName == item.ActionName) continue;
                    if (item.Description != null)
                    {
                        codeConent.AppendLine($"        /// <summary>");
                        codeConent.AppendLine($"        /// {item.Description}");
                        codeConent.AppendLine($"        /// </summary>");
                    }
                    string start = "        public async Task";
                    string temp = $" {item.ActionName}Async";
                    string paramsStr = $"(\"{item.ControllerName}/{item.ActionName}\"";
                    if (item.BodyParam != null && item.QueryParams != null)
                    {
                        temp += $"({item.BodyParam} data, ";
                        paramsStr += ", data, new() { ";
                        foreach (QueryParamModel queryParam in item.QueryParams)
                        {
                            temp += $"{queryParam.TrueType} {queryParam.Name}, ";
                            paramsStr += $"[nameof({queryParam.Name})] = {queryParam.Name}.ToString(), ";
                        }
                        paramsStr = paramsStr.Substring(0, paramsStr.Length - 2);
                        temp = temp.Substring(0, temp.Length - 2);
                        temp += $") =>";
                        paramsStr += " });";
                    }
                    else if (item.BodyParam != null)
                    {
                        temp += $"({item.BodyParam} data) => ";
                        paramsStr += ", data);";
                    }
                    else if (item.QueryParams != null)
                    {
                        temp += $"(";
                        paramsStr += ", null, new() { ";
                        foreach (QueryParamModel queryParam in item.QueryParams)
                        {
                            temp += $"{queryParam.TrueType} {queryParam.Name}, ";
                            paramsStr += $"[nameof({queryParam.Name})] = {queryParam.Name}.ToString(), ";
                        }
                        paramsStr = paramsStr.Substring(0, paramsStr.Length - 2);
                        temp = temp.Substring(0, temp.Length - 2);
                        temp += $") =>";
                        paramsStr += " });";
                    }
                    else
                    {
                        temp += $"() => ";
                        paramsStr += ");";
                    }
                    if (item.Response != null)
                    {
                        if (item.Response == "ResultModel")
                        {
                            temp += $"await GetResultModelBy{item.HttpType}Async";
                        }
                        else if (item.Response.EndsWith("PageResultModel"))
                        {
                            int index = item.Response.LastIndexOf("PageResultModel");
                            string type = HandlerType(item.Response.Substring(0, index));
                            start += $"<(List<{type}> data, PageModel pageInfo)>";
                            temp += $"await GetPageResultModelBy{item.HttpType}Async<{type}>";
                        }
                        else if (item.Response.EndsWith("ListResultModel"))
                        {
                            int index = item.Response.LastIndexOf("ListResultModel");
                            string type = HandlerType(item.Response.Substring(0, index));
                            start += $"<List<{type}>>";
                            temp += $"await GetResultModelBy{item.HttpType}Async<List<{type}>>";
                        }
                        else if (item.Response.EndsWith("ResultModel"))
                        {
                            int index = item.Response.LastIndexOf("ResultModel");
                            string type = HandlerType(item.Response.Substring(0, index));
                            start += $"<{type}>";
                            temp += $"await GetResultModelBy{item.HttpType}Async<{type}>";
                        }
                    }
                    temp = start + temp + paramsStr;
                    codeConent.AppendLine(temp);
                    actionCount++;
                }
                codeConent.AppendLine($"    }}");
                codeConent.AppendLine($"}}");
                if(actionCount > 0)
                {
                    WriteFile("", $"{path.Key}HttpClient.cs", codeConent.ToString());
                }
            }
        }
        private string HandlerType(string type)
        {
            if(type == nameof(Decimal)) return "decimal";
            else if (type == nameof(Double)) return "double";
            else if (type == nameof(Single)) return "float";
            else if (type == nameof(Int16)) return "short";
            else if (type == nameof(Int32)) return "int";
            else if (type == nameof(Int64)) return "long";
            else if (type == nameof(String)) return "string";
            else if (type == nameof(UInt16)) return "ushort";
            else if (type == nameof(UInt32)) return "uint";    
            else if (type == nameof(UInt64)) return "ulong";
            else if (type.EndsWith("List"))
            {
                type = $"List<{type.Substring(0, type.Length - 4)}>";
                return type;
            }
            else if (type.EndsWith("Dictionary"))
            {
                if (type.StartsWith(nameof(String)))
                {
                    type = HandlerType(type.Substring(0, type.Length - "Dictionary".Length).Substring(nameof(String).Length));
                    var reslt = $"Dictionary<string, {type}>";
                    return reslt;
                }
                else if (type.StartsWith(nameof(Int32)))
                {
                    type = HandlerType(type.Substring(0, type.Length - "Dictionary".Length).Substring(nameof(Int32).Length));
                    var reslt = $"Dictionary<int, {type}>";
                    return reslt;
                }
                else if (type.StartsWith(nameof(DateTime)))
                {
                    type = HandlerType(type.Substring(0, type.Length - "Dictionary".Length).Substring(nameof(DateTime).Length));
                    var reslt = $"Dictionary<DateTime, {type}>";
                    return reslt;
                }
                return type;
            }
            return type;
        }
        /// <summary>
        /// 创建模型文件
        /// </summary>
        private void CreateModelFiles()
        {
            string[] blackList = new[]
            {
                "ResultModel",
                "ResultTypeEnum",
                "PageModel"
            };
            if (SwaggerContent == null || SwaggerContent.Components == null || SwaggerContent.Components.Schemas == null) return;
            foreach (KeyValuePair<string, SchemaModel> item in SwaggerContent.Components.Schemas)
            {
                if (blackList.Any(m => item.Key.EndsWith(m))) continue;
                StringBuilder codeConent = new();
                if (item.Value.Enum != null && item.Value.Enum.Count > 0)
                {
                    #region 生成Enum
                    codeConent.AppendLine($"namespace {ProjectName}.HttpClient.Models");
                    codeConent.AppendLine($"{{");
                    codeConent.AppendLine($"    public enum {item.Key}");
                    codeConent.AppendLine($"    {{");
                    foreach (int @enum in item.Value.Enum)
                    {
                        codeConent.AppendLine($"        Item{@enum} = {@enum},");
                    }
                    codeConent.AppendLine($"    }}");
                    codeConent.AppendLine($"}}");
                    #endregion
                }
                else
                {
                    codeConent.AppendLine($"using System.ComponentModel.DataAnnotations;");
                    codeConent.AppendLine($"");
                    codeConent.AppendLine($"namespace {ProjectName}.HttpClient.Models");
                    codeConent.AppendLine($"{{");
                    codeConent.AppendLine($"    public class {item.Key}");
                    codeConent.AppendLine($"    {{");
                    if (item.Value.Properties != null && item.Value.Properties.Count > 0)
                    {
                        foreach (KeyValuePair<string, PropertyModel> property in item.Value.Properties)
                        {
                            if (property.Value.Description != null)
                            {
                                codeConent.AppendLine($"        /// <summary>");
                                codeConent.AppendLine($"        /// {property.Value.Description}");
                                codeConent.AppendLine($"        /// </summary>");
                            }
                            string attributeContent = "";
                            string suffix = string.Empty;
                            if(item.Value.Required?.Count > 0 && item.Value.Required.Contains(property.Key))
                            {

                            }
                            if (!property.Value.Nullable || (item.Value.Required?.Count > 0 && item.Value.Required.Contains(property.Key)))
                            {
                                attributeContent += "Required";
                                suffix = property.Value.TrueType switch
                                {
                                    "string" => " = string.Empty;",
                                    _ => string.Empty
                                };
                            }
                            if (property.Value.MaxLength != null)
                            {
                                if (!property.Value.Nullable)
                                {
                                    attributeContent += ", ";
                                }
                                attributeContent += "StringLength(100";
                                if (property.Value.MinLength != null)
                                {
                                    attributeContent += ", MinimumLength = 0";
                                }
                                attributeContent += ")";
                            }
                            if (!string.IsNullOrWhiteSpace(attributeContent))
                            {
                                codeConent.AppendLine($"        [{attributeContent}]");
                            }
                            codeConent.AppendLine($"        public {property.Value.TrueType} {property.Key} {{ get; set; }}{suffix}");
                        }
                    }
                    codeConent.AppendLine($"    }}");
                    codeConent.AppendLine($"}}");
                }
                WriteFile("Models", $"{item.Key}.cs", codeConent.ToString());
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
            fileContent = fileContent.Replace("{{BaseUrl}}", BaseUrl);
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
            File.WriteAllText(outputPath, content);
        }
    }
}
