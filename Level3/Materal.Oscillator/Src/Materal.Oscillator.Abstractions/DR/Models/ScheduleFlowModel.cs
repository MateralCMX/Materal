using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.Abstractions.DR.Models
{
    /// <summary>
    /// 调度流程模型
    /// </summary>
    public class ScheduleFlowModel : Schedule
    {
        /// <summary>
        /// 认证码
        /// </summary>
        public Guid AuthenticationCode { get; set; } = Guid.NewGuid();
    }
}
