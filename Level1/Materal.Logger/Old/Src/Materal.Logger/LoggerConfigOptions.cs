using Materal.Logger.Models;
using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    /// <summary>
    /// 日志配置选项
    /// </summary>
    public class LoggerConfigOptions
    {
        private readonly List<Action<List<LoggerTargetConfigModel>>> _initTargets = new();
        private readonly List<Action<List<LoggerRuleConfigModel>, string[]>> _initRules = new();
        /// <summary>
        /// 添加一个控制台目标
        /// </summary>
        /// <param name="name"></param>
        /// <param name="format"></param>
        /// <param name="colors"></param>
        public void AddConsoleTarget(string name, string? format = null, Dictionary<LogLevel, ConsoleColor>? colors = null)
        {
            _initTargets.Add(targets => targets.AddConsole(name, format, colors));
        }

        /// <summary>
        /// 添加一个文件输出
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="format"></param>
        /// <param name="colors"></param>
        /// <param name="bufferCount"></param>
        public void AddFileTarget(string name, string path, string? format = null, Dictionary<LogLevel, ConsoleColor>? colors = null, int? bufferCount = null)
        {
            _initTargets.Add(targets => targets.AddFile(name, path, format, colors, bufferCount));
        }
        /// <summary>
        /// 添加一个Sqlite输出
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="colors"></param>
        /// <param name="bufferCount"></param>
        public void AddSqliteTarget(string name, string path, Dictionary<LogLevel, ConsoleColor>? colors = null, int? bufferCount = null)
        {
            _initTargets.Add(targets => targets.AddSqlite(name, path, colors, bufferCount));
        }
        /// <summary>
        /// 添加一个SqlServer输出
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="colors"></param>
        /// <param name="bufferCount"></param>
        public void AddSqlServerTarget(string name, string connectionString, Dictionary<LogLevel, ConsoleColor>? colors = null, int? bufferCount = null)
        {
            _initTargets.Add(targets => targets.AddSqlServer(name, connectionString, colors, bufferCount));
        }
        /// <summary>
        /// 添加一个全目标规则
        /// </summary>
        /// <param name="minLevel"></param>
        /// <param name="maxLevel"></param>
        /// <param name="ignores"></param>
        public void AddAllTargetRule(LogLevel? minLevel = null, LogLevel? maxLevel = null, string[]? ignores = null)
        {
            _initRules.Add((rules, allTargets) => rules.AddRule(allTargets, minLevel, maxLevel, ignores));
        }
        /// <summary>
        /// 添加一个规则
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="minLevel"></param>
        /// <param name="maxLevel"></param>
        /// <param name="ignores"></param>
        public void AddRule(string[] targets, LogLevel? minLevel = null, LogLevel? maxLevel = null, string[]? ignores = null)
        {
            _initRules.Add((rules, allTargets) => rules.AddRule(targets, minLevel, maxLevel, ignores));
        }
        /// <summary>
        /// 应用选项
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="rules"></param>
        public void Apply(List<LoggerTargetConfigModel> targets, List<LoggerRuleConfigModel> rules)
        {
            foreach (Action<List<LoggerTargetConfigModel>> initTarget in _initTargets)
            {
                initTarget?.Invoke(targets);
            }
            string[] allTargetNames = targets.Select(m => m.Name).ToArray();
            foreach (Action<List<LoggerRuleConfigModel>, string[]> initRule in _initRules)
            {
                initRule?.Invoke(rules, allTargetNames);
            }
        }
    }
}
