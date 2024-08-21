namespace Materal.ContextCache
{
    internal class ContextCacheBlockModel(bool runNextStep, bool isEnd, ContextCacheGroupModel? groupInfo, Type? contextType, Guid lastID, object? context, IContextCachePersistence contextCachePersistence, Action<object?>? changeContextAction)
    {
        public bool RunNextStep { get; } = runNextStep;
        public bool IsEnd { get; } = isEnd;
        public ContextCacheGroupModel? GroupInfo { get; } = groupInfo;
        public Type? ContextType { get; } = contextType;
        public Guid LastID { get; } = lastID;
        public object? Context { get; } = context;
        public IContextCachePersistence ContextCachePersistence { get; } = contextCachePersistence;
        public Action<object?>? ChangeContextAction { get; } = changeContextAction;
    }
}
