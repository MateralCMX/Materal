using CommunityToolkit.Mvvm.Input;
using Materal.Utils.Model;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.HttpClient;
using RC.Authority.PresentationModel.User;
using RCManagementTool.Controls;
using System.Collections.ObjectModel;

namespace RCManagementTool.Pages.User
{
    public partial class UserListPageViewModel : ObservableObject
    {
        /// <summary>
        /// 加载遮罩
        /// </summary>
        public LoadingMaskViewModel LoadingMask { get; } = new();
        /// <summary>
        /// 用户列表
        /// </summary>
        public ObservableCollection<UserListModel> Users { get; } = new();
        /// <summary>
        /// 查询请求模型
        /// </summary>
        [ObservableProperty]
        private QueryUserModel _queryModel = new();
        /// <summary>
        /// 抽屉面板
        /// </summary>
        public DrawerPanelViewModel DrawerPanel { get; } = new();
        /// <summary>
        /// 用户操作
        /// </summary>
        public UserOptionsViewModel UserOptions { get; }
        public UserListPageViewModel()
        {
            QueryModel.SearchDataCommand = LoadDataCommand;
            UserOptions = new(DrawerPanel);
            UserOptions.OnSaveSuccess += UserOptions_OnSaveSuccess;
        }
        /// <summary>
        /// 保存成功
        /// </summary>
        private async Task UserOptions_OnSaveSuccess() => await LoadDataAsync();
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task LoadDataAsync(int? pageIndex = null)
        {
            try
            {
                LoadingMask.IsShow = true;
                UserHttpClient userHttpClient = App.ServiceProvider.GetRequiredService<UserHttpClient>();
                QueryUserRequestModel requestModel = QueryModel.CopyProperties<QueryUserRequestModel>();
                if (pageIndex is not null && pageIndex.Value > 0)
                {
                    requestModel.PageIndex = pageIndex.Value;
                }
                (List<UserListDTO>? users, PageModel pageInfo) = await userHttpClient.GetListAsync(requestModel);
                pageInfo.CopyProperties(QueryModel);
                Users.Clear();
                if (users is null || users.Count <= 0) return;
                foreach (UserListDTO user in users)
                {
                    Users.Add(new(user, OpenEditUserPanelCommand, this));
                }
            }
            catch (Exception ex)
            {
                RCMessageManager.SendExceptionMessage(ex);
            }
            finally
            {
                LoadingMask.IsShow = false;
            }
        }
        /// <summary>
        /// 打开添加用户面板
        /// </summary>
        [RelayCommand]
        private async Task OpenAddUserPanelAsync() => await OpenUserOptionsPanelAsync(null);
        /// <summary>
        /// 打开编辑用户面板
        /// </summary>
        [RelayCommand]
        private async Task OpenEditUserPanelAsync(Guid id) => await OpenUserOptionsPanelAsync(id);
        /// <summary>
        /// 打开用户操作面板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task OpenUserOptionsPanelAsync(Guid? id)
        {
            await UserOptions.LoadUserInfoAsync(id);
            DrawerPanel.Visibility = Visibility.Visible;
        }
    }
}
