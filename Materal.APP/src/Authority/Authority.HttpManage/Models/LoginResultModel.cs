namespace Authority.HttpManage.Models
{
    /// <summary>
    /// Token返回模型
    /// </summary>
    public class LoginResultModel
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public uint ExpiredTime { get; set; }
    }
}
