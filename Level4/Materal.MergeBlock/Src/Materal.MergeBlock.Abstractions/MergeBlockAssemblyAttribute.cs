namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock程序集标识
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class MergeBlockAssemblyAttribute(string description, string? moduleName = null, string[]? depends = null) : Attribute
    {
        /// <summary>
        /// 模块描述
        /// </summary>
        public string Description { get; } = description;
        /// <summary>
        /// 模块
        /// </summary>
        public string? ModuleName { get; } = moduleName;
        /// <summary>
        /// 依赖
        /// </summary>
        public string[] Depends { get; } = depends ?? [];
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="description"></param>
        /// <param name="depends"></param>
        public MergeBlockAssemblyAttribute(string description, string[]? depends) : this(description, null, depends)
        {
        }
    }
}
