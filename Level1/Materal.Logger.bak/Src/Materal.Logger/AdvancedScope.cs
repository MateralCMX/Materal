namespace Materal.Logger
{
    /// <summary>
    /// 高级域
    /// </summary>
    [Serializable]
    public class AdvancedScope(string scopeName, Dictionary<string, object?>? scopeData = null)
    {
        /// <summary>
        /// 域名称
        /// </summary>
        public string ScopeName { get; set; } = scopeName;
        /// <summary>
        /// 域数据
        /// </summary>
        public Dictionary<string, object?> ScopeData { get; set; } = scopeData ?? [];
    }
}
