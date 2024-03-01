using Materal.MergeBlock.GeneratorCode.Models.AnalysisCodeHandlers;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 枚举模型
    /// </summary>
    public class EnumModel(string[] codes) : CSharpCodeFileModel(codes)
    {
        /// <summary>
        /// 值
        /// </summary>
        public List<EnumValueModel> Values { get; set; } = [];
        /// <summary>
        /// 获取分析代码处理者
        /// </summary>
        /// <returns></returns>
        protected override AnalysisCodeHandler GetAnalysisCodeHandler()
        {
            AnalysisCodeHandler analysisCodeHandler = new UsingAnalysisCodeHandler
            {
                NextHandler = new NamespaceAnalysisCodeHandler
                {
                    NextHandler = new NameAnalysisCodeHandler()
                    {
                        NextHandler = new EnumValueAnalysisCodeHandler()
                    }
                }
            };
            return analysisCodeHandler;
        }
    }
}
