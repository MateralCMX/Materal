using System;

namespace RC.InGenerator.Attribtues
{
    /// <summary>
    /// 不生成默认服务代码
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class NotDefaultServiceGeneratorAttribute : Attribute { }
}
