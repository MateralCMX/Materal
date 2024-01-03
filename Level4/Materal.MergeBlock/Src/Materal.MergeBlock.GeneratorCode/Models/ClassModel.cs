namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 类模型
    /// </summary>
    /// <param name="codes"></param>
    public class ClassModel(string[] codes) : InterfaceModel(codes)
    {
        /// <summary>
        /// 父类
        /// </summary>
        public string? BaseClass { get; set; }
    }
}
