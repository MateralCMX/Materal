using System;

namespace Materal.CodeGenerator
{
    /// <summary>
    /// 不生成代码
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class NotGeneratorAttribute : Attribute { }
}
