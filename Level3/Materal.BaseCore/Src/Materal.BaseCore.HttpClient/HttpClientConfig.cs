using Materal.BaseCore.Common;
using Materal.BaseCore.HttpClient.ConfigModels;

namespace Materal.BaseCore.HttpClient
{
    public class HttpClientConfig
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public static string AppName { get; set; } = string.Empty;
        private static HttpClientUrlConfigModel? _httpClientConfig;
        public static HttpClientUrlConfigModel HttpClienUrltConfig
        {
            get
            {
                if (_httpClientConfig != null) return _httpClientConfig;
                _httpClientConfig = MateralCoreConfig.GetValueObject<HttpClientUrlConfigModel>(nameof(HttpClienUrltConfig));
                return _httpClientConfig;
            }
        }
    }
}
