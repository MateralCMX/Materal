using RabbitMQ.Client;

namespace Materal.APP.TFMS.Core
{
    public static class ConnectionHelper
    {
        public const string ExchangeName = "MateralTFMSDemoExchangeName";
        public static ConnectionFactory GetConnectionFactory()
        {
            return new ConnectionFactory
            {
                HostName = "127.0.0.1",
                Port = 5672,
                DispatchConsumersAsync = true,
                UserName = "admin",
                Password = "Materal@123456"
            };
        }
    }
}
