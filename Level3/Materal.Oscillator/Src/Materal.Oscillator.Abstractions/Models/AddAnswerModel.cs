using Materal.Oscillator.Abstractions.Answers;

namespace Materal.Oscillator.Abstractions.Models
{
    /// <summary>
    /// 添加响应模型
    /// </summary>
    public class AddAnswerModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称长度大于100")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 任务事件
        /// </summary>
        [Required(ErrorMessage = "任务事件为空"), StringLength(40, ErrorMessage = "任务事件长度大于40")]
        public string WorkEvent { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(400, ErrorMessage = "描述长度大于400")]
        public string? Description { get; set; }
        /// <summary>
        /// 响应数据
        /// </summary>
        [Required(ErrorMessage = "响应数据为空")]
        public IAnswer AnswerData { get; set; } = new NoneAnswer();
        /// <summary>
        /// 响应类型
        /// </summary>
        public string AnswerType => AnswerData.GetType().Name;
    }
}
