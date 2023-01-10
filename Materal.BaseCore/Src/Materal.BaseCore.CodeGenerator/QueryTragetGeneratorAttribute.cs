namespace Materal.BaseCore.CodeGenerator
{
    /// <summary>
    /// 查询目标生成
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class QueryTragetGeneratorAttribute : Attribute
    {
        /// <summary>
        /// 目标名称
        /// </summary>
        public string TargetName { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="targetName"></param>
        public QueryTragetGeneratorAttribute(string targetName)
        {
            TargetName = targetName;
        }
    }
}
