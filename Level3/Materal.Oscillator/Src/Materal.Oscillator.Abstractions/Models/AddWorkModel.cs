using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Abstractions.Models
{
    /// <summary>
    /// 添加任务模型
    /// </summary>
    public class AddWorkModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称长度大于100")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 任务类型
        /// </summary>
        [Required(ErrorMessage = "任务类型为空"), StringLength(100, ErrorMessage = "任务类型长度大于100")]
        public string WorkType => WorkData.GetType().Name;
        /// <summary>
        /// 任务数据
        /// </summary>
        [Required(ErrorMessage = "任务数据为空")]
        public IWorkData WorkData { get; set; } = new NoneWorkData();
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(400, ErrorMessage = "描述长度大于400")]
        public string? Description { get; set; }
    }
}
