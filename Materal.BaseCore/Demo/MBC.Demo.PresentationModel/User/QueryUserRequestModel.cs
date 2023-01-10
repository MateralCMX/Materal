using Materal.BaseCore.PresentationModel;
using Materal.Model;

namespace MBC.Demo.PresentationModel.User
{
    /// <summary>
    /// 查询用户请求模型
    /// </summary>
    public class QueryUserRequestModel : PageRequestModel, IQueryRequestModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string? Account { get; set; }
    }
}
