using Materal.Logger.LoggerHandlers;
using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using System.Threading.Tasks.Dataflow;

namespace Materal.Logger
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LoggerRuntime
    {
        /// <summary>
        /// 处理器集合
        /// </summary>
        private readonly List<ILoggerHandler> _handlers = new();
        /// <summary>
        /// 日志已关闭
        /// </summary>
        public bool _isClose = false;
        /// <summary>
        /// 日志处理数据流块
        /// </summary>
        private readonly ActionBlock<LoggerHandlerModel> _actionBlock;
        private readonly LoggerConfig _loggerConfig;
        private readonly ILoggerLog _loggerLog;
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerRuntime(LoggerConfig loggerConfig, ILoggerLog loggerLog)
        {
            _actionBlock = new(HandlerLog);
            _loggerConfig = loggerConfig;
            _loggerLog = loggerLog;
        }
        /// <summary>
        /// 添加一个处理器
        /// </summary>
        /// <param name="config"></param>
        public void AddHandler(LoggerTargetConfigModel config)
        {
            ILoggerHandler loggerHandler = config.GetLoggerHandler();
            AddHandler(loggerHandler);
        }
        /// <summary>
        /// 添加一个处理器
        /// </summary>
        /// <param name="loggerHandler"></param>
        private void AddHandler(ILoggerHandler loggerHandler)
        {
            if (_handlers.Contains(loggerHandler)) return;
            _handlers.Add(loggerHandler);
        }
        /// <summary>
        /// 处理日志
        /// </summary>
        /// <param name="model"></param>
        public void Handler(LoggerHandlerModel model)
        {
            if (_isClose) return;
            _actionBlock.Post(model);
        }
        /// <summary>
        /// 处理日志
        /// </summary>
        /// <param name="model"></param>
        private void HandlerLog(LoggerHandlerModel model)
        {
            if (_handlers is null || _handlers.Count <= 0) return;
            Parallel.ForEach(_handlers, handler => handler.Handler(model, _loggerConfig, _loggerLog));
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public async Task ShutdownAsync()
        {
            _isClose = true;
            if (_loggerConfig.Mode == LoggerModeEnum.Normal) return;
            _loggerLog.LogInfomation($"正在关闭MateralLogger");
            if (_handlers is null) return;
            _actionBlock.Complete();
            await _actionBlock.Completion;
            foreach (ILoggerHandler handler in _handlers)
            {
                await handler.ShutdownAsync(_loggerLog);
            }
            _loggerLog.LogInfomation($"已关闭MateralLogger");
            await _loggerLog.ShutdownAsync();
        }
    }
}
