namespace RC.CodeGenerator
{
    /// <summary>
    /// 查询目标生成
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class QueryTragetGeneratorAttribute : Attribute
    {
        public string ViewName { get; set; }

        public QueryTragetGeneratorAttribute(string viewName)
        {
            ViewName = viewName;
        }
    }
}
