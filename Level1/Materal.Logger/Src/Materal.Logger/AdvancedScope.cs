namespace Materal.Logger
{
    /// <summary>
    /// 高级域
    /// </summary>
    [Serializable]
    public class AdvancedScope
    {
        /// <summary>
        /// 域名称
        /// </summary>
        public string ScopeName { get; set; }
        /// <summary>
        /// 域数据
        /// </summary>
        public Dictionary<string, string>? ScopeData { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="scopeName"></param>
        /// <param name="scopeData"></param>
        public AdvancedScope(string scopeName, Dictionary<string, string>? scopeData = null)
        {
            ScopeName = scopeName;
            ScopeData = scopeData;
        }
    }
}
