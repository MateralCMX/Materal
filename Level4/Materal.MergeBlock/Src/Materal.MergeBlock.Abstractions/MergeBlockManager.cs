namespace Materal.MergeBlock.Abstractions
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
        /// <summary>
        /// 基础地址组
        /// </summary>
        public static List<Uri> BaseUris { get; internal set; } = [];
        /// <summary>
        /// 第一个基础地址
        /// </summary>
        public static Uri FirstBaseUri => BaseUris.First();
    }
}
