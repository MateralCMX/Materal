using Materal.BaseCore.CodeGenerator.Extensions;

namespace Materal.BaseCore.CodeGenerator.Models
{
    public class InterfaceMethodModel
    {
        /// <summary>
        /// 生成代码
        /// </summary>
        public bool GeneratorCode { get; set; }
        /// <summary>
        /// 注释
        /// </summary>
        public List<string> Annotations { get; set; } = new();
        /// <summary>
        /// Http类型
        /// </summary>
        public string HttpMethod { get; set; } = string.Empty;
        /// <summary>
        /// 返回类型
        /// </summary>
        public string ResultType { get; set; } = string.Empty;
        /// <summary>
        /// 有返回对象
        /// </summary>
        public bool HasResultDataModel => ResultType != "void" && ResultType != "Task";
        /// <summary>
        /// 返回模型类型
        /// </summary>
        public string ResultModelType { get; set; } = string.Empty;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 参数
        /// </summary>
        public List<ParamModel> Params { get; set; } = new List<ParamModel>();
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; set; } = new();
        /// <summary>
        /// 是异步
        /// </summary>
        public bool IsTask => ResultType.StartsWith("Task");

        public InterfaceMethodModel()
        {
        }
        public InterfaceMethodModel(string[] codes, int startIndex)
        {
            string code = codes[startIndex].Trim();
            string[] codeLines = code.Split(' ');
            int index = 0;
            ResultType = codeLines[index++];
            while (!ResultType.IsFullCodeBlock())
            {
                ResultType += $" {codeLines[index++]}";
            }
            index = ResultType.Length;
            if (HasResultDataModel)
            {
                if (ResultType.Contains(", PageModel "))
                {
                    string temp = ResultType;
                    if (temp.StartsWith("Task"))
                    {
                        temp = temp[5..^1];
                    }
                    int startAngleBracket = temp.IndexOf("<");
                    temp = temp[(startAngleBracket + 1)..];
                    int endAngleBracket = temp.IndexOf(">");
                    temp = temp[..endAngleBracket];
                    ResultModelType = $"PageResultModel<{temp}>";
                }
                else
                {
                    if (ResultType.StartsWith("Task"))
                    {
                        ResultModelType = $"ResultModel<{ResultType[5..^1]}>";
                    }
                    else
                    {
                        ResultModelType = $"ResultModel<{ResultType}>";
                    }
                }
            }
            else
            {
                ResultModelType = $"ResultModel";
            }
            code = code[(index + 1)..];
            index = code.IndexOf("(");
            Name = code[..index];
            code = code[(index + 1)..];
            index = code.IndexOf(')');
            code = code[..index];
            if (!string.IsNullOrWhiteSpace(code))
            {
                Params = code.Split(',').Select(m => new ParamModel(m.Trim())).ToList();
            }
            #region 解析特性
            int tempIndex = startIndex - 1;
            do
            {
                if (tempIndex < 0) break;
                string attributeCode = codes[tempIndex].Trim();
                if (!attributeCode.StartsWith("[") || !attributeCode.EndsWith("]")) break;
                tempIndex -= 1;
                List<string> attributeCodes = attributeCode.GetAttributeCodes();
                Attributes.AddRange(attributeCodes.Select(attributeName => new AttributeModel(attributeName.Trim())));
            } while (true);
            GeneratorCode = Attributes.HasAttribute<MapperControllerAttribute>();
            if (GeneratorCode)
            {
                AttributeModel attributeModel = Attributes.GetAttribute<MapperControllerAttribute>();
                HttpMethod = attributeModel.AttributeArguments[0].Value.Split('.')[1];
            }
            #endregion
            #region 解析注释
            code = codes[tempIndex].Trim();
            while (code.StartsWith("///"))
            {
                Annotations.Insert(0, code);
                code = codes[tempIndex--].Trim();
            }
            #endregion
        }
    }
}
