using RCManagementTool.Models;

namespace RCManagementTool.Pages.User
{
    /// <summary>
    /// 查询模型
    /// </summary>
    public partial class QueryUserModel : BaseQueryModel
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
    }
}
