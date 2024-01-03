using Materal.MergeBlock.GeneratorCode.Models.AnalysisCodeHandlers;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 接口模型
    /// </summary>
    public class InterfaceModel
    {
        /// <summary>
        /// 分析代码处理器
        /// </summary>
        protected static AnalysisCodeHandler AnalysisCodeHandler { get; set; }
        static InterfaceModel()
        {
            AnalysisCodeHandler = new UsingAnalysisCodeHandler
            {
                NextHandler = new NamespaceAnalysisCodeHandler
                {
                    NextHandler = new NameAnalysisCodeHandler()
                    {
                        NextHandler = new PropertiesAnalysisCodeHandler()
                    }
                }
            };
        }
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
        /// 名称
        /// </summary>
        public List<string> Interfaces { get; set; } = [];
        /// <summary>
        /// 属性
        /// </summary>
        public List<PropertyModel> Properties { get; set; } = [];
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="codes"></param>
        public InterfaceModel(string[] codes)
        {
            if (codes.Length < 0) return;
            for (int i = 0; i < codes.Length; i++)
            {
                AnalysisCodeHandler.Handler(this, codes, i);
            }
        }
    }
}
