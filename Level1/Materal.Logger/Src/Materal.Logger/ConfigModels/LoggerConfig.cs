using System.Dynamic;
using LogLevelEnum = Microsoft.Extensions.Logging.LogLevel;

namespace Materal.Logger.ConfigModels
{
    /// <summary>
    /// 日志配置
    /// </summary>
    public class LoggerConfig
    {
        private static IConfiguration? _configuration;
        /// <summary>
        /// 配置对象
        /// </summary>
        public static IConfiguration? Configuration
        {
            get => _configuration;
            set
            {
                _configuration = value;
                if (_configuration is null) return;
                _configuration.GetReloadToken().RegisterChangeCallback(Configuration_OnChanged, null);
                Configuration_OnChanged();
            }
        }
        /// <summary>
        /// 目标类型字典
        /// </summary>
        public static Dictionary<string, Type> TargetTypes { get; set; } = [];
        /// <summary>
        /// 目标类型改变事件
        /// </summary>
        public static event Action? OnConfigurationChanged;
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 最小日志等级
        /// </summary>
        public LogLevelEnum MinLogLevel { get; set; } = LogLevelEnum.Trace;
        /// <summary>
        /// 最大日志等级
        /// </summary>
        public LogLevelEnum MaxLogLevel { get; set; } = LogLevelEnum.Critical;
        /// <summary>
        /// 最小自身日志等级
        /// </summary>
        public LogLevelEnum MinLoggerLogLevel { get; set; } = LogLevelEnum.Trace;
        /// <summary>
        /// 最大自身日志等级
        /// </summary>
        public LogLevelEnum MaxLoggerLogLevel { get; set; } = LogLevelEnum.Critical;
        private string? _application;
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string Application
        {
            get => _application ?? Assembly.GetEntryAssembly()?.GetName().Name ?? "Use MateralLogger application";
            set => _application = value;
        }
        /// <summary>
        /// 日志等级组
        /// </summary>
        public Dictionary<string, LogLevelEnum>? LogLevel { get; set; }
        /// <summary>
        /// 目标配置
        /// </summary>
        public List<TargetConfig> Targets { get; set; } = [];
        /// <summary>
        /// 规则配置
        /// </summary>
        public List<RuleConfig> Rules { get; set; } = [];
        /// <summary>
        /// 日志等级组
        /// </summary>
        public static Dictionary<string, LogLevelEnum>? DefaultLogLevel { get; set; }
        /// <summary>
        /// 自定义配置
        /// </summary>
        public static Dictionary<string, object?> CustomConfig { get; private set; } = [];
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerConfig()
        {
            OnConfigurationChanged += LoggerConfig_OnTargetTypesChanged;
        }
        /// <summary>
        /// 析构方法
        /// </summary>
        ~LoggerConfig()
        {
            OnConfigurationChanged -= LoggerConfig_OnTargetTypesChanged;
        }
        /// <summary>
        /// 当目标类型改变
        /// </summary>
        private void LoggerConfig_OnTargetTypesChanged()
        {
            Targets.Clear();
            if (Configuration is null) return;
            DefaultLogLevel = Configuration.GetValueObject<Dictionary<string, LogLevelEnum>>("Logging:MateralLogger:LogLevel");
            List<ExpandoObject>? targets = GetTargetExpandoObjects();
            if (targets is null || targets.Count <= 0) return;
            foreach (ExpandoObject target in targets)
            {
                string? targetTypeName = target.GetValue<string>(nameof(TargetConfig.Type));
                if (targetTypeName is null || string.IsNullOrWhiteSpace(targetTypeName)) continue;
                if (!TargetTypes.ContainsKey(targetTypeName)) continue;
                Type targetConfigType = TargetTypes[targetTypeName];
                object targetConfigObj = target.ToJson().JsonToObject(targetConfigType);
                if (targetConfigObj is not TargetConfig targetConfig) continue;
                Targets.Add(targetConfig);
            }
        }
        /// <summary>
        /// 当配置改变
        /// </summary>
        /// <param name="state"></param>
        private static void Configuration_OnChanged(object? state = null)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Type[] targetConfigTypes = assembly.GetTypes().Where(m => !m.IsAbstract && m.IsClass && m.IsAssignableTo<TargetConfig>()).ToArray();
                foreach (Type targetConfigType in targetConfigTypes)
                {
                    TargetConfig? targetConfig = targetConfigType.InstantiationOrDefault<TargetConfig>();
                    if(targetConfig is null) continue;
                    TargetTypes.TryAdd(targetConfig.Type, targetConfigType);
                }
            }
            OnConfigurationChanged?.Invoke();
        }
        /// <summary>
        /// 获取目标配置
        /// </summary>
        /// <returns></returns>
        private static List<ExpandoObject>? GetTargetExpandoObjects()
        {
            if (Configuration is null) return null;
            string? targetsJson = Configuration.GetValue("Logging:MateralLogger:Targets");
            if (targetsJson is null || string.IsNullOrWhiteSpace(targetsJson) || !targetsJson.IsArrayJson()) return null;
            object? targetsObj = targetsJson.ToExpandoObject();
            if (targetsObj is not List<ExpandoObject> targets) return null;
            return targets;
        }
        /// <summary>
        /// 设置自定义配置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetCustomConfig(string key, object? value) => CustomConfig.TryAdd(key, value);
    }
}
