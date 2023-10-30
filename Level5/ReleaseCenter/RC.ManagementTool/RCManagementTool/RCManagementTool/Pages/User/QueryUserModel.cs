namespace RCManagementTool.Pages.User
{
    /// <summary>
    /// 查询模型
    /// </summary>
    public partial class QueryUserModel : ObservableObject
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [ObservableProperty]
        private string? _name;
        /// <summary>
        /// 账号
        /// </summary>
        [ObservableProperty]
        private string? _account;
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
