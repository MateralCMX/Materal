namespace Materal.MergeBlock.GeneratorCode.Models.AnalysisCodeHandlers
{
    /// <summary>
    /// 命名空间分析代码处理器
    /// </summary>
    public class NamespaceAnalysisCodeHandler : AnalysisCodeHandler
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
            if (!code.StartsWith("namespace ")) return true;
            cSharpCodeFileModel.Namespace = code[10..];
            return false;
        }
    }
}
