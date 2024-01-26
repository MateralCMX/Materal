namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock管理器
    /// </summary>
    public static class MergeBlockManager
    {
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
