namespace Materal.BaseCore.PresentationModel
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
    }
}
