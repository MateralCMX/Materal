using Materal.Abstractions;
using Materal.Extensions;
using Materal.Utils.Cache;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Materal.EventBus.RabbitMQ
{
    internal class RabbitMQEventBus : BaseEventBus
    {
        private readonly IOptionsMonitor<RabbitMQEventBusOptions> _options;
        private readonly ICacheHelper _cacheHelper;
        private readonly ConcurrentDictionary<string, IChannel> _consumerChannels = [];
        [AllowNull]
        private IConnection _connection;
        [AllowNull]
        private IChannel _publishChannel;
        public RabbitMQEventBus(IOptionsMonitor<RabbitMQEventBusOptions> options, IServiceProvider serviceProvider, ICacheHelper cacheHelper, ILogger<RabbitMQEventBus>? logger = null) : base(serviceProvider, logger)
        {
            _options = options;
            _cacheHelper = cacheHelper;
            _ = ConnectionRabbitMQServer();
        }
        #region 处理连接
        private async Task ConnectionRabbitMQServer()
        {
            ConnectionFactory factory = new()
            {
                HostName = _options.CurrentValue.HostName,
                Port = _options.CurrentValue.Port,
                UserName = _options.CurrentValue.UserName,
                Password = _options.CurrentValue.Password,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(_options.CurrentValue.RetryInterval),
                MaxInboundMessageBodySize = _options.CurrentValue.MaxMessageBodySize,
                ClientProvidedName = _options.CurrentValue.ClientName
            };
            AsyncRetryPolicy retryPolicy = Policy.Handle<Exception>(ex =>
            {
                Logger?.LogWarning($"连接RabbitMQ服务[{factory.HostName}:{factory.Port}]失败,{_options.CurrentValue.RetryInterval}秒后重试.");
                return true;
            }).WaitAndRetryForeverAsync(m => TimeSpan.FromSeconds(_options.CurrentValue.RetryInterval));
            await retryPolicy.ExecuteAsync(async () =>
            {
                Logger?.LogInformation($"正在连接RabbitMQ服务[{factory.HostName}:{factory.Port}]...");
                _connection = await factory.CreateConnectionAsync();
                _connection.ConnectionShutdownAsync += Connection_ConnectionShutdownAsync;
                _connection.ConnectionRecoveryErrorAsync += Connection_ConnectionRecoveryErrorAsync;
                _connection.RecoverySucceededAsync += Connection_RecoverySucceededAsync;
                Logger?.LogInformation($"RabbitMQ服务[{factory.HostName}:{factory.Port}]已连接");
                _publishChannel = await _connection.CreateChannelAsync();
            });
            await AutoSubscribeAsync();
        }
        private async Task Connection_RecoverySucceededAsync(object sender, AsyncEventArgs @event)
        {
            if (sender is not IConnection connection) return;
            Logger?.LogInformation($"RabbitMQ服务[{connection.Endpoint.HostName}:{connection.Endpoint.Port}]已连接");
            await Task.CompletedTask;
        }
        private async Task Connection_ConnectionRecoveryErrorAsync(object sender, ConnectionRecoveryErrorEventArgs @event)
        {
            if (sender is not IConnection connection) return;
            Logger?.LogWarning(@event.Exception, $"RabbitMQ服务[{connection.Endpoint.HostName}:{connection.Endpoint.Port}]连接失败,将在{_options.CurrentValue.RetryInterval}秒后重连");
            await Task.CompletedTask;
        }
        private async Task Connection_ConnectionShutdownAsync(object sender, ShutdownEventArgs @event)
        {
            if (sender is not IConnection connection) return;
            if (@event.Initiator == ShutdownInitiator.Application)
            {
                Logger?.LogInformation($"RabbitMQ服务[{connection.Endpoint.HostName}:{connection.Endpoint.Port}]已关闭");
            }
            else
            {
                Logger?.LogWarning(@event.Exception, $"RabbitMQ服务[{connection.Endpoint.HostName}:{connection.Endpoint.Port}]连接断开,将在{_options.CurrentValue.RetryInterval}秒后重连");
            }
            await Task.CompletedTask;
        }
        #endregion
        public override async Task PublishAsync(IEvent @event)
        {
            Type eventType = @event.GetType();
            if (string.IsNullOrWhiteSpace(eventType.FullName)) throw new EventBusException("获取事件名称失败");
            string eventJson = @event.ToJson();
            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(eventJson);
            string exchangeName = _options.CurrentValue.GetTrueExchangeName();
            string routingKey = eventType.FullName;
            AsyncRetryPolicy retryPolicy = Policy.Handle<Exception>(ex =>
            {
                Logger?.LogWarning(ex, $"发布事件[{eventType.FullName}]失败,{_options.CurrentValue.RetryInterval}秒后重新发布");
                return true;
            }).WaitAndRetryForeverAsync(m => TimeSpan.FromSeconds(_options.CurrentValue.RetryInterval));
            await retryPolicy.ExecuteAsync(async () =>
            {
                Logger?.LogInformation($"正在发布事件[{eventType.FullName}]...");
                await _publishChannel.BasicPublishAsync(exchangeName, routingKey, false, messageBodyBytes);
                Logger?.LogInformation($"已发布事件[{eventType.FullName}]");
            });
        }
        public override async Task SubscribeAsync(Type eventType, Type eventHandlerType)
        {
            if (string.IsNullOrWhiteSpace(eventType.FullName)) throw new EventBusException("获取事件名称失败");
            string queueName = _options.CurrentValue.GetTrueQueueName(eventHandlerType, ServiceProvider);
            string exchangeName = _options.CurrentValue.GetTrueExchangeName();
            string routingKey = eventType.FullName;
            AsyncRetryPolicy retryPolicy = Policy.Handle<Exception>(ex =>
            {
                Logger?.LogWarning(ex, $"订阅事件[{eventType.FullName}]失败,{_options.CurrentValue.RetryInterval}秒后重新订阅");
                return true;
            }).WaitAndRetryForeverAsync(m => TimeSpan.FromSeconds(_options.CurrentValue.RetryInterval));
            await retryPolicy.ExecuteAsync(async () =>
            {
                if (!_consumerChannels.TryGetValue(queueName, out IChannel? channel))
                {
                    try
                    {
                        channel = await _connection.CreateChannelAsync();
                        await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct, true, false);
                        await channel.QueueDeclareAsync(queueName, true, false, false, null);
                        AsyncEventingBasicConsumer consumer = new(channel);
                        consumer.ReceivedAsync += Consumer_ReceivedAsync;
                        await channel.BasicConsumeAsync(queueName, false, consumer);
                        _consumerChannels.TryAdd(queueName, channel);
                    }
                    catch
                    {
                        if (_consumerChannels.ContainsKey(queueName))
                        {
                            _consumerChannels.Remove(queueName, out _);
                        }
                        if (channel is not null)
                        {
                            if (channel.IsOpen)
                            {
                                await channel.CloseAsync();
                            }
                            await channel.DisposeAsync();
                        }
                        throw;
                    }
                }
                await channel.QueueBindAsync(queueName, exchangeName, routingKey, null);
                Subscribe(routingKey, eventType, eventHandlerType);
            });
        }
        private async Task Consumer_ReceivedAsync(object sender, BasicDeliverEventArgs args)
        {
            if (sender is not AsyncEventingBasicConsumer consumer) return;
            string routingKey = args.RoutingKey;
            while (_cacheHelper.KeyAny(routingKey)) { await Task.Delay(1000); }
            byte[] body = args.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);
            if (HandlerEvent(routingKey, message))
            {
                await consumer.Channel.BasicAckAsync(args.DeliveryTag, false);
            }
            else
            {
                if (_options.CurrentValue.ErrorMeesageDiscard)
                {
                    Logger?.LogWarning($"事件[{routingKey}]处理失败,丢弃消息,消息体：\r\n{message}");
                    await consumer.Channel.BasicNackAsync(args.DeliveryTag, false, false);
                }
                else
                {
                    Logger?.LogWarning($"事件[{routingKey}]处理失败,回滚消息,消息体：\r\n{message}");
                    _cacheHelper.SetByAbsolute(args.RoutingKey, message, _options.CurrentValue.RetryInterval, DateTimeTypeEnum.Second);
                    await consumer.Channel.BasicNackAsync(args.DeliveryTag, false, true);
                }
            }
        }
        public override async ValueTask DisposeAsync()
        {
            foreach (KeyValuePair<string, IChannel> item in _consumerChannels)
            {
                await item.Value.CloseAsync();
                await item.Value.DisposeAsync();
            }
            await _connection.CloseAsync();
            await _connection.DisposeAsync();
            _consumerChannels.Clear();
            await base.DisposeAsync();
        }
    }
}
