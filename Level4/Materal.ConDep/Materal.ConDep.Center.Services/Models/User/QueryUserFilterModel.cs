using Materal.Model;

namespace Materal.ConDep.Center.Services.Models.User
{
    public class QueryUserFilterModel : PageRequestModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Contains]
        public string Name { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [Equal]
        public string Account { get; set; }
    }
}
