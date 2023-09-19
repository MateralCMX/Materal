using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using Materal.Logger.Repositories;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// SqlServer日志处理器
    /// </summary>
    public class SqlServerLoggerHandler : BufferLoggerHandler<SqlServerLoggerHandlerModel, SqlServerLoggerTargetConfigModel>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="bufferPushInterval"></param>
        /// <param name="bufferCount"></param>
        public SqlServerLoggerHandler(int bufferPushInterval, int bufferCount) : base(bufferPushInterval, bufferCount)
        {
        }
        /// <summary>
        /// 处理合格的数据
        /// </summary>
        /// <param name="datas"></param>
        protected override void HandlerData(SqlServerLoggerHandlerModel[] datas)
        {
            IGrouping<string, SqlServerLoggerHandlerModel>[] groupDatas = datas.GroupBy(m => m.ConnectionString).ToArray();
            ILoggerLog loggerLog = datas.GetLoggerLog();
            Parallel.ForEach(groupDatas, item =>
            {
                try
                {
                    SqlServerRepository repository = new(item.Key);
                    repository.Inserts(datas);
                }
                catch (Exception exception)
                {
                    loggerLog.LogError($"日志记录到SqlServer[{item.Key}]失败：", exception);
                }
            });
        }
    }
}
