namespace Log.DataTransmitModel.Log
{
    /// <summary>
    /// 日志数据传输模型
    /// </summary>
    public class LogDTO : LogListDTO
    {
        /// <summary>
        /// 日志
        /// </summary>
        public string Logger { get; set; }
        /// <summary>
        /// 调用点
        /// </summary>
        public string Callsite { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        public string Exception { get; set; }
    }
}
