namespace Materal.BaseCore.CodeGenerator
{
    /// <summary>
    /// 查询视图
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class QueryViewAttribute : Attribute
    {
        /// <summary>
        /// 目标名称
        /// </summary>
        public string TargetName { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="targetName"></param>
        public QueryViewAttribute(string targetName)
        {
            TargetName = targetName;
        }
    }
}
