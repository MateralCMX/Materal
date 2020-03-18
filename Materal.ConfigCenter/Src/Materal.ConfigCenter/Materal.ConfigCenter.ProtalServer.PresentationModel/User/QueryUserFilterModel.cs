using Materal.Model;

namespace Materal.ConfigCenter.ProtalServer.PresentationModel.User
{
    public class QueryUserFilterModel : PageRequestModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Equal]
        public string Account { get; set; }
    }
}
