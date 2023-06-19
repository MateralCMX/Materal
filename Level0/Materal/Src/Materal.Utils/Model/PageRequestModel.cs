namespace Materal.Utils.Model
{
    /// <summary>
    /// 分页请求模型
    /// </summary>
    public abstract class PageRequestModel : FilterModel
    {
        /// <summary>
        /// 起始页码
        /// </summary>
        public static int PageStartNumber { get; set; } = 1;
        /// <summary>
        /// 页面位序
        /// </summary>
        public int PageIndex { get; set; } = PageStartNumber;
        /// <summary>
        /// 显示数量
        /// </summary>
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// 跳过数量
        /// </summary>
        public int Skip => (PageIndex - PageStartNumber) * PageSize;
        /// <summary>
        /// 获取数量
        /// </summary>
        public int Take => PageSize;
        /// <summary>
        /// 构造方法
        /// </summary>
        protected PageRequestModel() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        protected PageRequestModel(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            if (PageIndex < PageStartNumber)
            {
                PageIndex = PageStartNumber;
            }
            if (PageSize < 0)
            {
                PageSize = 1;
            }
        }
    }
}
