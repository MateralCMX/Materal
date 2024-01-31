namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 模块信息
    /// </summary>
    public interface IModuleInfo
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        Guid ID { get; }
        /// <summary>
        /// 模块名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 模块描述
        /// </summary>
        string Description { get; }
        /// <summary>
        /// 模块位置
        /// </summary>
        string Location { get; }
        /// <summary>
        /// 依赖模块
        /// </summary>
        string[] Depends { get; }
        /// <summary>
        /// 模块类型
        /// </summary>
        Type ModuleType { get; }
        /// <summary>
        /// 模块文件夹信息
        /// </summary>
        IModuleDirectoryInfo ModuleDirectoryInfo { get; }
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ConfigServiceBeforeAsync(IConfigServiceContext context);
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ConfigServiceAsync(IConfigServiceContext context);
        /// <summary>
        /// 配置服务后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ConfigServiceAfterAsync(IConfigServiceContext context);
        /// <summary>
        /// 应用程序初始化前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ApplicationInitBeforeAsync(IApplicationContext context);
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ApplicationInitAsync(IApplicationContext context);
        /// <summary>
        /// 应用程序初始化后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ApplicationInitAfterAsync(IApplicationContext context);
        /// <summary>
        /// 应用程序关闭前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ApplicationCloseBeforeAsync(IApplicationContext context);
        /// <summary>
        /// 应用程序关闭
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ApplicationCloseAsync(IApplicationContext context);
        /// <summary>
        /// 应用程序关闭后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ApplicationCloseAfterAsync(IApplicationContext context);
    }
    /// <summary>
    /// 模块信息
    /// </summary>
    public interface IModuleInfo<TModule> : IModuleInfo
        where TModule : IMergeBlockModule
    {
        /// <summary>
        /// 模块实例
        /// </summary>
        TModule Instance { get; }
    }
}
