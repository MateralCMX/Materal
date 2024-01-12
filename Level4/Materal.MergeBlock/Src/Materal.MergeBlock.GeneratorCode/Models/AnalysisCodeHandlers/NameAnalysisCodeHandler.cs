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
        /// <param name="interfaceModel"></param>
        /// <param name="code"></param>
        /// <param name="codes"></param>
        /// <param name="currentLine"></param>
        /// <returns></returns>
        protected override bool AnalysisCodes(InterfaceModel interfaceModel, string code, string[] codes, int currentLine)
        {
            int index = code.IndexOf(" class ");
            string codeContent;
            if (index > 0)
            {
                codeContent = code[(index + 7)..];
            }
            else
            {
                index = code.IndexOf(" interface ");
                if (index < 0) return true;
                codeContent = code[(index + 11)..];
            }
            index = codeContent.IndexOf(" : ");
            #region Name
            if (index > 0)
            {
                interfaceModel.Name = codeContent[..index];
            }
            else
            {
                interfaceModel.Name = codeContent;
            }
            #endregion
            #region 父类和接口
            if (index > 0)
            {
                codeContent = codeContent[(index + 3)..];
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
            #endregion
            #region 其他
            interfaceModel.Annotation = StringHelper.GetAnnotationSetAttributes(codes, currentLine, interfaceModel.Attributes);
            #endregion
            return false;
        }
    }
}
