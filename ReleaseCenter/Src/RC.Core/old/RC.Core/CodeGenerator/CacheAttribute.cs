﻿namespace RC.Core.CodeGenerator
{
    /// <summary>
    /// 使用缓存仓储
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class CacheAttribute : Attribute { }
}