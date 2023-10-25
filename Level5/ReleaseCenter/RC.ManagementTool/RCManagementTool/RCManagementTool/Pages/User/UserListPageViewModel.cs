using Materal.Utils.Model;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.HttpClient;
using RC.Authority.PresentationModel.User;

namespace RCManagementTool.Pages.User
{
    public partial class UserListPageViewModel
    {
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns></returns>
        public async Task LoadDataAsync()
        {
            try
            {
                UserHttpClient userHttpClient = App.ServiceProvider.GetRequiredService<UserHttpClient>();
                (List<UserListDTO>? users, PageModel pageInfo) = await userHttpClient.GetListAsync(new QueryUserRequestModel()
                {
                    PageIndex = 1,
                    PageSize = 10
                });
            }
            catch (Exception ex)
            {
                RCMessageManager.SendExceptionMessage(ex);
            }
        }
    }
}
