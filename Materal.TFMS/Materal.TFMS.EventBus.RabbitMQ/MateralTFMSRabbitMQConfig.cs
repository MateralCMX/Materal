using Materal.TFMS.EventBus.RabbitMQ.Models;

namespace Materal.TFMS.EventBus.RabbitMQ
{
    public static class MateralTFMSRabbitMQConfig
    {
        public static EventErrorConfig EventErrorConfig { get; set; } = new EventErrorConfig();
    }
}
