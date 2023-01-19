using Materal.BaseCore.Services;
using Materal.Model;

namespace MBC.Demo.Services.Models.User
{
    /// <summary>
    /// 查询用户模型
    /// </summary>
    public class QueryUserModel : PageRequestModel, IQueryServiceModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Contains]
        public string? Name { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [Contains]
        public string? Account { get; set; }
    }
}
