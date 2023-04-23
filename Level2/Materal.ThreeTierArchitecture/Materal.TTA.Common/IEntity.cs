namespace Materal.TTA.Common
{
    /// <summary>
    /// 实体接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntity<out T>
        where T : struct
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        T ID { get; }
    }
}
