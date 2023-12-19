namespace Materal.MergeBlock.Abstractions.Config
{
    /// <summary>
    /// 异常配置
    /// </summary>
    public class ExceptionConfig
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
