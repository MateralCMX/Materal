using System.Dynamic;
using LogLevelEnum = Microsoft.Extensions.Logging.LogLevel;

namespace Materal.Logger.ConfigModels
{
    /// <summary>
    /// 日志配置
    /// </summary>
    public class LoggerConfig
    {
        /// <summary>
        /// 目标类型字典
        /// </summary>
        public static Dictionary<string, Type> TargetTypes { get; set; } = [];
        static LoggerConfig()
        {
            DirectoryInfo directoryInfo = new(AppDomain.CurrentDomain.BaseDirectory);
            if (directoryInfo.Exists)
            {
                FileInfo[] fileInfos = directoryInfo.GetFiles("*.dll");
                foreach (FileInfo fileInfo in fileInfos)
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFrom(fileInfo.FullName);
                        Type[] targetConfigTypes = assembly.GetTypes().Where(m => !m.IsAbstract && m.IsClass && m.IsAssignableTo<TargetConfig>()).ToArray();
                        foreach (Type targetConfigType in targetConfigTypes)
                        {
                            TargetConfig? targetConfig = targetConfigType.InstantiationOrDefault<TargetConfig>();
                            if (targetConfig is null) continue;
                            TargetTypes.TryAdd(targetConfig.Type, targetConfigType);
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
        }
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
        /// 作用域组
        /// </summary>
        public Dictionary<string, LogLevelEnum>? Scopes { get; set; }
        /// <summary>
        /// 代码配置目标名称
        /// </summary>
        public List<string> CodeConfigTargetNames { get; } = [];
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
        /// 更新配置
        /// </summary>
        public void UpdateConfig(IServiceProvider serviceProvider)
        {
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            DefaultLogLevel = configuration.GetValueObject<Dictionary<string, LogLevelEnum>>("Logging:MateralLogger:LogLevel");
            Targets.RemoveAll(m => !CodeConfigTargetNames.Contains(m.Name));
            List<ExpandoObject>? targets = GetTargetExpandoObjects(configuration);
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
            UpdateLoggerWriterConfig();
        }
        /// <summary>
        /// 更新日志写入器配置
        /// </summary>
        public void UpdateLoggerWriterConfig()
        {
            foreach (TargetConfig target in Targets)
            {
                ILoggerWriter loggerWriter = target.GetLoggerWriter();
                loggerWriter.OnLoggerConfigChanged?.Invoke(this);
            }
        }
        /// <summary>
        /// 获取目标配置
        /// </summary>
        /// <returns></returns>
        private static List<ExpandoObject>? GetTargetExpandoObjects(IConfiguration configuration)
        {
            string? targetsJson = configuration.GetValue("Logging:MateralLogger:Targets");
            if (targetsJson is null || string.IsNullOrWhiteSpace(targetsJson) || !targetsJson.IsArrayJson()) return null;
            object? targetsObj = targetsJson.ToExpandoObject();
            if (targetsObj is not List<ExpandoObject> targets) return null;
            return targets;
        }
    }
}
