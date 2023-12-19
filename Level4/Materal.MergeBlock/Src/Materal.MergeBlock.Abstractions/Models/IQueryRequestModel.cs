namespace Materal.MergeBlock.Abstractions.Models
{
    /// <summary>
    /// 查询请求模型
    /// </summary>
    public interface IQueryRequestModel
    {
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
    }
}
