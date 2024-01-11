using Materal.Logger.Repositories;

namespace Materal.Logger.ConfigModels
{
    /// <summary>
    /// 数据库目标配置
    /// </summary>
    public abstract class DBLoggerTargetConfig<TLoggerWriter, TDBFiled> : BatchLoggerTargetConfig<TLoggerWriter>
        where TLoggerWriter : ILoggerWriter
        where TDBFiled : IDBFiled, new()
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public abstract string ConnectionString { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; } = "MateralLogger";
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
