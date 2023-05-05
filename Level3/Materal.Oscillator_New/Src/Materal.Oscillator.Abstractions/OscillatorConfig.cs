using Microsoft.Extensions.Configuration;

namespace Materal.Oscillator.Abstractions
{
    /// <summary>
    /// 调度器配置
    /// </summary>
    public static class OscillatorConfig
    {
        private static IConfiguration? _configuration;
        private const string BaseConfigRootName = "MateralOscillator";
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configuration"></param>
        public static void Init(IConfiguration? configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// 获得真实Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetTrueKey(string key) => $"{BaseConfigRootName}:{key}";
        private static int? _maxConcurrency;
        /// <summary>
        /// 最大并发数
        /// </summary>
        public static int MaxConcurrency
        {
            get
            {
                if (_maxConcurrency != null) return _maxConcurrency.Value;
                string? enableAsyncString = _configuration?.GetValue(GetTrueKey(nameof(MaxConcurrency)));
                _maxConcurrency = enableAsyncString == null || string.IsNullOrWhiteSpace(enableAsyncString) || !enableAsyncString.IsNumber() ? 5 : Convert.ToInt32(enableAsyncString);
                return _maxConcurrency.Value;
            }
            set => _maxConcurrency = value;
        }
    }
}
