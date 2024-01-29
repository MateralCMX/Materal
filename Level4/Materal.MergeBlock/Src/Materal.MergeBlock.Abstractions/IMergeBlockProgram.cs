namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock程序
    /// </summary>
    public interface IMergeBlockProgram
    {
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        /// <param name="autoRemoveAssemblies"></param>
        /// <returns></returns>
        Task RunAsync(string[] args, bool autoRemoveAssemblies = true);
        /// <summary>
        /// 配置服务
        /// <paramref name="services"></paramref>
        /// <paramref name="configuration"></paramref>
        /// </summary>
        /// <returns></returns>
        Task ConfigModuleAsync(IServiceCollection services, ConfigurationManager configuration, bool autoRemoveAssemblies);
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <paramref name="serviceProvider"></paramref>
        /// <returns></returns>
        Task InitModuleAsync(IServiceProvider serviceProvider);
    }
}
