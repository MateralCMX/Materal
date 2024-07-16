using Newtonsoft.Json;

namespace Materal.ContextCache
{
    /// <summary>
    /// 上下文缓存模型
    /// </summary>
    public class ContextCacheModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid GroupID { get; set; }
        /// <summary>
        /// 上一个唯一标识
        /// </summary>
        public Guid? UpID { get; set; }
        /// <summary>
        /// 上下文类型
        /// </summary>
        public string ContextType { get; set; } = string.Empty;
        /// <summary>
        /// 上下文数据
        /// </summary>
        public string? ContextData { get; set; }
        /// <summary>
        /// 创建上下文缓存模型
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="upID"></param>
        /// <param name="groupID"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ContextCacheException"></exception>
        public static ContextCacheModel CreateContextCacheModel<TContext>(Guid? upID, Guid groupID, TContext? context)
            where TContext : class, new() => CreateContextCacheModel(upID, groupID, typeof(TContext), context);
        /// <summary>
        /// 创建上下文缓存模型
        /// </summary>
        /// <param name="upID"></param>
        /// <param name="groupID"></param>
        /// <param name="contextType"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ContextCacheException"></exception>
        public static ContextCacheModel CreateContextCacheModel(Guid? upID, Guid groupID, Type contextType, object? context) => new()
        {
            ID = Guid.NewGuid(),
            UpID = upID,
            GroupID = groupID,
            ContextType = contextType.FullName ?? throw new ContextCacheException("获取恢复器类型名称失败"),
            ContextData = JsonConvert.SerializeObject(context)
        };
        /// <summary>
        /// 获得上下文组
        /// </summary>
        /// <returns></returns>
        public (Type, object?) GetContext()
        {
            Type contextType = ContextType.GetTypeByTypeFullName() ?? throw new ContextCacheException($"获取上下文类型{ContextType}失败");
            if (ContextData is null || string.IsNullOrWhiteSpace(ContextData)) return (contextType, null);
            object? context = JsonConvert.DeserializeObject(ContextData, contextType);
            return (contextType, context);
        }
    }
}
