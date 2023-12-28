﻿namespace Materal.Logger.MySqlLogger
{
    /// <summary>
    /// MySql目标配置
    /// </summary>
    public class MySqlLoggerTargetConfig : BatchTargetConfig<MySqlLoggerWriter>
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override string Type => "MySql";
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; } = "Server=127.0.0.1;Port=3306;Database=LogDB;Uid=root;Pwd=123456;";
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; } = "MateralLogger";
        /// <summary>
        /// 默认字段
        /// </summary>
        public static List<MySqlDBFiled> DefaultFileds { get; } =
        [
            new() { Name = "ID", Type = "char(36)", Value = "${LogID}", PK = true },
            new() { Name = "CreateTime", Type = "datetime", Value = "${DateTime}", Index = "DESC", IsNull = false },
            new() { Name = "Level", Type = "varchar(50)", Value = "${Level}" },
            new() { Name = "ProgressID", Type = "varchar(20)", Value = "${ProgressID}" },
            new() { Name = "ThreadID", Type = "varchar(20)", Value = "${ThreadID}" },
            new() { Name = "Scope", Type = "varchar(100)", Value = "${Scope}" },
            new() { Name = "MachineName", Type = "varchar(100)", Value = "${MachineName}" },
            new() { Name = "CategoryName", Type = "varchar(100)", Value = "${CategoryName}" },
            new() { Name = "Application", Type = "varchar(50)", Value = "${Application}" },
            new() { Name = "Message", Type = "longtext", Value = "${Message}" },
            new() { Name = "Exception", Type = "longtext", Value = "${Exception}" }
        ];
        /// <summary>
        /// 字段
        /// </summary>
        public List<MySqlDBFiled> Fileds { get; set; } = [];
    }
}