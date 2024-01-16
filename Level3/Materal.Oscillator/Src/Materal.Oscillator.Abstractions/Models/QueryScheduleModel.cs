namespace Materal.Oscillator.Abstractions.Models
{
    /// <summary>
    /// 查询调度器模型
    /// </summary>
    public class QueryScheduleModel : PageRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string? Name { get; set; }
        /// <summary>
        /// 业务领域
        /// </summary>
        [Equal]
        public string? Territory { get; set; }
        /// <summary>
        /// 启用标识
        /// </summary>
        [Equal]
        public bool? Enable { get; set; }
    }
}
