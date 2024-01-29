using Materal.MergeBlock.GeneratorCode.Extensions;

namespace Materal.MergeBlock.GeneratorCode.Models.AnalysisCodeHandlers
{
    /// <summary>
    /// 方法分析代码处理器
    /// </summary>
    public class InterfaceMethodAnalysisCodeHandler : AnalysisCodeHandler
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
            if (!code.EndsWith(");")) return true;
            if (cSharpCodeFileModel is not InterfaceModel interfaceModel) return true;
            MethodModel methodModel = new();
            string frontCode;
            string residualCode = code;
            int index = 0;
            int upIndex = 0;
            do
            {
                index += residualCode.IndexOf("(");
                if (index < upIndex) return true;
                upIndex = index;
                frontCode = code[0..index];
                residualCode = code[(++index)..];
            } while (!frontCode.IsFullCodeBlock());
            index = frontCode.LastIndexOf(" ");
            methodModel.ReturnType = frontCode[0..index];
            if (methodModel.IsTaskReturnType)
            {
                if (methodModel.ReturnType == "Task")
                {
                    methodModel.NotTaskReturnType = "void";
                }
                else
                {
                    methodModel.NotTaskReturnType = methodModel.ReturnType[5..^1];
                }
            }
            else
            {
                methodModel.NotTaskReturnType = methodModel.ReturnType;
            }
            methodModel.Name = frontCode[(index + 1)..];
            string secondCode = residualCode[..^2];
            if (!string.IsNullOrWhiteSpace(secondCode))
            {
                string[] secondCodes = secondCode.Split(',');
                string argumentCode = string.Empty;
                foreach (string item in secondCodes)
                {
                    if (string.IsNullOrWhiteSpace(argumentCode))
                    {
                        argumentCode = item;
                    }
                    else
                    {
                        argumentCode += $",{item}";
                    }
                    if(argumentCode.IsFullCodeBlock())
                    {
                        MethodArgumentModel argument = new(argumentCode);
                        methodModel.Arguments.Add(argument);
                        argumentCode = string.Empty;
                    }
                }
            }
            methodModel.Annotation = StringHelper.GetAnnotationSetAttributes(codes, currentLine, methodModel.Attributes);
            interfaceModel.Methods.Add(methodModel);
            return false;
        }
    }
}
