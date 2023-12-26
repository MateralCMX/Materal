namespace Materal.Logger.ConfigModels
{
    /// <summary>
    /// 批量配置
    /// </summary>
    public class BatchConfig
    {
        /// <summary>
        /// 批量处理大小
        /// </summary>
        public int BatchSize { get; set; } = 2000;
        /// <summary>
        /// 推送间隔(秒)
        /// </summary>
        public int PushInterval { get; set; } = 2;
    }
}
