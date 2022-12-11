using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.DR.Models
{
    public class ScheduleFlowModel : Schedule
    {
        /// <summary>
        /// 认证码
        /// </summary>
        public Guid AuthenticationCode { get; set; } = Guid.NewGuid();
    }
}
