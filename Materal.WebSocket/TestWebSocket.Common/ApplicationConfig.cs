using Microsoft.Extensions.Configuration;
using System;

namespace TestWebSocket.Common
{
    public static class ApplicationConfig
    {

        private static IConfiguration _configuration;
        private static WebSocketConfigModel _testWebSocketConfig;
        /// <summary>
        /// 配置对象
        /// </summary>
        public static IConfiguration Configuration
        {
            get
            {
                if (_configuration != null) return _configuration;
                _configuration = ConfigurationBuilder();
                return _configuration;
            }
            set => _configuration = value;
        }
        /// <summary>
        /// TestWebSocket服务器
        /// </summary>
        public static WebSocketConfigModel TestWebSocket => _testWebSocketConfig ?? (_testWebSocketConfig = new WebSocketConfigModel
        {
            CertificatePassword = Configuration["TestWebSocketServer:CertificatePassword"],
            CertificatePath = Configuration["TestWebSocketServer:CertificatePath"],
            Host = Configuration["TestWebSocketServer:Host"],
            Port = Convert.ToInt32(Configuration["TestWebSocketServer:Port"]),
            IsSsl = Convert.ToBoolean(Configuration["TestWebSocketServer:IsSsl"]),
            UserLibuv = Convert.ToBoolean(Configuration["TestWebSocketServer:UserLibuv"]),
        });
        /// <summary>
        /// 配置生成
        /// </summary>
        /// <returns></returns>
        private static IConfiguration ConfigurationBuilder()
        {
            if (_configuration != null) return _configuration;

#if DEBUG
            const string appConfigFile = "appsettings.Development.json";
#else
            const string appConfigFile = "appsettings.json";
#endif
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(appConfigFile);
            _configuration = builder.Build();
            return _configuration;
        }
    }
}
