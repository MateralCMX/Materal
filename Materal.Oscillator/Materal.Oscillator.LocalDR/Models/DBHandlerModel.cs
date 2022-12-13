using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.DR.Domain;
using Materal.Oscillator.DR.Models;

namespace Materal.Oscillator.LocalDR.Models
{
    public class DBHandlerModel
    {
        /// <summary>
        /// 操作对象
        /// </summary>
        public Flow? Flow { get; set; }
        /// <summary>
        /// 调度器
        /// </summary>
        public ScheduleFlowModel? ScheduleFlow { get; set; }
        /// <summary>
        /// 调度器任务
        /// </summary>
        public ScheduleWorkView? ScheduleWork { get; set; }
        /// <summary>
        /// 任务返回
        /// </summary>
        public string? WorkResult { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public DBHandlerTypeEnum Type { get; set; }
    }
}
