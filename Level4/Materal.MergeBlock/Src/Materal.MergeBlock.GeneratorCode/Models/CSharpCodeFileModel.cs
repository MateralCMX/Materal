using Materal.MergeBlock.GeneratorCode.Models.AnalysisCodeHandlers;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// C#代码文件模型
    /// </summary>
    public abstract class CSharpCodeFileModel
    {
        /// <summary>
        /// 引用组
        /// </summary>
        public List<string> Usings { get; set; } = [];
        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; set; } = string.Empty;
        /// <summary>
        /// 注释
        /// </summary>
        public string? Annotation { get; set; }
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; set; } = [];
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 获得分析代码处理器
        /// </summary>
        /// <returns></returns>
        protected virtual AnalysisCodeHandler GetAnalysisCodeHandler()
        {
            AnalysisCodeHandler analysisCodeHandler = new UsingAnalysisCodeHandler
            {
                NextHandler = new NamespaceAnalysisCodeHandler
                {
                    NextHandler = new NameAnalysisCodeHandler()
                    {
                        NextHandler = new PropertyAnalysisCodeHandler()
                        {
                            NextHandler = new InterfaceMethodAnalysisCodeHandler()
                        }
                    }
                }
            };
            return analysisCodeHandler;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="codes"></param>
        public CSharpCodeFileModel(string[] codes)
        {
            if (codes.Length < 0) return;
            AnalysisCodeHandler analysisCodeHandler = GetAnalysisCodeHandler();
            for (int i = 0; i < codes.Length; i++)
            {
                analysisCodeHandler.Handler(this, codes, i);
            }
        }
    }
}
