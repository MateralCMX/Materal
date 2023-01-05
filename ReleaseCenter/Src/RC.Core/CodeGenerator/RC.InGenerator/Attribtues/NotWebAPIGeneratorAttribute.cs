﻿using System;

namespace RC.InGenerator.Attribtues
{
    /// <summary>
    /// 不生成WebAPI代码
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class NotWebAPIGeneratorAttribute : Attribute { }
}