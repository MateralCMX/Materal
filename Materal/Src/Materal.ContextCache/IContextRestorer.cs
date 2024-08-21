namespace Materal.ContextCache
{
    /// <summary>
    /// 上下文恢复器
    /// </summary>
    public interface IContextRestorer
    {
        /// <summary>
        /// 重新开始
        /// </summary>
        /// <param name="contextCache"></param>
        /// <returns></returns>
        Task RenewAsync(IContextCache contextCache);
    }
}
