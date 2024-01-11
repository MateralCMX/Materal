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
                foreach (string item in codeContents)
                {
                    if (interfaceModel is ClassModel classModel)
                    {
                        if ((classModel.BaseClass is null || string.IsNullOrWhiteSpace(classModel.BaseClass)) && !item.StartsWith("I"))
                        {
                            classModel.BaseClass = item.Trim();
                        }
                        else
                        {
                            classModel.Interfaces.Add(item.Trim());
                        }
                    }
                    else
                    {
                        interfaceModel.Interfaces.Add(item.Trim());
                    }
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
