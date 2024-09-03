namespace Materal.MergeBlock.GeneratorCode.Models.AnalysisCodeHandlers
{
    /// <summary>
    /// 名称分析代码处理器
    /// </summary>
    public class EnumValueAnalysisCodeHandler : AnalysisCodeHandler
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
            if (cSharpCodeFileModel is not EnumModel enumModel || string.IsNullOrWhiteSpace(enumModel.Name)) return true;
            string trueCode = code.Trim();
            if (trueCode.StartsWith("{") || trueCode.StartsWith("///") || trueCode.StartsWith("[") || trueCode.StartsWith("}")) return false;
            if (trueCode.EndsWith(","))
            {
                trueCode = trueCode[..^1];
            }
            string[] valueCodes = trueCode.Split('=');
            EnumValueModel enumValue = new() { Name = valueCodes[0].Trim() };
            enumValue.Annotation = StringHelper.GetAnnotationSetAttributes(codes, currentLine, enumValue.Attributes);
            enumModel.Values.Add(enumValue);
            return false;
        }
    }
}
