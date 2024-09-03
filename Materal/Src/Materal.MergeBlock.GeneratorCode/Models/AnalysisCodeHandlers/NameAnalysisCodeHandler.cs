using Materal.MergeBlock.GeneratorCode.Extensions;

namespace Materal.MergeBlock.GeneratorCode.Models.AnalysisCodeHandlers
{
    /// <summary>
    /// 名称分析代码处理器
    /// </summary>
    public class NameAnalysisCodeHandler : AnalysisCodeHandler
    {
        /// <summary>
        /// 分析代码
        /// </summary>
        /// <param name="cSharpCodeFileModel"></param>
        /// <param name="code"></param>
        /// <param name="codes"></param>
        /// <param name="currentLine"></param>
        /// <returns></returns>
        protected override bool AnalysisCodes(CSharpCodeFileModel cSharpCodeFileModel, string code, string[] codes, int currentLine)
        {
            #region Name
            string? name = GetName(code, "class");
            name ??= GetName(code, "interface");
            name ??= GetName(code, "enum");
            if (name is null) return true;
            cSharpCodeFileModel.Name = name;
            #endregion
            #region 父类和接口
            if (cSharpCodeFileModel is InterfaceModel interfaceModel)
            {
                int index = code.LastIndexOf(" : ");
                if (index > 0)
                {
                    string codeContent = code[(index + 3)..];
                    string[] codeContents = codeContent.Split(',');
                    codeContent = string.Empty;
                    foreach (string item in codeContents)
                    {
                        if (string.IsNullOrWhiteSpace(codeContent))
                        {
                            codeContent = item;
                        }
                        else
                        {
                            codeContent += $",{item}";
                        }
                        if (!codeContent.IsFullCodeBlock()) continue;
                        if (interfaceModel is ClassModel classModel)
                        {
                            if ((classModel.BaseClass is null || string.IsNullOrWhiteSpace(classModel.BaseClass)) && !codeContent.StartsWith("I"))
                            {
                                classModel.BaseClass = codeContent.Trim();
                            }
                            else
                            {
                                classModel.Interfaces.Add(codeContent.Trim());
                            }
                        }
                        else
                        {
                            interfaceModel.Interfaces.Add(codeContent.Trim());
                        }
                        codeContent = string.Empty;
                    }
                }
            }
            #endregion
            #region 其他
            cSharpCodeFileModel.Annotation = StringHelper.GetAnnotationSetAttributes(codes, currentLine, cSharpCodeFileModel.Attributes);
            #endregion
            return false;
        }
        private string? GetName(string code, string type)
        {
            int index = code.IndexOf($" {type} ");
            if (index < 0) return null;
            string codeContent = code[(index + type.Length + 2)..];
            index = codeContent.IndexOf(" : ");
            if (index > 0)
            {
                return codeContent[..index];
            }
            else
            {
                return codeContent;
            }
        }
    }
}
