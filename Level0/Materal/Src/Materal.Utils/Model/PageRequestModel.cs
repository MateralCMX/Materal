namespace Materal.Utils.Model
{
    /// <summary>
    /// 分页请求模型
    /// </summary>
    public abstract class PageRequestModel : FilterModel
    {
        private long pageSize = 10;
        /// <summary>
        /// 起始页码
        /// </summary>
        public static long PageStartNumber { get; set; } = 1;
        /// <summary>
        /// 页面位序
        /// </summary>
        public long PageIndex { get; set; } = PageStartNumber;
        /// <summary>
        /// 显示数量
        /// </summary>
        public long PageSize
        {
            get => pageSize > 0 ? pageSize : 1;
            set => pageSize = value;
        }
        /// <summary>
        /// 跳过数量
        /// </summary>
        public long Skip => (PageIndex - PageStartNumber) * PageSize;
        /// <summary>
        /// 获取数量
        /// </summary>
        public long Take => PageSize;
        /// <summary>
        /// 构造方法
        /// </summary>
        protected PageRequestModel() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        protected PageRequestModel(long pageIndex, long pageSize)
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
