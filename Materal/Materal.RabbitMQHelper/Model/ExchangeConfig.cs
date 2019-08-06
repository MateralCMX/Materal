using System.Collections.Generic;
using Materal.Common;

namespace Materal.RabbitMQHelper.Model
{
    public class ExchangeConfig
    {
        /// <summary>
        /// 交换机名称
        /// </summary>
        public string ExchangeName { get; set; }
        /// <summary>
        /// 交换机类型
        /// </summary>
        public ExchangeCategoryEnum ExchangeCategory { get; set; } = ExchangeCategoryEnum.Direct;
        /// <summary>
        /// 交换机类型文本
        /// </summary>
        public string ExchangeCategoryString => ExchangeCategory.GetDescription();
    }
}
