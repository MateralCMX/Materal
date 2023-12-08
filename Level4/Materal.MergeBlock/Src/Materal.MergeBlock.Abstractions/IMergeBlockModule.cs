namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock模块
    /// </summary>
    public interface IMergeBlockModule
    {
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnConfigServiceBeforeAsync(IConfigServiceContext context);
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnConfigServiceAsync(IConfigServiceContext context);
        /// <summary>
        /// 配置服务后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnConfigServiceAfterAsync(IConfigServiceContext context);
        /// <summary>
        /// 应用程序初始化前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnApplicationInitBeforeAsync(IApplicationContext context);
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnApplicationInitAsync(IApplicationContext context);
        /// <summary>
        /// 应用程序初始化后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnApplicationInitAfterAsync(IApplicationContext context);
    }
}
