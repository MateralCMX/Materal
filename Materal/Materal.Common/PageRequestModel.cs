namespace Materal.Common
{
    public abstract class PageRequestModel
    {
        /// <summary>
        /// 页面位序
        /// </summary>
        public uint PageIndex { get; set; } = MateralConfig.PageStartNumber;

        /// <summary>
        /// 显示数量
        /// </summary>
        public uint PageSize { get; set; } = 10;

        /// <summary>
        /// 跳过数量
        /// </summary>
        public uint Skip => (PageIndex - MateralConfig.PageStartNumber) * PageSize;

        /// <summary>
        /// 获取数量
        /// </summary>
        public uint Take => PageSize;

        /// <summary>
        /// 构造方法
        /// </summary>
        protected PageRequestModel() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        protected PageRequestModel(uint pageIndex, uint pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
