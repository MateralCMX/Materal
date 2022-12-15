using RabbitMQ.Client;

namespace Materal.TFMS.Demo.Core
{
    public static class ConnectionHelper
    {
        public const string ExchangeName = "MateralTFMSDemoExchangeName";
        public static ConnectionFactory GetConnectionFactory()
        {
            return new ConnectionFactory
            {
                HostName = "175.27.194.19",
                Port = 5672,
                DispatchConsumersAsync = true,
                UserName = "GDB",
                Password = "GDB2022"
            };
        }
    }
}
