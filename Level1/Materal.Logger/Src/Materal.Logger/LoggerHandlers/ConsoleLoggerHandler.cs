using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using Materal.Utils.Model;
using System.Threading.Tasks.Dataflow;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// 控制台日志处理器
    /// </summary>
    public class ConsoleLoggerHandler : LoggerHandler<ConsoleLoggerTargetConfigModel>
    {
        private readonly ActionBlock<ConsoleMessageModel> _writeBuffer;
        /// <summary>
        /// 构造方法
        /// </summary>
        public ConsoleLoggerHandler(LoggerRuntime loggerRuntime) : base(loggerRuntime)
        {
            _writeBuffer = new(WriteMessage);
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        protected override void Handler(LoggerRuleConfigModel rule, ConsoleLoggerTargetConfigModel target, LoggerHandlerModel model)
        {
            try
            {
                string writeMessage = LoggerHandlerHelper.FormatMessage(Config, target.Format, model);
                ConsoleColor color = target.Colors.GetConsoleColor(model.LogLevel);
                _writeBuffer.Post(new ConsoleMessageModel
                {
                    Color = color,
                    Message = writeMessage
                });
            }
            catch (Exception exception)
            {
                LoggerLog.LogError($"输出控制台日志[{model.ID}]失败", exception);
            }
        }
        /// <summary>
        /// 输出消息
        /// </summary>
        /// <param name="model"></param>
        private void WriteMessage(ConsoleMessageModel model)
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = model.Color;
            if (model.Args != null)
            {
                Console.WriteLine(model.Message, model.Args);
            }
            else
            {
                Console.WriteLine(model.Message);
            }
            Console.ForegroundColor = foregroundColor;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public override async Task ShutdownAsync()
        {
            LoggerLog.LogDebug($"正在关闭ConsoleLoggerHandler");
            _writeBuffer.Complete();
            await _writeBuffer.Completion;
            LoggerLog.LogDebug($"已关闭ConsoleLoggerHandler");
        }
    }
}
