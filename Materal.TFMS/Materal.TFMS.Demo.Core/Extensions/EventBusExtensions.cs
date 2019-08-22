using Materal.TFMS.EventBus;
using Materal.TFMS.EventBus.Extensions;
using Materal.TFMS.EventBus.RabbitMQ;
using Materal.TFMS.EventBus.RabbitMQ.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Materal.TFMS.Demo.Core.Extensions
{
    public static class EventBusExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            services.AddEventBusSubscriptionsManager().AddRabbitMQPersistentConnection();
            return services;
        }
    }
}
