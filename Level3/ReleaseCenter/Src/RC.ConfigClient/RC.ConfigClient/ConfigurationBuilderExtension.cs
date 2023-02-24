﻿using ConfigCenter.Client;
using Microsoft.Extensions.Configuration;

namespace RC.ConfigClient
{
    public static class ConfigurationBuilderExtension
    {
        private const string _defaultNameSpace = "Application";
        private static string? _configUrl;
        private static string? _projectName;
        private static int? _reloadSecondInterval;
        private static bool _isAbsoluteUrl = false;
        public static IConfigurationBuilder SetConfigCenter(this IConfigurationBuilder builder, string configUrl, string projectName, int reloadSecondInterval = 60, bool isAbsoluteUrl = false)
        {
            _configUrl = configUrl;
            _projectName = projectName;
            _reloadSecondInterval = reloadSecondInterval;
            _isAbsoluteUrl = isAbsoluteUrl;
            return builder;
        }
        public static IConfigurationBuilder AddNameSpace(this IConfigurationBuilder builder, string configUrl, string projectName, string @namespace, int reloadSecondInterval = 60, bool isAbsoluteUrl = false)
        {
            SetConfigCenter(builder, configUrl, projectName, reloadSecondInterval, isAbsoluteUrl);
            builder.Add(new MateralConfigurationSource(@namespace, configUrl, projectName, reloadSecondInterval, isAbsoluteUrl));
            return builder;
        }
        public static IConfigurationBuilder AddNameSpace(this IConfigurationBuilder builder, string @namespace)
        {
            if (string.IsNullOrWhiteSpace(_configUrl) || !_configUrl.IsUrl() || string.IsNullOrWhiteSpace(_projectName) || _reloadSecondInterval == null)
            {
                throw new MateralConfigClientException($"请先调用IConfigurationBuilder.{nameof(SetConfigCenter)}方法设置环境");
            }
            builder.Add(new MateralConfigurationSource(@namespace, _configUrl, _projectName, _reloadSecondInterval.Value, _isAbsoluteUrl));
            return builder;
        }
        public static IConfigurationBuilder AddDefaultNameSpace(this IConfigurationBuilder builder, string configUrl, string projectName, int reloadSecondInterval = 60, bool isAbsoluteUrl = false) => builder.AddNameSpace(configUrl, projectName, _defaultNameSpace, reloadSecondInterval, isAbsoluteUrl);
        public static IConfigurationBuilder AddDefaultNameSpace(this IConfigurationBuilder builder) => builder.AddNameSpace(_defaultNameSpace);
    }
}
