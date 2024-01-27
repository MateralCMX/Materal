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
        /// <returns></returns>
        Task RunAsync(string[] args);
        /// <summary>
        /// 配置服务
        /// <paramref name="services"></paramref>
        /// <paramref name="configuration"></paramref>
        /// </summary>
        /// <returns></returns>
        Task ConfigModuleAsync(IServiceCollection services, ConfigurationManager configuration);
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <returns></returns>
        Task InitModuleAsync();
    }
}
