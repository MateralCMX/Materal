namespace Materal.ContextCache
{
    /// <summary>
    /// 上下文缓存
    /// </summary>
    public class ContextCache : IContextCache
    {
        /// <summary>
        /// 分组信息
        /// </summary>
        public ContextCacheGroupModel? GroupInfo { get; private set; }
        /// <summary>
        /// 上下文类型
        /// </summary>
        public Type? ContextType { get; private set; }
        /// <summary>
        /// 上下文
        /// </summary>
        public object? Context { get; private set; }
        /// <summary>
        /// 最后一个ID
        /// </summary>
        private Guid LastID => GroupInfo?.ContextCacheData.LastOrDefault()?.ID ?? throw new ContextCacheException("上下文缓存未开始");
        private bool _isEnd = false;
        private readonly IContextCachePersistence _contextCachePersistence;
        /// <summary>
        /// 构造方法
        /// </summary>
        public ContextCache(ContextCacheGroupModel groupInfo, IContextCachePersistence contextCachePersistence)
        {
            _contextCachePersistence = contextCachePersistence;
            GroupInfo = groupInfo;
            if (GroupInfo is not null && GroupInfo.ContextCacheData.Count > 0)
            {
                ContextCacheModel lastData = GroupInfo.ContextCacheData.Last();
                (ContextType, Context) = lastData.GetContext();
            }
        }
        /// <summary>
        /// 开始
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="context"></param>
        /// <exception cref="ContextCacheException"></exception>
        public void Begin<TContext>(TContext? context) where TContext : class, new()
        {
            if (_isEnd) throw new ContextCacheException("上下文缓存已结束");
            if (GroupInfo is null) throw new ContextCacheException("未初始化上下文缓存");
            if (GroupInfo.ContextCacheData.Count > 0) throw new ContextCacheException("上下文缓存已开始");
            ContextCacheModel contextCache = ContextCacheModel.CreateContextCacheModel(null, GroupInfo.ID, context);
            _contextCachePersistence.Save(GroupInfo, contextCache);
            GroupInfo.ContextCacheData.Add(contextCache);
            ContextType = typeof(TContext);
            Context = context;
        }
        /// <summary>
        /// 开始
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ContextCacheException"></exception>
        public async Task BeginAsync<TContext>(TContext? context) where TContext : class, new()
        {
            if (_isEnd) throw new ContextCacheException("上下文缓存已结束");
            if (GroupInfo is null) throw new ContextCacheException("未初始化上下文缓存");
            if (GroupInfo.ContextCacheData.Count > 0) throw new ContextCacheException("上下文缓存已开始");
            ContextCacheModel contextCache = ContextCacheModel.CreateContextCacheModel(null, GroupInfo.ID, context);
            await _contextCachePersistence.SaveAsync(GroupInfo, contextCache);
            GroupInfo.ContextCacheData.Add(contextCache);
            ContextType = typeof(TContext);
            Context = context;
        }
        /// <summary>
        /// 下一个步骤
        /// </summary>
        /// <exception cref="ContextCacheException"></exception>
        public void NextStep()
        {
            if (_isEnd) throw new ContextCacheException("上下文缓存已结束");
            if (GroupInfo is null) throw new ContextCacheException("未初始化上下文缓存");
            if (ContextType is null) throw new ContextCacheException("上下文缓存未开始");
            ContextCacheModel contextCache = ContextCacheModel.CreateContextCacheModel(LastID, GroupInfo.ID, ContextType, Context);
            _contextCachePersistence.Save(contextCache);
            GroupInfo.ContextCacheData.Add(contextCache);
        }
        /// <summary>
        /// 下一个步骤
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ContextCacheException"></exception>
        public async Task NextStepAsync()
        {
            if (_isEnd) throw new ContextCacheException("上下文缓存已结束");
            if (GroupInfo is null) throw new ContextCacheException("未初始化上下文缓存");
            if (ContextType is null) throw new ContextCacheException("上下文缓存未开始");
            ContextCacheModel contextCache = ContextCacheModel.CreateContextCacheModel(LastID, GroupInfo.ID, ContextType, Context);
            await _contextCachePersistence.SaveAsync(contextCache);
            GroupInfo.ContextCacheData.Add(contextCache);
        }
        /// <summary>
        /// 下一个步骤
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="context"></param>
        /// <exception cref="ContextCacheException"></exception>
        public void NextStep<TContext>(TContext context) where TContext : class, new()
        {
            if (_isEnd) throw new ContextCacheException("上下文缓存已结束");
            if (GroupInfo is null) throw new ContextCacheException("未初始化上下文缓存");
            ContextType = typeof(TContext);
            Context = context;
            NextStep();
        }
        /// <summary>
        /// 下一个步骤
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ContextCacheException"></exception>
        public async Task NextStepAsync<TContext>(TContext context) where TContext : class, new()
        {
            if (_isEnd) throw new ContextCacheException("上下文缓存已结束");
            if (GroupInfo is null) throw new ContextCacheException("未初始化上下文缓存");
            ContextType = typeof(TContext);
            Context = context;
            await NextStepAsync();
        }
        /// <summary>
        /// 结束
        /// </summary>
        /// <exception cref="ContextCacheException"></exception>
        public void End()
        {
            if (_isEnd) throw new ContextCacheException("上下文缓存已结束");
            if (GroupInfo is null) throw new ContextCacheException("未初始化上下文缓存");
            if (GroupInfo.ContextCacheData.Count <= 0) throw new ContextCacheException("未开始上下文缓存");
            _contextCachePersistence.Remove(GroupInfo.ID);
            _isEnd = true;
        }
        /// <summary>
        /// 结束
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ContextCacheException"></exception>
        public async Task EndAsync()
        {
            if (_isEnd) throw new ContextCacheException("上下文缓存已结束");
            if (GroupInfo is null) throw new ContextCacheException("未初始化上下文缓存");
            if (GroupInfo.ContextCacheData.Count <= 0) throw new ContextCacheException("未开始上下文缓存");
            await _contextCachePersistence.RemoveAsync(GroupInfo.ID);
            _isEnd = true;
        }
        /// <inheritdoc/>
        public void Dispose()
        {
            if (GroupInfo is null) return;
            if (GroupInfo.ContextCacheData.Count > 0 && !_isEnd)
            {
                End();
            }
            ContextType = null;
            Context = null;
            GroupInfo.ContextCacheData.Clear();
            GroupInfo = null;
            GC.SuppressFinalize(this);
        }
#if NETSTANDARD2_0
#else
        /// <inheritdoc/>
        public async ValueTask DisposeAsync()
        {
            if (GroupInfo is null) return;
            if (GroupInfo.ContextCacheData.Count > 0 && !_isEnd)
            {
                await EndAsync();
            }
            ContextType = null;
            Context = null;
            GroupInfo.ContextCacheData.Clear();
            GroupInfo = null;
            GC.SuppressFinalize(this);
        }
#endif
    }
}
