using System;

namespace RC.InGenerator.Attribtues
{
    /// <summary>
    /// 视图服务
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
