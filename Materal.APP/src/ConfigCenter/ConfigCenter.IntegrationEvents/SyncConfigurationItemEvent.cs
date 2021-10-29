using ConfigCenter.Environment.PresentationModel.ConfigurationItem;
using Materal.TFMS.EventBus;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.IntegrationEvents
{
    public class SyncConfigurationItemEvent : IntegrationEvent
    {
        /// <summary>
        /// 目标API
        /// </summary>
        [Required(ErrorMessage = "目标API不能为空")]
        public string[] TargetsAPI { get; set; }
        /// <summary>
        /// 要同步的配置项
        /// </summary>
        [Required(ErrorMessage = "要同步的配置项不能为空")]
        public List<AddConfigurationItemRequestModel> ConfigurationItem { get; set; }
    }
}
