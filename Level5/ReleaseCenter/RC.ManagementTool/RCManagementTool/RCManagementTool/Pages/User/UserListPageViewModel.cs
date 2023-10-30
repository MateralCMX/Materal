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
        /// 加载数据
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task LoadDataAsync(int pageIndex)
        {
            try
            {
                LoadingMask.IsShow = true;
                UserHttpClient userHttpClient = App.ServiceProvider.GetRequiredService<UserHttpClient>();
                QueryUserRequestModel requestModel = QueryModel.CopyProperties<QueryUserRequestModel>();
                requestModel.PageIndex = pageIndex;
                (List<UserListDTO>? users, PageModel pageInfo) = await userHttpClient.GetListAsync(requestModel);
                pageInfo.CopyProperties(QueryModel);
                Users.Clear();
                if (users is not null && users.Count > 0)
                {
                    foreach (UserListDTO user in users)
                    {
                        Users.Add(new(user));
                    }
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
    }
}
