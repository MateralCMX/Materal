using Materal.Model;

namespace Materal.Oscillator.Abstractions.Models.Schedule
{
    public class QueryScheduleModel : QueryScheduleManagerModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Equal]
        public Guid? ID { get; set; }
        /// <summary>
        /// 业务领域
        /// </summary>
        [Equal]
        public string? Territory { get; set; }
    }
}
