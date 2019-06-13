using Materal.Common;
using System;
namespace WeChatService.PresentationModel.Application.Request
{
    /// <summary>
    /// 应用查询请求模型
    /// </summary>
    public class QueryApplicationFilterRequestModel : PageRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public Guid? UserID { get; set; }
        /// <summary>
        /// AppID
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// WeChatToken
        /// </summary>
        public string WeChatToken { get; set; }
    }
}
