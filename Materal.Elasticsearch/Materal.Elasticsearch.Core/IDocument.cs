namespace Materal.Elasticsearch.Core
{
    /// <summary>
    /// 文档接口
    /// </summary>
    /// <typeparam name="TIdentifier"></typeparam>
    public interface IDocument<TIdentifier> where TIdentifier : struct
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        TIdentifier ID { get; set; }
        /// <summary>
        /// 获取唯一标识字符串
        /// </summary>
        /// <returns></returns>
        string GetIDString();
    }
}
