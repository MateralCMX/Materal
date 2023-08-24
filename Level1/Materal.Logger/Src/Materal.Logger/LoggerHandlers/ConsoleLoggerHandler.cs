using Materal.Logger.LoggerHandlers.Models;
using Materal.Utils;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// 控制台日志处理器
    /// </summary>
    public class ConsoleLoggerHandler : BufferLoggerHandler<ConsoleLoggerHandlerModel>
    {
        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="datas"></param>
        protected override void HandlerOKData(ConsoleLoggerHandlerModel[] datas)
        {
            foreach (ConsoleLoggerHandlerModel data in datas)
            {
                ConsoleQueue.WriteLine(data.WriteMessage, data.Color);
            }
        }
    }
}
