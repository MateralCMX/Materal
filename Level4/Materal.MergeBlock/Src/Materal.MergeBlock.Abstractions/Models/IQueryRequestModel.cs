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
        public long PageSize { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public long PageIndex { get; set; }
        /// <summary>
        /// 分页跳过数量
        /// </summary>
        public long PageSkip { get; }
        /// <summary>
        /// 跳过数量
        /// </summary>
        public long Skip { get; set; }
        /// <summary>
        /// 获取数量
        /// </summary>
        public long Take { get; set; }
        /// <summary>
        /// 唯一标识组
        /// </summary>
        public List<Guid>? IDs { get; set; }
        /// <summary>
        /// 最小创建时间
        /// </summary>
        public DateTime? MinCreateTime { get; set; }
        /// <summary>
        /// 最大创建时间
        /// </summary>
        public DateTime? MaxCreateTime { get; set; }
    }
}
