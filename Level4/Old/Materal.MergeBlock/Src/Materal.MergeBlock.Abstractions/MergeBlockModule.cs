namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock模块
    /// </summary>
    public abstract class MergeBlockModule : IMergeBlockModule
    {
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnConfigServiceBeforeAsync(IConfigServiceContext context) { await Task.CompletedTask; }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnConfigServiceAsync(IConfigServiceContext context) { await Task.CompletedTask; }
        /// <summary>
        /// 配置服务后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnConfigServiceAfterAsync(IConfigServiceContext context) { await Task.CompletedTask; }
        /// <summary>
        /// 应用程序初始化前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationInitBeforeAsync(IApplicationContext context) { await Task.CompletedTask; }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationInitAsync(IApplicationContext context) { await Task.CompletedTask; }
        /// <summary>
        /// 应用程序初始化后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationInitAfterAsync(IApplicationContext context) { await Task.CompletedTask; }
    }
}
