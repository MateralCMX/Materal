namespace Materal.MergeBlock
{
    /// <summary>
    /// 插件
    /// </summary>
    public interface IPlugin : IPluginBase
    {
        /// <summary>
        /// 加载上下文
        /// </summary>
        [JsonIgnore]
        PluginLoadContext? LoadContext { get; }
        /// <summary>
        /// 确保插件
        /// </summary>
        void EnsurePlugin();
    }
}
