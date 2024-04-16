using Materal.Logger.DBLogger.Repositories;

namespace Materal.Logger.DBLogger
{
    /// <summary>
    /// 数据库日志目标选项
    /// </summary>
    /// <typeparam name="TDBFiled"></typeparam>
    public abstract class DBLoggerTargetOptions<TDBFiled> : LoggerTargetOptions
        where TDBFiled : IDBFiled, new()
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public abstract string ConnectionString { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; } = "Logs";
        /// <summary>
        /// 默认字段
        /// </summary>
        public abstract List<TDBFiled> DefaultFileds { get; }
        /// <summary>
        /// 字段
        /// </summary>
        public List<TDBFiled> Fileds { get; set; } = [];
    }
}
