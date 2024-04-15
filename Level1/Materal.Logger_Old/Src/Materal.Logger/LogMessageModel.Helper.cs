namespace Materal.Logger
{
    /// <summary>
    /// 日志消息模型
    /// </summary>
    public partial class LogMessageModel
    {
        /// <summary>
        /// 通过日志写入器模型转换为日志消息模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static LogMessageModel Pass(LoggerWriterModel model)
        {
            return new LogMessageModel
            {
                ID = model.ID,
                CreateTime = model.CreateTime,
                Level = model.LogLevel,
                ProgressID = LoggerWriterHelper.GetProgressID(),
                ThreadID = model.ThreadID,
                Scope = model.Scope?.ScopeName,
                MachineName = LoggerWriterHelper.MachineName,
                CategoryName = model.CategoryName,
                Application = model.Config.Application,
                Message = LoggerWriterHelper.FormatText(model.Message, model),
                Exception = model.Exception?.GetErrorMessage(),
                ScopeData = model.Scope?.AdvancedScope?.ScopeData
            };
        }
    }
}
