namespace Materal.Common
{
    public class PageModel : PageRequestModel
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public uint PageCount => DataCount % PageSize > 0 ? DataCount / PageSize + 1 : DataCount / PageSize;

        /// <summary>
        /// 数据总数
        /// </summary>
        public uint DataCount { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="dataCount">数据总数</param>
        public PageModel(PageRequestModel pageRequestModel, uint dataCount) : base(pageRequestModel.PageIndex, pageRequestModel.PageSize)
        {
            DataCount = dataCount;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pagingIndex">页面位序</param>
        /// <param name="pagingSize">显示数量</param>
        /// <param name="dataCount">数据总数</param>
        public PageModel(uint pagingIndex, uint pagingSize, uint dataCount) : base(pagingIndex, pagingSize)
        {
            DataCount = dataCount;
        }
    }
}
