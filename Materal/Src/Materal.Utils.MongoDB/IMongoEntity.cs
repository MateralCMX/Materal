namespace Materal.Utils.MongoDB
{
    /// <summary>
    /// MongoDB实体对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMongoEntity<T>
        where T : struct
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        T ID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
    }
}
