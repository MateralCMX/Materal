namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 依赖关系特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DependsOnAttribute(params Type[] dependedTypes) : Attribute
    {
        /// <summary>
        /// 依赖的类型
        /// </summary>
        public Type[] DependedTypes { get; } = dependedTypes;
    }
}
