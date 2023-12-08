using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.OcelotExtension.ConfigModel
{
    /// <summary>
    /// 服务质量配置模型
    /// </summary>
    public class QoSOptionsModel
    {
        /// <summary>
        /// 熔断时间(ms)
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public int DurationOfBreak { get; set; } = 60000;
        /// <summary>
        /// 熔断前允许错误次数
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public int ExceptionsAllowedBeforeBreaking { get; set; } = 3;
        /// <summary>
        /// 超时时间(ms)
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public int TimeoutValue { get; set; } = 2000;
    }
}
