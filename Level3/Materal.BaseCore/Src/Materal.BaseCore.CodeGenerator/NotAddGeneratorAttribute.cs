﻿namespace Materal.BaseCore.CodeGenerator
{
    /// <summary>
    /// 不生成到添加模型
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class NotAddGeneratorAttribute : Attribute { }
}
