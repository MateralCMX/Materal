using Materal.TFMS.EventBus.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace Materal.TFMS.EventBus.RabbitMQ
{
    /// <summary>
    /// RabbitMQ事件总线
    /// </summary>
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        /// <summary>
        /// 交换机名称
        /// </summary>
        private readonly string _exchangeName;
        /// <summary>
        /// 队列名称
        /// </summary>
        private string _queueName;
        /// <summary>
        /// RabbitMQ持久连接
        /// </summary>
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger<EventBusRabbitMQ>? _logger;
        /// <summary>
        /// 订阅管理器
        /// </summary>
        private readonly IEventBusSubscriptionsManager _subsManager;
        /// <summary>
        /// 依赖注入容器
        /// </summary>
        private readonly IServiceProvider _service;
        /// <summary>
        /// 消费通道
        /// </summary>
        private IModel? _consumerChannel;
        /// <summary>
        /// RabbitMQ事件总线
        /// </summary>
        /// <param name="persistentConnection">RabbitMQ持久连接</param>
        /// <param name="logger">日志</param>
        /// <param name="service">生命周期</param>
        /// <param name="subsManager">订阅管理器</param>
        /// <param name="queueName">队列名称</param>
        /// <param name="exchangeName">交换机名称</param>
        /// <param name="autoListening"></param>
        public EventBusRabbitMQ(IRabbitMQPersistentConnection persistentConnection, IServiceProvider service, IEventBusSubscriptionsManager subsManager, string queueName, string exchangeName = "MateralTFMSEventBusExchange", bool autoListening = true, ILogger<EventBusRabbitMQ>? logger = null)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger;
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
            _queueName = queueName;
            _service = service;
            _exchangeName = exchangeName;
            _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
            Task task = Task.Run(async () =>
            {
                _consumerChannel = await CreateConsumerChannelAsync();
            });
            task.Wait();
            if (autoListening)
            {
                StartListening();
            }
        }
        public void StartListening()
        {
            StartBasicConsume();
        }
        public async Task PublishAsync(IntegrationEvent @event)
        {
            using IDisposable? scope = _logger?.BeginScope("Publish");
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            RetryPolicy policy = Policy.Handle<BrokerUnreachableException>().Or<SocketException>()
                .WaitAndRetryForever(count => TimeSpan.FromSeconds(5), (ex, time) =>
                {
                    _logger?.LogError(ex, "发布事件失败: EventId={EventId} Timeout={Timeout} ({ExceptionMessage})", @event.ID, $"{time.TotalSeconds:n1}", ex.Message);
                });
            string eventName = @event.GetType().Name;
            _logger?.LogInformation("创建RabbitMQ发布事件通道: {EventId} ({EventName})", @event.ID, eventName);
            IModel channel = await _persistentConnection.CreateModelAsync();
            channel.ExchangeDeclare(_exchangeName, "direct");
            string message = JsonConvert.SerializeObject(@event);
            byte[] body = Encoding.UTF8.GetBytes(message);
            policy.Execute(() =>
            {
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2;
                _logger?.LogInformation("发布事件: {EventId}", @event.ID);
                channel.BasicPublish(_exchangeName, eventName, properties, body);
                channel.Dispose();
            });
        }
        public async Task SubscribeAsync<T, THandler>() where T : IntegrationEvent where THandler : IIntegrationEventHandler<T>
        {
            using IDisposable? scope = _logger?.BeginScope("Subscribe");
            string eventName = _subsManager.GetEventKey<T>();
            await DoInternalSubscriptionAsync(eventName);
            _logger?.LogInformation("订阅事件{EventName},处理器为{EventHandler}", eventName, typeof(THandler).GetGenericTypeName());
            _subsManager.AddSubscription<T, THandler>();
        }
        public void Unsubscribe<T, THandler>() where T : IntegrationEvent where THandler : IIntegrationEventHandler<T>
        {
            using IDisposable? scope = _logger?.BeginScope("Unsubscribe");
            string eventName = _subsManager.GetEventKey<T>();
            _logger?.LogInformation("取消订阅事件{EventName}", eventName);
            _subsManager.RemoveSubscription<T, THandler>();
        }
        public async Task SubscribeDynamicAsync<THandler>(string eventName) where THandler : IDynamicIntegrationEventHandler
        {
            using IDisposable? scope = _logger?.BeginScope("Subscribe");
            _logger?.LogInformation("订阅动态事件{EventName},处理器为{EventHandler}", eventName, typeof(THandler).GetGenericTypeName());
            await DoInternalSubscriptionAsync(eventName);
            _subsManager.AddDynamicSubscription<THandler>(eventName);
        }
        public void UnsubscribeDynamic<THandler>(string eventName) where THandler : IDynamicIntegrationEventHandler
        {
            using IDisposable? scope = _logger?.BeginScope("Unsubscribe");
            _logger?.LogInformation("取消订阅动态事件{EventName}", eventName);
            _subsManager.RemoveDynamicSubscription<THandler>(eventName);
        }
        public void Dispose()
        {
            if(_consumerChannel != null)
            {
                _consumerChannel.Dispose();
                GC.SuppressFinalize(this);
            }
            _subsManager.Clear();
        }
        #region 私有方法
        /// <summary>
        /// 创建消费通道
        /// </summary>
        /// <returns></returns>
        private async Task<IModel> CreateConsumerChannelAsync()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            _logger?.LogInformation("创建RabbitMQ消费通道...");
            IModel channel = await _persistentConnection.CreateModelAsync();
            channel.ExchangeDeclare(_exchangeName, "direct");
            channel.QueueDeclare(_queueName, true, false, false, null);
            channel.CallbackException += async (sender, ea) =>
            {
                _logger?.LogWarning(ea.Exception, "重新创建RabbitMQ消费通道...");
                _consumerChannel?.Dispose();
                _consumerChannel = await CreateConsumerChannelAsync();
                StartBasicConsume();
            };
            return channel;
        }
        /// <summary>
        /// 开始消费
        /// </summary>
        private void StartBasicConsume()
        {
            _logger?.LogInformation("RabbitMQ开始消费消息...");
            if (_consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);
                consumer.Received += Consumer_ReceivedAsync;
                _consumerChannel.BasicConsume(_queueName, false, consumer);
            }
            else
            {
                throw new NullReferenceException("未创建消费通道");
            }
        }
        /// <summary>
        /// 消费者接收到消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        private async Task Consumer_ReceivedAsync(object sender, BasicDeliverEventArgs eventArgs)
        {
            using IDisposable? scope = _logger?.BeginScope("Received");
            string eventName = eventArgs.RoutingKey;
            string message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            try
            {
                if (message.ToLowerInvariant().Contains("throw-fake-exception"))
                {
                    throw new InvalidOperationException($"异常请求: \"{message}\"");
                }
                if (await TryProcessEvent(eventName, message))
                {
                    _consumerChannel?.BasicAck(eventArgs.DeliveryTag, false);
                }
                else
                {
                    _logger?.LogError(new MateralTFMSException("消息处理失败"), "错误消息: \"{Message}\"", message);
                    _consumerChannel?.BasicReject(eventArgs.DeliveryTag, !MateralTFMSRabbitMQConfig.EventErrorConfig.Discard);
                }
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, "错误消息: \"{Message}\"", message);
                _consumerChannel?.BasicReject(eventArgs.DeliveryTag, !MateralTFMSRabbitMQConfig.EventErrorConfig.Discard);
            }
        }
        /// <summary>
        /// 处理错误消息
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        private static async Task HandlerErrorMessageAsync(Exception exception, Func<Task> handler)
        {
            var isOK = false;
            if (MateralTFMSRabbitMQConfig.EventErrorConfig.IsRetry)
            {
                for (var i = 0; i < MateralTFMSRabbitMQConfig.EventErrorConfig.RetryNumber; i++)
                {
                    try
                    {
                        await handler();
                        isOK = true;
                        break;
                    }
                    catch (Exception)
                    {
                        isOK = false;
                    }
                }
            }
            if (!isOK)
            {
                throw new MateralTFMSException("消息处理失败", exception);
            }
        }
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task<bool> TryProcessEvent(string eventName, string message)
        {
            using IDisposable? scope = _logger?.BeginScope("ProcessEvent");
            _logger?.LogInformation("处理事件: {eventName}", eventName);
            var result = false;
            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                IEnumerable<SubscriptionInfo> subscriptions = _subsManager.GetHandlersForEvent(eventName);
                foreach (SubscriptionInfo subscription in subscriptions)
                {
                    using IServiceScope serviceScope = _service.CreateScope();
                    IServiceProvider service = serviceScope.ServiceProvider;
                    if (subscription.IsDynamic)
                    {
                        if (service.GetService(subscription.HandlerType) is not IDynamicIntegrationEventHandler handler) continue;
                        dynamic eventData = JObject.Parse(message);
                        try
                        {
                            await handler.HandleAsync(eventData);
                        }
                        catch (Exception exception)
                        {
                            await HandlerErrorMessageAsync(exception, async () => await handler.HandleAsync(eventData));
                        }
                    }
                    else
                    {
                        object? handler = service.GetService(subscription.HandlerType);
                        if (handler == null) continue;
                        Type? eventType = _subsManager.GetEventTypeByName(eventName);
                        if (eventType == null) continue;
                        object? integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                        Type concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        MethodInfo? handlerMethodInfo = concreteType.GetMethod("HandleAsync");
                        if (handlerMethodInfo == null || handlerMethodInfo.ReturnType != typeof(Task)) continue;
                        try
                        {
                            object? handlerObj = handlerMethodInfo.Invoke(handler, new[] { integrationEvent });
                            if (handlerObj is Task handlerTask)
                            {
                                await handlerTask;
                            }
                        }
                        catch (Exception exception)
                        {
                            await HandlerErrorMessageAsync(exception, async () =>
                            {
                                object? handlerObj = handlerMethodInfo.Invoke(handler, new[] { integrationEvent });
                                if (handlerObj is Task handlerTask)
                                {
                                    await handlerTask;
                                }
                            });
                        }
                    }
                    _logger?.LogTrace("处理器{Name}执行完毕", subscription.HandlerType.Name);
                }
                result= true;
            }
            else
            {
                _logger?.LogError("未找到订阅事件: {eventName}", eventName);
            }
            return result;
        }
        /// <summary>
        /// 事件移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventName"></param>
        private async void SubsManager_OnEventRemoved(object? sender, string eventName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            using IModel channel = await _persistentConnection.CreateModelAsync();
            channel.QueueUnbind(_queueName, _exchangeName, eventName);
            if (!_subsManager.IsEmpty) return;
            _queueName = string.Empty;
            _consumerChannel?.Close();
        }
        /// <summary>
        /// 内部订阅
        /// </summary>
        /// <param name="eventName"></param>
        private async Task DoInternalSubscriptionAsync(string eventName)
        {
            bool containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (containsKey) return;
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            using IModel channel = await _persistentConnection.CreateModelAsync();
            channel.QueueBind(_queueName, _exchangeName, eventName);
        }
        #endregion
    }
}
