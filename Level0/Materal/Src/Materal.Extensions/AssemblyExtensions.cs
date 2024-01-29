﻿namespace Materal.Extensions
{
    /// <summary>
    /// 程序集扩展
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 获取所在文件夹路径
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static string GetDirectoryPath(this Assembly assembly) => Path.GetDirectoryName(assembly.Location) ?? throw new MateralException("获取所在文件夹路径");
        /// <summary>
        /// 是否包含指定特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static bool HasCustomAttribute<T>(this Assembly assembly)
            where T : Attribute => assembly.GetCustomAttribute<T>() is not null;
    }
}