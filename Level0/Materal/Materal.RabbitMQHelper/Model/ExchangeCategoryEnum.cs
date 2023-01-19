using System.ComponentModel;

namespace Materal.RabbitMQHelper.Model
{
    public enum ExchangeCategoryEnum
    {
        [Description("direct")]
        Direct,
        [Description("fanout")]
        FanOut,
        [Description("headers")]
        Headers,
        [Description("topic")]
        Topic
    }
}
