using Materal.BaseCore.Common;
using Materal.BaseCore.HttpClient;
using System.Timers;
using Timer = System.Timers.Timer;

namespace MBC.Core.HttpClient
{
    public static class HttpClientHelper
    {
        private static string _token = string.Empty;
        private static readonly Timer _tokenTimer;
        static HttpClientHelper()
        {
            _tokenTimer = new Timer();
            _tokenTimer.Elapsed += UpdateToken;
            UpdateToken();
        }
        /// <summary>
        /// 获得默认头部
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetDefaultHeaders()
        {
            Dictionary<string, string> result = new()
            {
                ["Content-Type"] = "application/json",
                ["Authorization"] = $"Bearer {_token}"
            };
            return result;
        }
        /// <summary>
        /// 更新Token
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void UpdateToken(object? sender, ElapsedEventArgs e) => UpdateToken();
        /// <summary>
        /// 更新Token
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private static void UpdateToken()
        {
            _tokenTimer.Stop();
            _token = MateralCoreConfig.JWTConfig.GetToken(HttpClientConfig.AppName);
            _tokenTimer.Interval = (MateralCoreConfig.JWTConfig.ExpiredTime - 60) * 1000;
            _tokenTimer.Start();
        }
    }
}
