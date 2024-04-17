using Materal.Logger.BatchLogger;
using Materal.Logger.DBLogger.Repositories;

namespace Materal.Logger.DBLogger
{
    /// <summary>
    /// 数据库日志写入器
    /// </summary>
    /// <typeparam name="TLoggerTargetOptions"></typeparam>
    /// <typeparam name="TDBFiled"></typeparam>
    public abstract class DBLog<TLoggerTargetOptions, TDBFiled>
        where TLoggerTargetOptions : DBLoggerTargetOptions<TDBFiled>
        where TDBFiled : IDBFiled, new()
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; }
        /// <summary>
        /// 字段
        /// </summary>
        public List<IDBFiled> Fileds { get; set; }
        /// <summary>
        /// 日志
        /// </summary>
        public Log Log { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="batchLog"></param>
        /// <param name="options"></param>
        public DBLog(BatchLog<TLoggerTargetOptions> batchLog, LoggerOptions options)
        {
            Log = batchLog.Log;
            ConnectionString = Log.ApplyText(batchLog.TargetOptions.ConnectionString, options);
            TableName = Log.ApplyText(batchLog.TargetOptions.TableName, options);
            Fileds = batchLog.TargetOptions.Fileds.Count <= 0
                ? batchLog.TargetOptions.DefaultFileds.Select(m => m.GetNewDBFiled(batchLog.Log, batchLog.RuleOptions, batchLog.TargetOptions, options)).ToList()
                : batchLog.TargetOptions.Fileds.Select(m => m.GetNewDBFiled(batchLog.Log, batchLog.RuleOptions, batchLog.TargetOptions, options)).ToList();
        }
    }
}
