namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock模块
    /// </summary>
    public interface IMergeBlockModule<TConfigServiceContext, TApplicationContext>
        where TConfigServiceContext : IConfigServiceContext
        where TApplicationContext : IApplicationContext
    {
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnConfigServiceBeforeAsync(TConfigServiceContext context);
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnConfigServiceAsync(TConfigServiceContext context);
        /// <summary>
        /// 配置服务后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnConfigServiceAfterAsync(TConfigServiceContext context);
        /// <summary>
        /// 应用程序初始化前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnApplicationInitBeforeAsync(TApplicationContext context);
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnApplicationInitAsync(TApplicationContext context);
        /// <summary>
        /// 应用程序初始化后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnApplicationInitAfterAsync(TApplicationContext context);
        /// <summary>
        /// 应用程序结束前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnApplicationCloseBeforeAsync(TApplicationContext context);
        /// <summary>
        /// 应用程序结束
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnApplicationCloseAsync(TApplicationContext context);
        /// <summary>
        /// 应用程序结束后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnApplicationCloseAfterAsync(TApplicationContext context);
    }
    /// <summary>
    /// MergeBlock普通模块
    /// </summary>
    public interface IMergeBlockModule : IMergeBlockModule<IConfigServiceContext, IApplicationContext>
    {
        /// <summary>
        /// 模块描述
        /// </summary>
        string Description { get; }
        /// <summary>
        /// 模块
        /// </summary>
        string ModuleName { get; }
        /// <summary>
        /// 依赖
        /// </summary>
        string[] Depends { get; }
    }
}
