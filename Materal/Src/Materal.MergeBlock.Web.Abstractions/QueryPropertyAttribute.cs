namespace Materal.MergeBlock.Web.Abstractions
{
    /// <summary>
    /// 目标属性特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class QueryPropertyAttribute(string targetProperty, string? typeName = null) : Attribute
    {
        /// <summary>
        /// 目标属性
        /// </summary>
        public string TargetProperty { get; private set; } = targetProperty;
        /// <summary>
        /// 目标属性
        /// </summary>
        public string? TypeName { get; private set; } = typeName;
    }
}
