using Newtonsoft.Json;

namespace Materal.ContextCache
{
    /// <summary>
    /// 上下文缓存分组模型
    /// </summary>
    public class ContextCacheGroupModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 恢复器类型
        /// </summary>
        public string RestorerTypeName { get => RestorerType.FullName ?? throw new ContextCacheException("获取恢复器类型名称失败"); set => RestorerType = value.GetTypeByTypeFullName<IContextRestorer>() ?? throw new ContextCacheException("获取恢复器类型失败"); }
        private Type? _restorerType;
        /// <summary>
        /// 恢复器类型
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public Type RestorerType { get => _restorerType ?? throw new ContextCacheException("尚未设置恢复器类型"); set => _restorerType = value; }
        /// <summary>
        /// 上下文缓存数据
        /// </summary>
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public List<ContextCacheModel> ContextCacheData { get; set; } = [];
        /// <summary>
        /// 创建上下文缓存
        /// </summary>
        /// <returns></returns>
        public IContextCache CreateContextCache(IContextCachePersistence contextCachePersistence) => new ContextCache(this, contextCachePersistence);
        /// <summary>
        /// 创建上下文恢复器
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public IContextRestorer CreateContextRestorer(IServiceProvider serviceProvider) => RestorerType.Instantiation<IContextRestorer>(serviceProvider);
    }
}
