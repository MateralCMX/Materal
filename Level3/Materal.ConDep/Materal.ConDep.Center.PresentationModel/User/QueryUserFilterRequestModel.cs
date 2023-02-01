using Materal.Model;

namespace Materal.ConDep.Center.PresentationModel.User
{
    public class QueryUserFilterRequestModel : PageRequestModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
    }
}
