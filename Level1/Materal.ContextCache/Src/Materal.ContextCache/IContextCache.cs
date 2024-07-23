namespace Materal.ContextCache
{
    /// <summary>
    /// 上下文缓存
    /// </summary>
    public interface IContextCache
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
        /// 下一个步骤
        /// </summary>
        void NextStep(Action<object?>? changeContext = null);
        /// <summary>
        /// 下一个步骤
        /// </summary>
        void NextStep<TContext>(Action<TContext>? changeContext = null);
        /// <summary>
        /// 下一个步骤
        /// </summary>
        /// <returns></returns>
        void NextStepAsync(Action<object?>? changeContext = null);
        /// <summary>
        /// 下一个步骤
        /// </summary>
        /// <returns></returns>
        void NextStepAsync<TContext>(Action<TContext>? changeContext = null);
        /// <summary>
        /// 结束
        /// </summary>
        void End();
        /// <summary>
        /// 结束
        /// </summary>
        void EndAsync();
    }
}
