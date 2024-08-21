namespace Materal.Utils.Wechat.Model.OfficialAccount.Result
{
    /// <summary>
    /// 网页授权接口调用凭证结果模型
    /// </summary>
    public class WebAssessTokenResultModel
    {
        /// <summary>
        /// OpenID
        /// </summary>
        public string OpenID { get; set; } = string.Empty;
        /// <summary>
        /// 网页授权接口调用凭证
        /// </summary>
        public string WebAssessToken { get; set; } = string.Empty;
        /// <summary>
        /// 有效时间
        /// </summary>
        public int ExpiresIn { get; set; }
        /// <summary>
        /// 刷新Token
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;
        /// <summary>
        /// 有效域
        /// </summary>
        public string Scope { get; set; } = string.Empty;
    }
}
