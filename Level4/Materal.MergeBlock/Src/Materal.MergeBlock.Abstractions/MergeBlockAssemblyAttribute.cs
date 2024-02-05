namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock程序集标识
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class MergeBlockAssemblyAttribute(bool hasController = false) : Attribute
    {
        /// <summary>
        /// 是否包含控制器
        /// </summary>
        public bool HasController { get; } = hasController;
    }
}
