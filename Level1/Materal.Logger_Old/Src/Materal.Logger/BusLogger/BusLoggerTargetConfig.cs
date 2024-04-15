namespace Materal.Logger.BusLogger
{
    /// <summary>
    /// 总线日期记录器目标配置
    /// </summary>
    public class BusLoggerTargetConfig : TargetConfig<BusLoggerWriter>
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override string Type => "Bus";
    }
}
