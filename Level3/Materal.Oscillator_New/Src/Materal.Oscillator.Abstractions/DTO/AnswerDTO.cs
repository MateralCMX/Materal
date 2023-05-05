using Materal.Oscillator.Abstractions.Answers;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Helper;
using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.DTO
{
    /// <summary>
    /// 响应数据传输模型
    /// </summary>
    public class AnswerDTO : BaseDTO
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称长度大于100")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 调度器唯一标识
        /// </summary>
        [Required(ErrorMessage = "调度器唯一标识为空")]
        public Guid ScheduleID { get; set; }
        /// <summary>
        /// 任务事件
        /// </summary>
        [Required(ErrorMessage = "任务事件为空"), StringLength(40, ErrorMessage = "任务事件长度大于40")]
        public string WorkEvent { get; set; } = string.Empty;
        /// <summary>
        /// 位序
        /// </summary>
        [Required(ErrorMessage = "位序为空")]
        public int Index { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(400, ErrorMessage = "描述长度大于400")]
        public string? Description { get; set; }
        /// <summary>
        /// 响应数据类型
        /// </summary>
        public string AnswerType => AnswerData.GetType().Name;
        /// <summary>
        /// 响应数据
        /// </summary>
        [Required(ErrorMessage = "响应数据为空")]
        public IAnswer AnswerData { get; set; } = new NoneAnswer();
        /// <summary>
        /// 构造方法
        /// </summary>
        public AnswerDTO() : base() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        public AnswerDTO(Answer domain) : base(domain)
        {
            domain.CopyProperties(this, nameof(AnswerData));
            AnswerData = OscillatorConvertHelper.ConvertToInterface<IAnswer>(domain.AnswerType, domain.AnswerData);
        }
    }
}
