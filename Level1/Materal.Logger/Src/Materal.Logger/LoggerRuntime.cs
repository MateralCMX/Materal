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
        public static List<ILoggerHandler> Handlers { get; } = new();
        /// <summary>
        /// 日志已关闭
        /// </summary>
        public bool IsClose { get; private set; } = false;
        /// <summary>
        /// 日志处理数据流块
        /// </summary>
        private readonly ActionBlock<LoggerHandlerModel> _actionBlock;
        private readonly LoggerConfig _loggerConfig;
        private readonly LoggerLog _loggerLog;
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerRuntime(LoggerConfig loggerConfig, LoggerLog loggerLog)
        {
            _actionBlock = new(HandlerLog);
            _loggerConfig = loggerConfig;
            _loggerLog = loggerLog;
        }
        /// <summary>
        /// 处理日志
        /// </summary>
        /// <param name="model"></param>
        public void Handler(LoggerHandlerModel model) => _actionBlock.Post(model);
        /// <summary>
        /// 处理日志
        /// </summary>
        /// <param name="model"></param>
        private void HandlerLog(LoggerHandlerModel model)
        {
            Console.WriteLine(1234);
            //if (Handlers is null || Handlers.Count <= 0 || IsClose) return;
            //Parallel.ForEach(Handlers, handler => handler.Handler(model));
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public async Task ShutdownAsync()
        {
            IsClose = true;
            if (_loggerConfig.Mode == LoggerModeEnum.Normal) return;
            _loggerLog.LogInfomation($"正在关闭MateralLogger");
            if (Handlers is null) return;
            _actionBlock.Complete();
            await _actionBlock.Completion;
            foreach (ILoggerHandler handler in Handlers)
            {
                string handlerName = handler.GetType().Name;
                _loggerLog.LogDebug($"正在关闭{handlerName}");
                await handler.ShutdownAsync();
                _loggerLog.LogDebug($"已关闭{handlerName}");
            }
            _loggerLog.LogInfomation($"已关闭MateralLogger");
            await Task.CompletedTask;
        }
    }
}
