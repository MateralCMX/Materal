namespace Materal.Gateway.Services.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OcelotTokenModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string token_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scope { get; set; }
    }
}
