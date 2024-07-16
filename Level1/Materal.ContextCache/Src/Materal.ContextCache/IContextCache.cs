namespace Materal.ContextCache
{
    /// <summary>
    /// 上下文缓存
    /// </summary>
#if NETSTANDARD2_0
    public interface IContextCache : IDisposable
#else
    public interface IContextCache : IDisposable, IAsyncDisposable
#endif
    {
        /// <summary>
        /// 分组信息
        /// </summary>
        public ContextCacheGroupModel? GroupInfo { get; }
        /// <summary>
        /// 上下文类型
        /// </summary>
        public Type? ContextType { get; }
        /// <summary>
        /// 上下文
        /// </summary>
        public object? Context { get; }
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="context"></param>
        void Begin<TContext>(TContext? context)
            where TContext : class, new();
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task BeginAsync<TContext>(TContext? context)
            where TContext : class, new();
        /// <summary>
        /// 下一个步骤
        /// </summary>
        void NextStep();
        /// <summary>
        /// 下一个步骤
        /// </summary>
        /// <returns></returns>
        Task NextStepAsync();
        /// <summary>
        /// 下一个步骤
        /// </summary>
        /// <param name="context"></param>
        void NextStep<TContext>(TContext context)
            where TContext : class, new();
        /// <summary>
        /// 下一个步骤
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task NextStepAsync<TContext>(TContext context)
            where TContext : class, new();
        /// <summary>
        /// 结束
        /// </summary>
        void End();
        /// <summary>
        /// 结束
        /// </summary>
        /// <returns></returns>
        Task EndAsync();
    }
}
