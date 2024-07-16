namespace Materal.ContextCache
{
    /// <summary>
    /// 上下文缓存持久化
    /// </summary>
    public interface IContextCachePersistence
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="groupModel"></param>
        /// <param name="model"></param>
        void Save(ContextCacheGroupModel groupModel, ContextCacheModel model);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="groupModel"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task SaveAsync(ContextCacheGroupModel groupModel, ContextCacheModel model);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        void Save(ContextCacheModel model);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task SaveAsync(ContextCacheModel model);
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="groupID"></param>
        void Remove(Guid groupID);
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        Task RemoveAsync(Guid groupID);
        /// <summary>
        /// 获得所有分组信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<ContextCacheGroupModel> GetAllGroupInfo();
    }
}
