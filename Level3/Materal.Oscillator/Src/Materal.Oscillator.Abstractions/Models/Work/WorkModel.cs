using Materal.Oscillator.Abstractions.Enums;
using Materal.Oscillator.Abstractions.Works;
using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models.Work
{
    public class WorkModel
    {
        /// <summary>
        /// 成功事件
        /// </summary>
        [Required(ErrorMessage = "成功事件为空"), StringLength(100, ErrorMessage = "成功事件长度大于100")]
        public string SuccessEvent { get; set; } = DefaultWorkEventEnum.Next.ToString();
        /// <summary>
        /// 失败事件
        /// </summary>
        [Required(ErrorMessage = "失败事件为空"), StringLength(100, ErrorMessage = "失败事件长度大于100")]
        public string FailEvent { get; set; } = DefaultWorkEventEnum.Fail.ToString();
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称长度大于100")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 任务数据
        /// </summary>
        [Required(ErrorMessage = "任务数据为空")]
        public IWork WorkData { get; set; } = new NoneWork();
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(400, ErrorMessage = "描述长度大于400")]
        public string? Description { get; set; }
    }
}
