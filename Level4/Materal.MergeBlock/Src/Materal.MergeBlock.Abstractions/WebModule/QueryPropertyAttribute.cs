namespace Materal.MergeBlock.Abstractions.WebModule
{
    /// <summary>
    /// 目标属性特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class QueryPropertyAttribute : Attribute
    {
        /// <summary>
        /// 目标属性
        /// </summary>
        public string TargetProperty { get; private set; }
        /// <summary>
        /// 目标属性
        /// </summary>
        public string? TypeName { get; private set; }
        /// <summary>
        /// 查询属性
        /// </summary>
        /// <param name="targetProperty"></param>
        /// <param name="typeName"></param>
        public QueryPropertyAttribute(string targetProperty, string? typeName = null)
        {
            TargetProperty = targetProperty;
            TypeName = typeName;
        }
    }
}
