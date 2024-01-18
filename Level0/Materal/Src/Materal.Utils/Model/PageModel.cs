namespace Materal.Utils.Model
{
    /// <summary>
    /// 分页模型
    /// </summary>
    public class PageModel : PageRequestModel
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public long PageCount => (int)Math.Ceiling(DataCount / (decimal)PageSize);
        /// <summary>
        /// 开始序号
        /// </summary>
        public long StartIndex => (PageIndex - PageStartNumber) * PageSize + 1;
        /// <summary>
        /// 数据总数
        /// </summary>
        public long DataCount { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public PageModel() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pagingIndex">页面位序</param>
        /// <param name="pagingSize">显示数量</param>
        public PageModel(long pagingIndex, long pagingSize) : this(pagingIndex, pagingSize, 0)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="dataCount">数据总数</param>
        public PageModel(PageRequestModel pageRequestModel, long dataCount) : this(pageRequestModel.PageIndex, pageRequestModel.PageSize, dataCount)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pagingIndex">页面位序</param>
        /// <param name="pagingSize">显示数量</param>
        /// <param name="dataCount">数据总数</param>
        public PageModel(long pagingIndex, long pagingSize, long dataCount) : base(pagingIndex, pagingSize)
        {
            DataCount = dataCount;
        }
    }
}
