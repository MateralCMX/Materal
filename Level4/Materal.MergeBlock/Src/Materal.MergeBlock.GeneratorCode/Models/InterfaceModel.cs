namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 接口模型
    /// </summary>
    public class InterfaceModel(string[] codes) : CSharpCodeFileModel(codes)
    {
        /// <summary>
        /// 接口
        /// </summary>
        public List<string> Interfaces { get; set; } = [];
        /// <summary>
        /// 属性
        /// </summary>
        public List<PropertyModel> Properties { get; set; } = [];
        /// <summary>
        /// 方法
        /// </summary>
        public List<MethodModel> Methods { get; set; } = [];
    }
}
