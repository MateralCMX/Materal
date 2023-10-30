using CommunityToolkit.Mvvm.Input;
using RC.Authority.DataTransmitModel.User;

namespace RCManagementTool.Pages.User
{
    /// <summary>
    /// 用户列表模型
    /// </summary>
    public partial class UserListModel : ObservableObject
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [ObservableProperty]
        private Guid _ID;
        /// <summary>
        /// 姓名
        /// </summary>
        [ObservableProperty]
        private string _name = string.Empty;
        /// <summary>
        /// 账号
        /// </summary>
        [ObservableProperty]
        private string _account = string.Empty;
        public UserListModel(UserListDTO user) => user.CopyProperties(this);
        /// <summary>
        /// 修改
        /// </summary>
        [RelayCommand]
        private void Edit()
        {

        }
        /// <summary>
        /// 删除
        /// </summary>
        [RelayCommand]
        private void Delete()
        {

        }
    }
}
