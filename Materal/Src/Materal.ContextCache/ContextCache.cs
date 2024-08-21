using System.Threading.Tasks.Dataflow;

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
        private readonly ActionBlock<ContextCacheBlockModel> _handlerBlock;
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
            _handlerBlock = new(Handler);
        }
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        private static void Handler(ContextCacheBlockModel model)
        {
            if (model.RunNextStep)
            {
                if (model.IsEnd) throw new ContextCacheException("上下文缓存已结束");
                if (model.GroupInfo is null) throw new ContextCacheException("未初始化上下文缓存");
                if (model.ContextType is null) throw new ContextCacheException("上下文缓存未开始");
                model.ChangeContextAction?.Invoke(model.Context);
                ContextCacheModel contextCache = ContextCacheModel.CreateContextCacheModel(model.LastID, model.GroupInfo.ID, model.ContextType, model.Context);
                model.ContextCachePersistence.Save(contextCache);
                model.GroupInfo.ContextCacheData.Add(contextCache);
            }
            else
            {
                if (model.IsEnd) throw new ContextCacheException("上下文缓存已结束");
                if (model.GroupInfo is null) throw new ContextCacheException("未初始化上下文缓存");
                if (model.ContextType is null) throw new ContextCacheException("上下文缓存未开始");
                model.ContextCachePersistence.Remove(model.GroupInfo.ID);
            }
        }
        /// <inheritdoc/>
        public void NextStep(Action<object?>? changeContext = null)
        {
            ContextCacheBlockModel nextStepModel = new(true, _isEnd, GroupInfo, ContextType, LastID, Context, _contextCachePersistence, changeContext);
            Handler(nextStepModel);
        }
        /// <inheritdoc/>
        public void NextStep<TContext>(Action<TContext>? changeContext = null) => NextStep(context =>
        {
            if (context is not TContext tContext) return;
            changeContext?.Invoke(tContext);
        });
        /// <inheritdoc/>
        public void NextStepAsync(Action<object?>? changeContext = null)
        {
            ContextCacheBlockModel nextStepModel = new(true, _isEnd, GroupInfo, ContextType, LastID, Context, _contextCachePersistence, changeContext);
            _handlerBlock.Post(nextStepModel);
        }
        /// <inheritdoc/>
        public void NextStepAsync<TContext>(Action<TContext>? changeContext = null) => NextStepAsync(context =>
        {
            if (context is not TContext tContext) return;
            changeContext?.Invoke(tContext);
        });
        /// <inheritdoc/>
        public void End()
        {
            if (_isEnd) throw new ContextCacheException("上下文缓存已结束");
            if (GroupInfo is null) throw new ContextCacheException("未初始化上下文缓存");
            if (GroupInfo.ContextCacheData.Count <= 0) throw new ContextCacheException("未开始上下文缓存");
            Handler(new(false, _isEnd, GroupInfo, ContextType, LastID, Context, _contextCachePersistence, m =>
            {
                _isEnd = true;
            }));
            _handlerBlock.Complete();
            _handlerBlock.Completion.Wait();
        }
        /// <inheritdoc/>
        public void EndAsync()
        {
            if (_isEnd) throw new ContextCacheException("上下文缓存已结束");
            if (GroupInfo is null) throw new ContextCacheException("未初始化上下文缓存");
            if (GroupInfo.ContextCacheData.Count <= 0) throw new ContextCacheException("未开始上下文缓存");
            _handlerBlock.Post(new(false, _isEnd, GroupInfo, ContextType, LastID, Context, _contextCachePersistence, m =>
            {
                _isEnd = true;
            }));
            _handlerBlock.Complete();
        }
    }
}
