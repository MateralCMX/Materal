namespace Materal.Oscillator.Abstractions.Domain
{
    /// <summary>
    /// 调度器任务映射
    /// </summary>
    [Serializable]
    public class ScheduleWork : BaseDomain, IDomain
    {
        /// <summary>
        /// 调度器唯一标识
        /// </summary>
        [Required(ErrorMessage = "调度器唯一标识为空")]
        public Guid ScheduleID { get; set; }
        /// <summary>
        /// 任务唯一标识
        /// </summary>
        [Required(ErrorMessage = "任务唯一标识为空")]
        public Guid WorkID { get; set; }
        /// <summary>
        /// 位序
        /// </summary>
        [Required(ErrorMessage = "位序为空")]
        public int Index { get; set; }
        /// <summary>
        /// 成功事件
        /// </summary>
        [Required(ErrorMessage = "成功事件为空"), StringLength(100, ErrorMessage = "成功事件长度大于100")]
        public string SuccessEvent { get; set; } = string.Empty;
        /// <summary>
        /// 失败事件
        /// </summary>
        [Required(ErrorMessage = "失败事件为空"), StringLength(100, ErrorMessage = "失败事件长度大于100")]
        public string FailEvent { get; set; } = string.Empty;
    }
}
