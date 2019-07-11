using Materal.Common;

namespace Materal.Model
{
    public abstract class PageRequestModel : FilterModel
    {
        /// <summary>
        /// 页面位序
        /// </summary>
        public int PageIndex { get; set; } = MateralConfig.PageStartNumber;

        /// <summary>
        /// 显示数量
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 跳过数量
        /// </summary>
        public int Skip => (PageIndex - MateralConfig.PageStartNumber) * PageSize;

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
        }
    }
}
