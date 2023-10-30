namespace RCManagementTool.Models
{
    /// <summary>
    /// 基础查询模型
    /// </summary>
    public partial class BaseQueryModel : ObservableObject
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        [ObservableProperty]
        private int _pageIndex = 1;
        /// <summary>
        /// 每页显示数量
        /// </summary>
        [ObservableProperty]
        private int _pageSize = 10;
        /// <summary>
        /// 数据总数
        /// </summary>
        [ObservableProperty]
        private int _dataCount = int.MaxValue;
        /// <summary>
        /// 页数
        /// </summary>
        [ObservableProperty]
        private int _pageCount = int.MaxValue;
    }
}
