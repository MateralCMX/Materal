﻿using Materal.Logger.DBLogger;

namespace Materal.Logger.OracleLogger
{
    /// <summary>
    /// Oracle目标配置
    /// </summary>
    public class OracleLoggerTargetOptions : DBLoggerTargetOptions<OracleFiled>
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public override string ConnectionString { get; set; } = "user id=oracle; Password=123456; data source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST =127.0.0.1)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = YXEYORCL)));VALIDATE CONNECTION=True;";
        private static readonly List<OracleFiled> _defaultFileds =
        [
            new() { Name = "ID", Type = "VARCHAR2(36)", Value = "${LogID}", PK = true },
            new() { Name = "CreateTime", Type = "DATE", Value = "${DateTime}", Index = "DESC", IsNull = false },
            new() { Name = "Level", Type = "VARCHAR2(50)", Value = "${Level}" },
            new() { Name = "ProgressID", Type = "VARCHAR2(20)", Value = "${ProgressID}" },
            new() { Name = "ThreadID", Type = "VARCHAR2(20)", Value = "${ThreadID}" },
            new() { Name = "Scope", Type = "VARCHAR2(100)", Value = "${Scope}" },
            new() { Name = "MachineName", Type = "VARCHAR2(100)", Value = "${MachineName}" },
            new() { Name = "CategoryName", Type = "VARCHAR2(100)", Value = "${CategoryName}" },
            new() { Name = "Application", Type = "VARCHAR2(50)", Value = "${Application}" },
            new() { Name = "Message", Type = "CLOB", Value = "${Message}" },
            new() { Name = "Exception", Type = "CLOB", Value = "${Exception}" }
        ];
        /// <summary>
        /// 默认字段
        /// </summary>
        public override List<OracleFiled> DefaultFileds => _defaultFileds;
    }
}
