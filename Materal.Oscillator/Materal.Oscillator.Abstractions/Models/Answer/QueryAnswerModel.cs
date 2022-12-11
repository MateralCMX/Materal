using Materal.Model;

namespace Materal.Oscillator.Abstractions.Models.Answer
{
    public class QueryAnswerModel : QueryAnswerManagerModel
    {
        /// <summary>
        /// 调度器名称
        /// </summary>
        [Contains]
        public string? ScheduleName { get; set; }
        /// <summary>
        /// 业务领域
        /// </summary>
        [Equal]
        public string? Territory { get; set; }
    }
}
