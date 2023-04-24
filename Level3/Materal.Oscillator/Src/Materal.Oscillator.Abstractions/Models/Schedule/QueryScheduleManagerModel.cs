using Materal.Utils.Model;

namespace Materal.Oscillator.Abstractions.Models.Schedule
{
    public class QueryScheduleManagerModel : PageRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string? Name { get; set; }
        /// <summary>
        /// 启用标识
        /// </summary>
        [Equal]
        public bool? Enable { get; set; }
        /// <summary>
        /// 唯一标识组
        /// </summary>
        [Contains]
        public Guid[]? IDs { get; set; }
    }
}
