namespace Materal.BaseCore.CodeGenerator
{
    /// <summary>
    /// 查询视图
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class QueryViewAttribute(string targetName) : Attribute
    {
        /// <summary>
        /// 目标名称
        /// </summary>
        public string TargetName { get; set; } = targetName;
    }
}
