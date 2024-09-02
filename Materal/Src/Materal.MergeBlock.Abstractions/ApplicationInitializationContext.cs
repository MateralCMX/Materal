namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 应用初始化上下文
    /// </summary>
    public class ApplicationInitializationContext(IServiceProvider serviceProvider)
    {
        /// <summary>
        /// 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; } = serviceProvider;
    }
}
