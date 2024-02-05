using RC.EnvironmentServer.Abstractions.DTO.ConfigurationItem;
using RC.EnvironmentServer.Abstractions.RequestModel.ConfigurationItem;
using System.Text.Json;

namespace RC.ConfigClient
{
    /// <summary>
    /// 配置提供者
    /// </summary>
    public class MateralConfigurationProvider : ConfigurationProvider
    {
        private readonly string _namespaceName;
        private readonly string _configUrl;
        private readonly string _projectName;
        private readonly int _reloadSecondInterval;
        private readonly TimeSpan _reloadInterval;
        private readonly Timer reloadTimer;
        private ILogger<MateralConfigurationProvider>? _logger;
        private string? _configMd5;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="namespaceName"></param>
        /// <param name="configUrl"></param>
        /// <param name="projectName"></param>
        /// <param name="reloadSecondInterval"></param>
        public MateralConfigurationProvider(string namespaceName, string configUrl, string projectName, int reloadSecondInterval)
        {
            _namespaceName = namespaceName;
            _configUrl = configUrl;
            _projectName = projectName;
            _reloadSecondInterval = reloadSecondInterval;
            _configMd5 = LoadConfig();
            reloadTimer = new Timer(AutoLoad);
            if (_reloadSecondInterval > 0)
            {
                _reloadInterval = TimeSpan.FromSeconds(_reloadSecondInterval);
                StartReloadTimer();
            }
            else
            {
                _reloadInterval = Timeout.InfiniteTimeSpan;
            }
        }
        /// <summary>
        /// 加载配置
        /// </summary>
        public override void Load()
        {
            try
            {
                string? configMD5 = LoadConfig();
                if(configMD5 != _configMd5)
                {
                    _configMd5 = configMD5;
                    OnReload();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                _logger ??= MateralServices.GetService<ILogger<MateralConfigurationProvider>>();
                if (_logger != null)
                {
                    _logger?.LogWarning(ex, $"重读配置失败{_projectName}->{_namespaceName}");
                }
                else
                {
                    ConsoleQueue.WriteLine(ex, ConsoleColor.DarkYellow);
                }
            }
        }
        /// <summary>
        /// 加载配置
        /// </summary>
        private string? LoadConfig()
        {
            ConfigurationItemHttpClient configRepository = new(_configUrl);
            ICollection<ConfigurationItemListDTO>? configurationItems = configRepository.GetDataAsync(new QueryConfigurationItemRequestModel
            {
                PageIndex = 1,
                PageSize = int.MaxValue,
                ProjectName = _projectName,
                NamespaceName = _namespaceName
            }).Result;
            if (configurationItems == null) return null;
            foreach (ConfigurationItemListDTO item in configurationItems)
            {
                if (item.Value.IsJson())
                {
                    JsonDocument jsonDoc = JsonDocument.Parse(item.Value);
                    SetJsonValue(jsonDoc.RootElement, item.Key);
                }
                else
                {
                    Set(item.Key, item.Value);
                }
            }
            return configurationItems.ToJson().ToMd5_32Encode();
        }
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="jsonElement"></param>
        /// <param name="key"></param>
        private void SetJsonValue(JsonElement jsonElement, string key)
        {
            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.Object:
                    JsonElement.ObjectEnumerator jsonObject = jsonElement.EnumerateObject();
                    SetJsonValue(jsonObject, key);
                    break;
                case JsonValueKind.Array:
                    JsonElement.ArrayEnumerator jsonArray = jsonElement.EnumerateArray();
                    SetJsonValue(jsonArray, key);
                    break;
                case JsonValueKind.String:
                    string? stringValue = jsonElement.GetString();
                    Set(key, stringValue);
                    break;
                case JsonValueKind.Number:
                    string numberValue = jsonElement.GetDecimal().ToString();
                    Set(key, numberValue);
                    break;
                case JsonValueKind.False:
                case JsonValueKind.True:
                    string booleanValue = jsonElement.GetBoolean().ToString();
                    Set(key, booleanValue);
                    break;
                case JsonValueKind.Undefined:
                case JsonValueKind.Null:
                    Set(key, null);
                    break;
            }
        }
        /// <summary>
        /// 设置Json值
        /// </summary>
        /// <param name="jsonArray"></param>
        /// <param name="key"></param>
        private void SetJsonValue(JsonElement.ArrayEnumerator jsonArray, string key)
        {
            int index = 0;
            foreach (JsonElement jsonElement in jsonArray)
            {
                string trueKey = $"{key}:{index++}";
                SetJsonValue(jsonElement, trueKey);
            }
        }
        /// <summary>
        /// 设置Json值
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <param name="key"></param>
        private void SetJsonValue(JsonElement.ObjectEnumerator jsonObject, string key)
        {
            foreach (JsonProperty jsonProperty in jsonObject)
            {
                SetJsonValue(jsonProperty, key);
            }
        }
        /// <summary>
        /// 设置Json值
        /// </summary>
        /// <param name="jsonProperty"></param>
        /// <param name="key"></param>
        private void SetJsonValue(JsonProperty jsonProperty, string key)
        {
            string trueKey = $"{key}:{jsonProperty.Name}";
            SetJsonValue(jsonProperty.Value, trueKey);
        }
        /// <summary>
        /// 自动加载
        /// </summary>
        /// <param name="state"></param>
        private void AutoLoad(object? state)
        {
            StopReloadTimer();
            Load();
            StartReloadTimer();
        }
        /// <summary>
        /// 开始重载定时器
        /// </summary>
        private void StartReloadTimer() => reloadTimer.Change(_reloadInterval, Timeout.InfiniteTimeSpan);
        /// <summary>
        /// 停止重载定时器
        /// </summary>
        private void StopReloadTimer() => reloadTimer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);

    }
}
