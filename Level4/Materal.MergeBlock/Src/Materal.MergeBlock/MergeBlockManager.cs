namespace Materal.MergeBlock
{
    /// <summary>
    /// MergeBlock管理器
    /// </summary>
    public static class MergeBlockManager
    {
        private static IServiceProvider? _serviceProvider;
        /// <summary>
        /// 服务容器
        /// </summary>
        public static IServiceProvider ServiceProvider { get => _serviceProvider ?? throw new MergeBlockException("获取容器失败"); internal set => _serviceProvider = value; }
    }
}
