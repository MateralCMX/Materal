namespace Materal.MergeBlock.Abstractions.Models
{
    /// <summary>
    /// 查询模型
    /// </summary>
    public interface IQueryServiceModel
    {
        /// <summary>
        /// 每页数量
        /// </summary>
        public long PageSize { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public long PageIndex { get; set; }
    }
}
