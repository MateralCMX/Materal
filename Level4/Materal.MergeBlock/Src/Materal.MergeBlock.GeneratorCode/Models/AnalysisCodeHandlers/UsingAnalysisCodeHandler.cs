namespace Materal.MergeBlock.GeneratorCode.Models.AnalysisCodeHandlers
{
    /// <summary>
    /// 引用分析代码处理器
    /// </summary>
    public class UsingAnalysisCodeHandler : AnalysisCodeHandler
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
            if (!code.StartsWith("using ")) return true;
            string usingCode = code[6..^1];
            interfaceModel.Usings.Add(usingCode);
            return false;
        }
    }
}
