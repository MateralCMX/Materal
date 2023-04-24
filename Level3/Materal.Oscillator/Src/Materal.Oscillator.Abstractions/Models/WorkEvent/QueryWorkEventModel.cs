using Materal.Utils.Model;

namespace Materal.Oscillator.Abstractions.Models.WorkEvent
{
    public class QueryWorkEventModel : QueryWorkEventManagerModel
    {
        /// <summary>
        /// 名称
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
