namespace Materal.BaseCore.HttpClient.ConfigModels
{
    /// <summary>
    /// Http客户端配置
    /// </summary>
    public class HttpClientUrlConfigModel
    {
        private string _baseUrl = "http://localhost:5000/";
        /// <summary>
        /// 主机名
        /// </summary>
        public string BaseUrl
        {
            get => _baseUrl;
            set
            {
                _baseUrl = value;
                if (!_baseUrl.EndsWith("/"))
                {
                    _baseUrl += "/";
                }
            }
        }
        /// <summary>
        /// 主机名
        /// </summary>
        public string? Suffix { get; set; }
    }
}
