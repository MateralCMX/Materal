using Newtonsoft.Json.Linq;
using System.Text;

namespace Materal.HttpGenerator.Swagger.Models
{
    public class SwaggerContentModel
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string? OpenAPI { get; }
        /// <summary>
        /// 地址组
        /// </summary>
        public List<PathModel>? Paths { get; }
        /// <summary>
        /// 组件
        /// </summary>
        public List<SchemaModel>? Schemas { get; }
        public SwaggerContentModel(JObject source)
        {
            string[] schemasBlackList = new[]
            {
                "PageModel"
            };
            foreach (KeyValuePair<string, JToken?> item in source)
            {
                if (item.Value == null) continue;
                switch (item.Key)
                {
                    case "openapi":
                        OpenAPI = item.Value.ToString();
                        break;
                    case "paths":
                        Paths = new();
                        foreach (JToken? path in item.Value)
                        {
                            if (path == null || path is not JProperty pathItem) continue;
                            Paths.Add(new(pathItem.Value, pathItem.Name));
                        }
                        break;
                    case "components":
                        JToken? schemas = item.Value["schemas"];
                        if (schemas == null) continue;
                        Schemas = new();
                        foreach (JToken schema in schemas)
                        {
                            if (schema is not JProperty schemaProperty) continue;
                            if (schemaProperty.Name.EndsWith("ResultModel")) continue;
                            if (schemasBlackList.Contains(schemaProperty.Name)) continue;
                            Schemas.Add(new(schemaProperty));
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(string? prefixName)
        {
            if (Schemas != null && Schemas.Count > 0)
            {
                foreach (SchemaModel schema in Schemas)
                {
                    schema.Init(prefixName);
                }
                foreach (SchemaModel schema in Schemas)
                {
                    schema.InitProperty(Schemas, prefixName);
                }
            }
            if (Paths != null && Paths.Count > 0)
            {
                foreach (PathModel path in Paths)
                {
                    path.Init(Schemas, prefixName);
                }
            }
        }
        /// <summary>
        /// 创建HttpClient文件
        /// </summary>
        /// <param name="generatorBuildImpl"></param>
        public void CreateHttpClientFiles(GeneratorBuildImpl generatorBuildImpl)
        {
            if (Paths == null) return;
            List<IGrouping<string, PathModel>> paths = Paths.GroupBy(m => m.ControllerName).ToList();
            foreach (IGrouping<string?, PathModel> path in paths)
            {
                if (path.Key == null) continue;
                StringBuilder codeContent = new();
                codeContent.AppendLine($"using Materal.Model;");
                codeContent.AppendLine($"using Materal.HttpClient.Base;");
                codeContent.AppendLine($"using {generatorBuildImpl.ProjectName}.HttpClient.Models;");
                codeContent.AppendLine($"");
                codeContent.AppendLine($"namespace {generatorBuildImpl.ProjectName}.HttpClient");
                codeContent.AppendLine($"{{");
                codeContent.AppendLine($"    public class {path.Key}HttpClient : HttpClientBase");
                codeContent.AppendLine($"    {{");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 构造方法");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        public {path.Key}HttpClient() : base(\"{generatorBuildImpl.ProjectName}\") {{ }}");
                int actionCount = 0;
                foreach (PathModel pathItem in path)
                {
                    string? itemCode = pathItem.GetCode();
                    if(string.IsNullOrWhiteSpace(itemCode)) continue;
                    codeContent.AppendLine(itemCode);
                    actionCount++;
                }
                codeContent.AppendLine($"    }}");
                codeContent.AppendLine($"}}");
                if (actionCount <= 0) continue;
                generatorBuildImpl.SaveFile("", $"{path.Key}HttpClient.cs", codeContent.ToString());
            }
        }
        /// <summary>
        /// 创建模型文件
        /// </summary>
        /// <param name="generatorBuildImpl"></param>
        public void CreateModelFiles(GeneratorBuildImpl generatorBuildImpl)
        {
            if (Schemas == null) return;
            foreach (SchemaModel schema in Schemas)
            {
                if (schema.IsEnum) continue;
                if (schema.Properties == null || schema.Properties.Count <= 0) continue;
                schema.CreateModelFile(generatorBuildImpl);
            }
        }
    }
}
