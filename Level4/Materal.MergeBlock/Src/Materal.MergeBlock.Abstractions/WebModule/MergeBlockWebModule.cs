﻿namespace Materal.MergeBlock.Abstractions.WebModule
{
    /// <summary>
    /// MergeBlock控制台模块
    /// </summary>
    public abstract class MergeBlockWebModule(string description, string? moduleName = null, string[]? depends = null) : MergeBlockModule(description, moduleName, depends), IMergeBlockWebModule
    {
        /// <summary>
        /// 应用程序初始化之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationInitAfterAsync(IWebApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationInitAsync(IWebApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 应用程序初始化之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationInitBeforeAsync(IWebApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnConfigServiceAfterAsync(IWebConfigServiceContext context) => await Task.CompletedTask;
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnConfigServiceAsync(IWebConfigServiceContext context) => await Task.CompletedTask;
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnConfigServiceBeforeAsync(IWebConfigServiceContext context) => await Task.CompletedTask;
        /// <summary>
        /// 应用程序结束之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationCloseBeforeAsync(IWebApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 应用程序结束
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationCloseAsync(IWebApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 应用程序结束之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationCloseAfterAsync(IWebApplicationContext context) => await Task.CompletedTask;
    }
}
