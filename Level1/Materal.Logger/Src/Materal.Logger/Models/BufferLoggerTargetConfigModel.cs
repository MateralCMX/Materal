namespace Materal.Logger.Models
{
    /// <summary>
    /// 流日志目标配置模型
    /// </summary>
    public abstract class BufferLoggerTargetConfigModel : LoggerTargetConfigModel
    {
        private int _bufferPushInterval = 1000;
        /// <summary>
        /// 缓冲推入间隔(ms)
        /// </summary>
        public virtual int BufferPushInterval
        {
            get => _bufferPushInterval;
            set => _bufferPushInterval = value < 500 ? 500 : value;
        }
        private int _bufferCount = 1000;
        /// <summary>
        /// 缓冲区数量
        /// </summary>
        public virtual int BufferCount
        {
            get => _bufferCount;
            set => _bufferCount = value < 2 ? 2 : value;
        }
    }
}
