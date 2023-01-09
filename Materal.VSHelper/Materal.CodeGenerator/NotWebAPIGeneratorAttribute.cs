using System;

namespace Materal.CodeGenerator
{
    /// <summary>
    /// 不生成WebAPI代码
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class NotWebAPIGeneratorAttribute : Attribute { }
}
