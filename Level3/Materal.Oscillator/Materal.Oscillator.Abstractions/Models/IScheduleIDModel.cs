namespace Materal.Oscillator.Abstractions.Models
{
    public interface IScheduleIDModel
    {
        /// <summary>
        /// 调度器唯一标识
        /// </summary>
        public Guid? ScheduleID { get; set; }
    }
}
