using CommunityToolkit.Mvvm.Input;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.HttpClient;

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
        /// <summary>
        /// 打开编辑面板命令
        /// </summary>
        public IAsyncRelayCommand<Guid> OpenEditPanelCommand { get; }
        public UserListPageViewModel UserListPage { get; }
        public UserListModel(UserListDTO user, IAsyncRelayCommand<Guid> openEditPanelCommand, UserListPageViewModel userListPage)
        {
            user.CopyProperties(this);
            OpenEditPanelCommand = openEditPanelCommand;
            UserListPage = userListPage;
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        [RelayCommand]
        private async Task ResetPasswordAsync()
        {
            UserListPage.LoadingMask.IsShow = true;
            try
            {
                UserHttpClient userHttpClient = App.ServiceProvider.GetRequiredService<UserHttpClient>();
                await userHttpClient.ResetPasswordAsync(ID);
            }
            catch (Exception ex)
            {
                RCMessageManager.SendExceptionMessage(ex);
            }
            finally
            {
                UserListPage.LoadingMask.IsShow = false;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        [RelayCommand]
        private async Task DeleteAsync()
        {
            UserListPage.LoadingMask.IsShow = true;
            try
            {
                UserHttpClient userHttpClient = App.ServiceProvider.GetRequiredService<UserHttpClient>();
                await userHttpClient.DeleteAsync(ID);
                await UserListPage.LoadDataAsync();
            }
            catch (Exception ex)
            {
                RCMessageManager.SendExceptionMessage(ex);
                UserListPage.LoadingMask.IsShow = false;
            }
        }
    }
}
