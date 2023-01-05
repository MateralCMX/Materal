namespace RC.Core.CodeGenerator
{
    /// <summary>
    /// 查询目标生成
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class QueryTragetGeneratorAttribute : Attribute
    {
        /// <summary>
        /// 视图名称
        /// </summary>
        public string ViewName { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="viewName"></param>
        public QueryTragetGeneratorAttribute(string viewName)
        {
            ViewName = viewName;
        }
    }
}
