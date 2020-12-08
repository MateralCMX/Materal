using RabbitMQ.Client;

namespace Materal.TFMS.Demo.Core
{
    public static class ConnectionHelper
    {
        public const string ExchangeName = "MateralTFMSDemoExchangeName";
        //public const string ExchangeName = "IntegratedPlatformEventBusDEVExchange";
        public static ConnectionFactory GetConnectionFactory()
        {
            return new ConnectionFactory
            {
                HostName = "116.55.251.31",
                Port = 5672,
                DispatchConsumersAsync = true,
                UserName = "guest",
                Password = "guest"
            };
            //return new ConnectionFactory
            //{
            //    HostName = "39.130.168.122",
            //    Port = 5672,
            //    DispatchConsumersAsync = true,
            //    UserName = "Admin",
            //    Password = "KMJQ@RabbitMQ2020"
            //};
        }
    }
}
