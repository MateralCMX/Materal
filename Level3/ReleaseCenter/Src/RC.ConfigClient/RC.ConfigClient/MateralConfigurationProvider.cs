﻿using Microsoft.Extensions.Configuration;
using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.PresentationModel.ConfigurationItem;
using System.Text.Json;

namespace RC.ConfigClient
{
    public class MateralConfigurationProvider : ConfigurationProvider
    {
        /// <summary>
        /// 命名空间名称
        /// </summary>
        public string NamespaceName { get; }
        private readonly string _configUrl;
        private readonly string _projectName;
        private readonly int _reloadSecondInterval;
        private readonly TimeSpan _reloadInterval;
        private readonly Timer reloadTimer;
        public MateralConfigurationProvider(string namespaceName, string configUrl, string projectName, int reloadSecondInterval)
        {
            NamespaceName = namespaceName;
            _configUrl = configUrl;
            _projectName = projectName;
            _reloadSecondInterval = reloadSecondInterval;
            Load();
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
        public override void Load()
        {
            ConfigurationItemHttpClient configRepository = new ConfigurationItemHttpClient(_configUrl);
            ICollection<ConfigurationItemListDTO>? configurationItems = configRepository.GetDataAsync(new QueryConfigurationItemRequestModel
            {
                PageIndex = 1,
                PageSize = int.MaxValue,
                ProjectName = _projectName,
                NamespaceName = NamespaceName
            }).Result;
            if (configurationItems == null) return;
            foreach (ConfigurationItemListDTO item in configurationItems)
            {
                try
                {
                    JsonDocument jsonDoc = JsonDocument.Parse(item.Value);
                    SetJsonValue(jsonDoc.RootElement, item.Key);
                }
                catch
                {
                    Set(item.Key, item.Value);
                }
            }
        }
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
        private void SetJsonValue(JsonElement.ArrayEnumerator jsonArray, string key)
        {
            int index = 0;
            foreach (JsonElement jsonElement in jsonArray)
            {
                string trueKey = $"{key}:{index++}";
                SetJsonValue(jsonElement, trueKey);
            }
        }
        private void SetJsonValue(JsonElement.ObjectEnumerator jsonObject, string key)
        {
            foreach (JsonProperty jsonProperty in jsonObject)
            {
                SetJsonValue(jsonProperty, key);
            }
        }
        private void SetJsonValue(JsonProperty jsonProperty, string key)
        {
            string trueKey = $"{key}:{jsonProperty.Name}";
            SetJsonValue(jsonProperty.Value, trueKey);
        }
        private void AutoLoad(object? state)
        {
            StopReloadTimer();
            Load();
            StartReloadTimer();
        }
        private void StartReloadTimer()
        {
            while (!reloadTimer.Change(_reloadInterval, Timeout.InfiniteTimeSpan)) ;
        }
        private void StopReloadTimer()
        {
            while (!reloadTimer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan)) ;
        }
    }
}