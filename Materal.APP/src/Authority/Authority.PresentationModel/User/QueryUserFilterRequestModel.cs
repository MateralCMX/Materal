using Materal.Model;

namespace Authority.PresentationModel.User
{
    /// <summary>
    /// 查询用户过滤器请求模型
    /// </summary>
    public class QueryUserFilterRequestModel : PageRequestModel
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
