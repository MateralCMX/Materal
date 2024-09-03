namespace Materal.MergeBlock.GeneratorCode.Models.AnalysisCodeHandlers
{
    /// <summary>
    /// 分析代码处理器
    /// </summary>
    public abstract class AnalysisCodeHandler
    {
        /// <summary>
        /// 下一个处理器
        /// </summary>
        public AnalysisCodeHandler? NextHandler { get; set; }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="cSharpCodeFileModel"></param>
        /// <param name="codes"></param>
        /// <param name="currentLine"></param>
        public virtual void Handler(CSharpCodeFileModel cSharpCodeFileModel, string[] codes, int currentLine)
        {
            if (currentLine >= codes.Length) return;
            string code = codes[currentLine].Trim();
            if (code is null || string.IsNullOrWhiteSpace(code)) return;
            if (!AnalysisCodes(cSharpCodeFileModel, code, codes, currentLine)) return;
            NextHandler?.Handler(cSharpCodeFileModel, codes, currentLine);
        }
        /// <summary>
        /// 分析代码
        /// </summary>
        /// <param name="cSharpCodeFileModel"></param>
        /// <param name="code"></param>
        /// <param name="codes"></param>
        /// <param name="currentLine"></param>
        protected abstract bool AnalysisCodes(CSharpCodeFileModel cSharpCodeFileModel, string code, string[] codes, int currentLine);
    }
    /// <summary>
    /// 分析代码处理器
    /// </summary>
    public abstract class AnalysisCodeHandler<T> : AnalysisCodeHandler
        where T : InterfaceModel
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
            if (cSharpCodeFileModel is not T model) return true;
            return AnalysisCodes(model, code, codes, currentLine);
        }
        /// <summary>
        /// 分析代码
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <param name="codes"></param>
        /// <param name="currentLine"></param>
        protected abstract bool AnalysisCodes(T model, string code, string[] codes, int currentLine);
    }
}
