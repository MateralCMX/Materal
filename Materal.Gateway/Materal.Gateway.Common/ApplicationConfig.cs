using Materal.Gateway.Common.ConfigModels;
using Microsoft.Extensions.Configuration;

namespace Materal.Gateway.Common
{
    public static class ApplicationConfig
    {
        public static IConfiguration Config;
        public static string BaseUrl { get; set; } = "http://127.0.0.1:5000";
        private static OcelotConfigModel _ocelotConfig;
        /// <summary>
        /// JWT配置
        /// </summary>
        public static OcelotConfigModel OcelotConfig => _ocelotConfig ??= new OcelotConfigModel();
        private static NLogConfigModel _nLogConfig;
        /// <summary>
        /// NLog配置
        /// </summary>
        public static NLogConfigModel NLogConfig => _nLogConfig ??= new NLogConfigModel();
        private static AuthorizationConfig _authorizationConfig;
        /// <summary>
        /// 授权配置
        /// </summary>
        public static AuthorizationConfig AuthorizationConfig => _authorizationConfig ??= new AuthorizationConfig();

        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static void Init(string[] args)
        {
            bool isVirtual = false;
            foreach (string arg in args)
            {
                string[] tempArgs = arg.Split('=');
                if (tempArgs.Length != 2) continue;
                string argName = tempArgs[0];
                string argValue = tempArgs[1];
                switch (argName)
                {
                    case "--urls":
                        if (!isVirtual)
                        {
                            BaseUrl = argValue;
                        }
                        break;
                    case "--HostAddress":
                        isVirtual = true;
                        BaseUrl = argValue;
                        break;
                }
            }
        }
        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static void Init(IConfiguration configuration)
        {
            Config = configuration;
        }
    }
}
