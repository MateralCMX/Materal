using Materal.Logger.Models;
using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    /// <summary>
    /// 日志扩展
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// 添加一个控制台输出
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <param name="format"></param>
        /// <param name="colors"></param>
        public static void AddConsole(this List<LoggerTargetConfigModel> list, string name, string? format = null, Dictionary<LogLevel, ConsoleColor>? colors = null)
        {
            LoggerTargetConfigModel model = new()
            {
                Enable = true,
                Type = TargetTypeEnum.Console,
                Name = name,
            };
            InitModel(model, format, colors, null);
            list.Add(model);
        }
        /// <summary>
        /// 添加一个文件输出
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="format"></param>
        /// <param name="colors"></param>
        /// <param name="bufferCount"></param>
        public static void AddFile(this List<LoggerTargetConfigModel> list, string name, string path, string? format = null, Dictionary<LogLevel, ConsoleColor>? colors = null, int? bufferCount = null)
        {
            LoggerTargetConfigModel model = new()
            {
                Enable = true,
                Type = TargetTypeEnum.File,
                Name = name,
                Path = path
            };
            InitModel(model, format, colors, bufferCount);
            list.Add(model);
        }
        /// <summary>
        /// 添加一个Sqlite输出
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="colors"></param>
        /// <param name="bufferCount"></param>
        public static void AddSqlite(this List<LoggerTargetConfigModel> list, string name, string path, Dictionary<LogLevel, ConsoleColor>? colors = null, int? bufferCount = null)
        {
            LoggerTargetConfigModel model = new()
            {
                Enable = true,
                Type = TargetTypeEnum.Sqlite,
                Name = name,
                Path = path
            };
            InitModel(model, null, colors, bufferCount);
            list.Add(model);
        }
        /// <summary>
        /// 添加一个SqlServer输出
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="colors"></param>
        /// <param name="bufferCount"></param>
        public static void AddSqlServer(this List<LoggerTargetConfigModel> list, string name, string connectionString, Dictionary<LogLevel, ConsoleColor>? colors = null, int? bufferCount = null)
        {
            LoggerTargetConfigModel model = new()
            {
                Enable = true,
                Type = TargetTypeEnum.Sqlite,
                Name = name,
                ConnectionString = connectionString
            };
            InitModel(model, null, colors, bufferCount);
            list.Add(model);
        }
        /// <summary>
        /// 添加一个规则
        /// </summary>
        /// <param name="list"></param>
        /// <param name="targets"></param>
        /// <param name="minLevel"></param>
        /// <param name="maxLevel"></param>
        /// <param name="ignores"></param>
        public static void AddRule(this List<LoggerRuleConfigModel> list, string[] targets, LogLevel? minLevel = null, LogLevel? maxLevel = null, string[]? ignores = null)
        {
            LoggerRuleConfigModel model = new()
            {
                Enable = true
            };
            model.Targets.AddRange(targets);
            if (minLevel != null)
            {
                model.MinLevel = minLevel.Value;
            }
            if (maxLevel != null)
            {
                model.MaxLevel = maxLevel.Value;
            }
            if (ignores != null && ignores.Length > 0)
            {
                model.Ignores.AddRange(ignores);
            }
            list.Add(model);
        }
        /// <summary>
        /// 初始化模型
        /// </summary>
        /// <param name="model"></param>
        /// <param name="format"></param>
        /// <param name="colors"></param>
        /// <param name="bufferCount"></param>
        private static void InitModel(LoggerTargetConfigModel model, string? format, Dictionary<LogLevel, ConsoleColor>? colors, int? bufferCount)
        {
            if (format != null && !string.IsNullOrWhiteSpace(format))
            {
                model.Format = format;
            }
            if (colors != null && colors.Count > 0)
            {
                foreach (var item in colors)
                {
                    switch (item.Key)
                    {
                        case LogLevel.Trace:
                            model.Colors.Trace = item.Value;
                            break;
                        case LogLevel.Debug:
                            model.Colors.Debug = item.Value;
                            break;
                        case LogLevel.Information:
                            model.Colors.Information = item.Value;
                            break;
                        case LogLevel.Warning:
                            model.Colors.Warning = item.Value;
                            break;
                        case LogLevel.Error:
                            model.Colors.Error = item.Value;
                            break;
                        case LogLevel.Critical:
                            model.Colors.Critical = item.Value;
                            break;
                        case LogLevel.None:
                            model.Colors.None = item.Value;
                            break;
                    }
                }
            }
            if(bufferCount != null && bufferCount > 0)
            {
                model.BufferCount = bufferCount.Value;
            }
        }
    }
}
