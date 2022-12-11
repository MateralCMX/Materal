using Materal.Model;

namespace Materal.Oscillator.DR.Models
{
    public class QueryFlowModel : PageRequestModel
    {
        /// <summary>
        /// 认证码
        /// </summary>
        [Equal]
        public Guid? AuthenticationCode { get; set; }
        /// <summary>
        /// 作业Key
        /// </summary>
        [Equal]
        public string? JobKey { get; set; }
        /// <summary>
        /// 调度器唯一标识
        /// </summary>
        [Equal]
        public Guid? ScheduleID { get; set; }
    }
}
