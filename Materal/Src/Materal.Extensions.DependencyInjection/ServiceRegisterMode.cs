namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// 服务注册模式
    /// </summary>
    public enum ServiceRegisterMode
    {
        /// <summary>
        /// 尝试添加
        /// </summary>
        [Description("尝试添加")]
        TryAdd,
        /// <summary>
        /// 添加
        /// </summary>
        [Description("添加")]
        Add,
        /// <summary>
        /// 替换
        /// </summary>
        [Description("替换")]
        Replace
    }
}
