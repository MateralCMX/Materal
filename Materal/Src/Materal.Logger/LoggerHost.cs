﻿using System.Threading.Tasks.Dataflow;

namespace Materal.Logger
{
    /// <summary>
    /// 日志主机
    /// </summary>
    public class LoggerHost : ILoggerHost
    {
        private readonly SemaphoreSlim _semaphore = new(0, 1);
        private readonly IEnumerable<ILoggerWriter> _logWriters;
        private readonly ActionBlock<Log> _writeLoggerBlock;
        private readonly Dictionary<LoggerRuleOptions, List<LoggerTargetOptions>> _targets = [];
        private readonly ILoggerInfo _loggerInfo;
        /// <summary>
        /// 是否关闭
        /// </summary>
        public bool Runing { get; private set; } = false;
        private LoggerOptions? _options;
        /// <summary>
        /// 配置
        /// </summary>
        public LoggerOptions Options
        {
            get => _options ?? throw new LoggerException("获取日志选项失败");
            set
            {
                _semaphore.Wait();
                try
                {
                    _options = value;
                    _targets.Clear();
                    foreach (LoggerRuleOptions ruleOptions in _options.Rules)
                    {
                        foreach (LoggerTargetOptions targetOptions in Options.Targets)
                        {
                            if (!ruleOptions.Targets.Contains(targetOptions.Name)) continue;
                            if (!_targets.TryGetValue(ruleOptions, out List<LoggerTargetOptions>? loggerTargetOptions))
                            {
                                _targets[ruleOptions] = [targetOptions];
                            }
                            else
                            {
                                loggerTargetOptions.Add(targetOptions);
                            }
                        }
                    }
                    _loggerInfo.Options = _options;
                }
                finally
                {
                    _semaphore.Release();
                }
            }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerHost(IEnumerable<ILoggerWriter> logWriters, ILoggerInfo loggerInfo)
        {
            _loggerInfo = loggerInfo;
            _logWriters = logWriters;
            _writeLoggerBlock = new(LoggerWriterHandlerAsync);
            _semaphore.Release();
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        public void Log(Log log)
        {
            if (!Runing) return;
            _writeLoggerBlock.Post(log);
        }
        /// <summary>
        /// 日志写入处理
        /// </summary>
        /// <param name="log"></param>
        public async Task LoggerWriterHandlerAsync(Log log)
        {
            log.Application = log.ApplyText(log.Application, Options);
            log.Message = log.ApplyText(log.Message, Options);
            foreach (KeyValuePair<LoggerRuleOptions, List<LoggerTargetOptions>> item in _targets)
            {
                foreach (LoggerTargetOptions targetOptions in item.Value)
                {
                    foreach (ILoggerWriter logWriter in _logWriters)
                    {
                        await logWriter.LogAsync(log, item.Key, targetOptions);
                    }
                }
            }
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (Runing) return;
                Runing = true;
                await _loggerInfo.StartAsync();
                _loggerInfo.LogDebug($"正在启动...");
                foreach (ILoggerWriter writer in _logWriters)
                {
                    string name = writer.GetType().Name;
                    _loggerInfo.LogDebug($"{name}正在启动");
                    await writer.StartAsync();
                    _loggerInfo.LogDebug($"{name}启动成功");
                }
                _loggerInfo.LogDebug($"已启动");
            }
            finally
            {
                _semaphore.Release();
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public async Task StopAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (!Runing) return;
                Runing = false;
                _loggerInfo.LogDebug($"正在关闭...");
                if (_writeLoggerBlock is not null)
                {
                    _writeLoggerBlock.Complete();
                    await _writeLoggerBlock.Completion;
                }
                foreach (ILoggerWriter writer in _logWriters)
                {
                    string name = writer.GetType().Name;
                    _loggerInfo.LogDebug($"{name}正在关闭");
                    await writer.StopAsync();
                    _loggerInfo.LogDebug($"{name}关闭成功");
                }
                _loggerInfo.LogDebug($"已关闭");
                await _loggerInfo.StopAsync();
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
