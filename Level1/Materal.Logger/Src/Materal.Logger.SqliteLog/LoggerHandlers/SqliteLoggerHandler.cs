using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using Materal.Logger.Repositories;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// Sqlite日志处理器
    /// </summary>
    public class SqliteLoggerHandler : BufferLoggerHandler<SqliteLoggerHandlerModel, SqliteLoggerTargetConfigModel>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="bufferPushInterval"></param>
        /// <param name="bufferCount"></param>
        public SqliteLoggerHandler(int bufferPushInterval, int bufferCount) : base(bufferPushInterval, bufferCount)
        {
        }
        /// <summary>
        /// 处理合格的数据
        /// </summary>
        /// <param name="datas"></param>
        protected override void HandlerData(SqliteLoggerHandlerModel[] datas)
        {
            IGrouping<string, SqliteLoggerHandlerModel>[] groupDatas = datas.GroupBy(m => m.Path).ToArray();
            ILoggerLog loggerLog = datas.GetLoggerLog();
            Parallel.ForEach(groupDatas, item =>
            {
                try
                {
                    SqliteRepository repository = new(item.Key);
                    repository.Inserts(datas);
                }
                catch (Exception exception)
                {
                    loggerLog.LogError($"日志记录到Sqlite[{item.Key}]失败：", exception);
                }
            });
        }
    }
}
