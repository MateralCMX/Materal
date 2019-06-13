using System;
using Materal.Common;
namespace WeChatService.Service.Model.Application
{
    /// <summary>
    /// 应用查询模型
    /// </summary>
    public class QueryApplicationFilterModel : PageRequestModel
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
