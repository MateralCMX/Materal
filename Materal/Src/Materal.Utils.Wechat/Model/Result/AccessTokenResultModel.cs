namespace Materal.Utils.Wechat.Model.Result
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
        public string AccessToken { get; set; } = string.Empty;
        /// <summary>
        /// 有效时间
        /// </summary>
        [Description("有效时间")]
        public int ExpiresIn { get; set; }
    }
}
