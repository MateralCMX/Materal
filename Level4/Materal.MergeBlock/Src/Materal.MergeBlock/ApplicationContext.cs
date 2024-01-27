namespace Materal.MergeBlock
{
    /// <summary>
    /// 应用程序上下文
    /// </summary>
    public abstract class ApplicationContext(IServiceProvider serviceProvider) : IApplicationContext
    {
        /// <summary>
        /// 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; } = serviceProvider;
    }
}
