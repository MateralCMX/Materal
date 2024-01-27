namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 应用程序上下文
    /// </summary>
    public interface IApplicationContext
    {
        /// <summary>
        /// 服务供应者
        /// </summary>
        IServiceProvider ServiceProvider { get; }
    }
}
