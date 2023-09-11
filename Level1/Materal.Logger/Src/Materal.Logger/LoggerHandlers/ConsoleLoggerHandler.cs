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
        private static readonly ActionBlock<ConsoleMessageModel> _writeBuffer = new(WriteMessage);
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        protected override void Handler(LoggerRuleConfigModel rule, ConsoleLoggerTargetConfigModel target, LoggerHandlerModel model)
        {
            string writeMessage = LoggerHandlerHelper.FormatMessage(target.Format, model.LogLevel, model.Message, model.CategoryName, model.Scope, model.CreateTime, model.Exception, model.ThreadID, Guid.NewGuid());
            ConsoleColor color = target.Colors.GetConsoleColor(model.LogLevel);
            _writeBuffer.Post(new ConsoleMessageModel
            {
                Color = color,
                Message = writeMessage
            });
        }
        /// <summary>
        /// 输出消息
        /// </summary>
        /// <param name="model"></param>
        private static void WriteMessage(ConsoleMessageModel model)
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
            _writeBuffer.Complete();
            await _writeBuffer.Completion;
        }
    }
}
