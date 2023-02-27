using Materal.BaseCore.Common;
using Materal.BaseCore.HttpClient;
using System.Timers;
using Timer = System.Timers.Timer;

namespace RC.Core.HttpClient
{
    public static class HttpClientHelper
    {
        private static string? _token;
        private static readonly Timer _tokenTimer;
        static HttpClientHelper()
        {
            _tokenTimer = new Timer();
            _tokenTimer.Elapsed += UpdateToken;
            UpdateToken();
        }
        public static Func<string, string, string>? GetUrl { get; set; }
        /// <summary>
        /// 获得默认头部
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetDefaultHeaders()
        {
            Dictionary<string, string> result = new();
            string? token = GetToken();
            if (!string.IsNullOrWhiteSpace(token))
            {
                result.Add("Authorization", $"Bearer {token}");
            }
            return result;
        }
        /// <summary>
        /// 关闭自动获取Token
        /// </summary>
        public static void CloseAutoToken()
        {
            _token = null;
            _tokenTimer.Stop();
        }
        /// <summary>
        /// 获取Token
        /// </summary>
        public static Func<string?> GetToken { get; set; } = () => _token;
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
            try
            {
                _token = MateralCoreConfig.JWTConfig.GetToken(HttpClientConfig.AppName);
                _tokenTimer.Interval = (MateralCoreConfig.JWTConfig.ExpiredTime - 60) * 1000;
                _tokenTimer.Start();
            }
            catch
            {
            }
        }
    }
}
