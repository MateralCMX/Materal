namespace Materal.Common
{
    public class PageModel : PageRequestModel
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount => DataCount % PageSize > 0 ? DataCount / PageSize + 1 : DataCount / PageSize;

        /// <summary>
        /// 数据总数
        /// </summary>
        public int DataCount { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="dataCount">数据总数</param>
        public PageModel(PageRequestModel pageRequestModel, int dataCount) : base(pageRequestModel.PageIndex, pageRequestModel.PageSize)
        {
            DataCount = dataCount;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pagingIndex">页面位序</param>
        /// <param name="pagingSize">显示数量</param>
        /// <param name="dataCount">数据总数</param>
        public PageModel(int pagingIndex, int pagingSize, int dataCount) : base(pagingIndex, pagingSize)
        {
            DataCount = dataCount;
        }
    }
}
