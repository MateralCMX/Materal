using Materal.Logger.LoggerHandlers;
using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using System.Reflection;
using System.Threading.Tasks.Dataflow;

namespace Materal.Logger
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LoggerRuntime
    {
        /// <summary>
        /// 日志处理数据流块
        /// </summary>
        private readonly ActionBlock<LoggerHandlerModel> _actionBlock;
        /// <summary>
        /// 处理器集合
        /// </summary>
        private readonly List<ILoggerHandler> _handlers = new();
        /// <summary>
        /// 日志已关闭
        /// </summary>
        public bool IsClose { get; private set; } = false;
        /// <summary>
        /// 日志配置
        /// </summary>
        public LoggerConfig Config { get; private set; }
        /// <summary>
        /// 日志自身日志
        /// </summary>
        public ILoggerLog LoggerLog { get; private set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerRuntime(LoggerConfig loggerConfig, ILoggerLog loggerLog)
        {
            _actionBlock = new(HandlerLog);
            Config = loggerConfig;
            LoggerLog = loggerLog;
        }
        /// <summary>
        /// 日志服务准备就绪
        /// </summary>
        public void OnLoggerServiceReady() => Parallel.ForEach(_handlers, handler => handler.OnLoggerServiceReady());
        /// <summary>
        /// 添加多个处理器
        /// </summary>
        /// <param name="configs"></param>
        public void AddHandlers(IEnumerable<LoggerTargetConfigModel> configs)
        {
            foreach (ILoggerHandler loggerHandler in configs.Select(m => m.GetLoggerHandler(this)))
            {
                if (_handlers.Contains(loggerHandler)) continue;
                _handlers.Add(loggerHandler);
            }
        }
        /// <summary>
        /// 处理日志
        /// </summary>
        /// <param name="model"></param>
        public void Handler(LoggerHandlerModel model)
        {
            if (IsClose) return;
            _actionBlock.Post(model);
        }
        /// <summary>
        /// 处理日志
        /// </summary>
        /// <param name="model"></param>
        private void HandlerLog(LoggerHandlerModel model)
        {
            if (_handlers is null || _handlers.Count <= 0) return;
            Parallel.ForEach(_handlers, handler => handler.Handler(model));
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public async Task ShutdownAsync()
        {
            IsClose = true;
            if (Config.Mode == LoggerModeEnum.Normal) return;
            LoggerLog.LogInfomation($"正在关闭MateralLogger");
            if (_handlers is null) return;
            _actionBlock.Complete();
            await _actionBlock.Completion;
            foreach (ILoggerHandler handler in _handlers)
            {
                await handler.ShutdownAsync();
            }
            LoggerLog.LogInfomation($"已关闭MateralLogger");
            await LoggerLog.ShutdownAsync();
        }
    }
}
