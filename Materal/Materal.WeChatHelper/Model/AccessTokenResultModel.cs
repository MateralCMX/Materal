namespace Materal.WeChatHelper.Model
{
    public class AccessTokenResultModel
    {
        /// <summary>
        /// AccessToken
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public int ExpiresIn { get; set; }
    }
}
