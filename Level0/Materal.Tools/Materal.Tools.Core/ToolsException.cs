namespace Materal.Tools.Core
{
    /// <summary>
    /// 工具异常
    /// </summary>
    public class ToolsException : Exception
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ToolsException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public ToolsException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ToolsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
