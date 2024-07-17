namespace Materal.Oscillator
{
    internal class RestorerContext
    {
        /// <summary>
        /// 触发器名称
        /// </summary>
        public string TriggerName { get; set; } = string.Empty;
        /// <summary>
        /// Oscillator数据
        /// </summary>
        public string? OscillatorData { get; set; }
        /// <summary>
        /// 日志作用域
        /// </summary>
        public Dictionary<string, object?> LoggerScopeData { get; set; } = [];
        /// <summary>
        /// 耗时
        /// </summary>
        public long ElapsedMilliseconds { get; set; }
        public OscillatorJobStep Step { get; set; } = OscillatorJobStep.RunWork;
        public string? Exception { get; set; }
    }
}
