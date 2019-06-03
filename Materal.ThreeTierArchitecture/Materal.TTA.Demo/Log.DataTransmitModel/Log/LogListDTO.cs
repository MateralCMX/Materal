namespace Log.DataTransmitModel.Log
{
    /// <summary>
    /// 日志列表数据传输模型
    /// </summary>
    public class LogListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string Application { get; set; }
        /// <summary>
        /// 日志等级
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }
}
