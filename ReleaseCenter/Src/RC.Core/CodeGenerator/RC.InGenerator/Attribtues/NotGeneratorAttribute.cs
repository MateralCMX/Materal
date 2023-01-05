using System;

namespace RC.InGenerator.Attribtues
{
    /// <summary>
    /// 不生成代码
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class NotGeneratorAttribute : Attribute { }
}
