using System.ComponentModel;

namespace Materal.WeChatHelper.Model.Basis.Result
{
    /// <summary>
    /// AccessToken返回模型
    /// </summary>
    public class AccessTokenResultModel
    {
        /// <summary>
        /// AccessToken
        /// </summary>
        [Description("AccessToken")]
        public string AccessToken { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        [Description("有效时间")]
        public int ExpiresIn { get; set; }
    }
}
