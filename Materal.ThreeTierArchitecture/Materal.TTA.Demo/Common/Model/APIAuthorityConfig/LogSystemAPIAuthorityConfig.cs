namespace Common.Model.APIAuthorityConfig
{
    /// <summary>
    /// 日志子系统API权限配置
    /// </summary>
    public class LogSystemAPIAuthorityConfig
    {
        #region Log
        /// <summary>
        /// 日志操作
        /// </summary>
        public const string LogOperationCode = "LogOperation";
        /// <summary>
        /// 查询敏感日志
        /// </summary>
        public const string QuerySensitiveLogCode = "QuerySensitiveLog";
        /// <summary>
        /// 查询错误日志
        /// </summary>
        public const string QueryErrorLogCode = "QueryErrorLog";
        /// <summary>
        /// 查询访问日志
        /// </summary>
        public const string QueryAccessLogCode = "QueryAccessLog";
        /// <summary>
        /// 日志配置
        /// </summary>
        public const string SettingLogCode = "SettingLog";
        #endregion
    }
}
