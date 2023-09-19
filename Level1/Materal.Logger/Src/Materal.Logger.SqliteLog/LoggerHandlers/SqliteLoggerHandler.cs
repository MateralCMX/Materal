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
        public SqliteLoggerHandler(LoggerRuntime loggerRuntime) : base(loggerRuntime)
        {
        }
        /// <summary>
        /// 处理合格的数据
        /// </summary>
        /// <param name="datas"></param>
        protected override void HandlerData(SqliteLoggerHandlerModel[] datas)
        {
            IGrouping<string, SqliteLoggerHandlerModel>[] groupDatas = datas.GroupBy(m => m.Path).ToArray();
            Parallel.ForEach(groupDatas, item =>
            {
                try
                {
                    SqliteRepository repository = new(item.Key);
                    repository.Inserts(datas);
                }
                catch (Exception exception)
                {
                    LoggerLog.LogError($"日志记录到Sqlite[{item.Key}]失败：", exception);
                }
            });
        }
    }
}
