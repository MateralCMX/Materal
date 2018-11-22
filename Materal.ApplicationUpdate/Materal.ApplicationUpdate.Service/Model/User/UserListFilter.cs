using Materal.Common;

namespace Materal.ApplicationUpdate.Service.Model.User
{
    public class UserListFilter : PageRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }
    }
}
