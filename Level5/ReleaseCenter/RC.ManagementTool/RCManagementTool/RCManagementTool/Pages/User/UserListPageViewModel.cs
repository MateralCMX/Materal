using Materal.Utils.Model;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.HttpClient;
using RC.Authority.PresentationModel.User;
using System.Collections.ObjectModel;

namespace RCManagementTool.Pages.User
{
    public partial class UserListPageViewModel : ObservableObject
    {
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
        /// <param name="pageIndex"></param>
        public void LoadData(int pageIndex = 1)
        {
            if (QueryModel.PageCount < pageIndex) return;
            QueryModel.PageIndex = pageIndex;
            OpenLoadingMaskModel openLoadingMaskModel = new("正在查询");
            openLoadingMaskModel.OnOpenAsync += LoadDataAsync;
            RCMessageManager.SendOpenLoadingMaskMessage(openLoadingMaskModel);
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns></returns>
        public async Task LoadDataAsync()
        {
            try
            {
                UserHttpClient userHttpClient = App.ServiceProvider.GetRequiredService<UserHttpClient>();
                QueryUserRequestModel requestModel = QueryModel.CopyProperties<QueryUserRequestModel>();
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
        }
    }
}
