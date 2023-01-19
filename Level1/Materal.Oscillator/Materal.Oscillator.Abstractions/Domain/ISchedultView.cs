namespace Materal.Oscillator.Abstractions.Domain
{
    public interface ISchedultView
    {
        /// <summary>
        /// 调度器名称
        /// </summary>
        public string ScheduleName { get; set; }
        /// <summary>
        /// 业务领域
        /// </summary>
        public string Territory { get; set; }
    }
}
