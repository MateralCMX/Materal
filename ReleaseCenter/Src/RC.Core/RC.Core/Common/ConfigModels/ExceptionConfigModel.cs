namespace RC.Core.Common.ConfigModels
{
    /// <summary>
    /// 异常配置模型
    /// </summary>
    public class ExceptionConfigModel
    {
        /// <summary>
        /// 显示异常
        /// </summary>
        public bool ShowException { get; set; } = false;
        /// <summary>
        /// 异常消息
        /// </summary>
        public string ErrorMessage { get; set; } = "服务器出错了！请联系管理员。";
    }
}
