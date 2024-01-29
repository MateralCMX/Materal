﻿using Microsoft.Extensions.Logging;

namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock主机
    /// </summary>
    public static class MergeBlockHost
    {
        /// <summary>
        /// 日志对象
        /// </summary>
        public static ILogger? Logger { get; set; }
        /// <summary>
        /// 模块文件夹信息
        /// </summary>
        public static List<IModuleDirectoryInfo> ModuleDirectoryInfos { get; } = [];
        /// <summary>
        /// 模块信息
        /// </summary>
        public static List<IModuleInfo> ModuleInfos => ModuleDirectoryInfos.SelectMany(m => m.ModuleInfos).ToList();
    }
}