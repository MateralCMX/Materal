namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 查询模型
    /// </summary>
    public interface IQueryServiceModel
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
