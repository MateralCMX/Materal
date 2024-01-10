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
using System;
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
        public void StartListening() => StartBasicConsume();
        public async Task PublishAsync(IntegrationEvent @event)
        {
            using IDisposable? scope = _logger?.BeginScope("Publish");
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            string eventName = @event.GetType().Name;
            RetryPolicy policy = Policy.Handle<BrokerUnreachableException>().Or<SocketException>()
                .WaitAndRetryForever(count => TimeSpan.FromSeconds(5), (ex, time) =>
                {
                    _logger?.LogError(ex, "发布事件失败: {EventName}_{EventId} Timeout={Timeout} ({ExceptionMessage})", eventName, @event.ID, $"{time.TotalSeconds:n1}", ex.Message);
                });
            _logger?.LogDebug("创建RabbitMQ发布事件通道: {EventName}_{EventId}", eventName, @event.ID);
            IModel channel = await _persistentConnection.CreateModelAsync();
            channel.ExchangeDeclare(_exchangeName, "direct");
            string message = JsonConvert.SerializeObject(@event);
            byte[] body = Encoding.UTF8.GetBytes(message);
            policy.Execute(() =>
            {
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2;
                _logger?.LogInformation("发布事件: {EventName}_{EventId}", eventName, @event.ID);
                channel.BasicPublish(_exchangeName, eventName, properties, body);
                channel.Dispose();
            });
        }
        public async Task SubscribeAsync<T, THandler>() where T : IntegrationEvent where THandler : IIntegrationEventHandler<T> 
            => await SubscribeAsync(typeof(T), typeof(THandler));
        public async Task SubscribeAsync(Type eventType, Type eventHandlerType)
        {
            if (!eventType.IsAssignableTo<IntegrationEvent>()) throw new MateralTFMSException($"事件类型必须继承类{nameof(IntegrationEvent)}");
            if (!eventHandlerType.IsAssignableTo<IIntegrationEventHandler>()) throw new MateralTFMSException($"事件处理类型必须继承接口{nameof(IIntegrationEventHandler)}");
            using IDisposable? scope = _logger?.BeginScope("Subscribe");
            string eventName = _subsManager.GetEventKey(eventType);
            await DoInternalSubscriptionAsync(eventName);
            _logger?.LogInformation("订阅事件{EventName},处理器为{EventHandler}", eventName, eventHandlerType.GetGenericTypeName());
            _subsManager.AddSubscription(eventType, eventHandlerType);
        }
        public void Unsubscribe<T, THandler>() where T : IntegrationEvent where THandler : IIntegrationEventHandler<T>
            => Unsubscribe(typeof(T), typeof(THandler));
        public void Unsubscribe(Type eventType, Type eventHandlerType)
        {
            if (!eventType.IsAssignableTo<IntegrationEvent>()) throw new MateralTFMSException($"事件类型必须继承类{nameof(IntegrationEvent)}");
            if (!eventHandlerType.IsAssignableTo<IIntegrationEventHandler>()) throw new MateralTFMSException($"事件处理类型必须继承接口{nameof(IIntegrationEventHandler)}");
            using IDisposable? scope = _logger?.BeginScope("Unsubscribe");
            string eventName = _subsManager.GetEventKey(eventType);
            _logger?.LogInformation("取消订阅事件{EventName}", eventName);
            _subsManager.RemoveSubscription(eventType, eventHandlerType);
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
            if (_consumerChannel != null)
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
            _logger?.LogDebug("创建RabbitMQ消费通道...");
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
            if (_consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);
                consumer.Received += Consumer_ReceivedAsync;
                _consumerChannel.BasicConsume(_queueName, false, consumer);
                _logger?.LogInformation("TFMS启动完毕");
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
                if (message.ToLowerInvariant().Contains("throw-fake-exception")) throw new MateralTFMSException($"异常请求: \"{message}\"");
                if (!await TryProcessEvent(eventName, message)) throw new MateralTFMSException("消息处理失败");
                _consumerChannel?.BasicAck(eventArgs.DeliveryTag, false);
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, "错误消息: \"{Message}\"", message);
                _consumerChannel?.BasicReject(eventArgs.DeliveryTag, !MateralTFMSRabbitMQConfig.EventErrorConfig.Discard);
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
            _logger?.LogDebug("事件触发: {eventName}", eventName);
            if (!_subsManager.HasSubscriptionsForEvent(eventName))
            {
                _logger?.LogError("未找到订阅事件: {eventName}", eventName);
                return false;
            }
            IEnumerable<SubscriptionInfo> subscriptions = _subsManager.GetHandlersForEvent(eventName);
            bool result = await HandlerSubscriptionsAsync(subscriptions, eventName, message);
            return result;
        }
        /// <summary>
        /// 处理订阅组
        /// </summary>
        /// <param name="subscriptions"></param>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task<bool> HandlerSubscriptionsAsync(IEnumerable<SubscriptionInfo> subscriptions, string eventName, string message)
        {
            var result = true;
            foreach (SubscriptionInfo subscription in subscriptions)
            {
                bool handlerResult = await HandlerSubscriptionAsync(subscription, eventName, message);
                if (result && !handlerResult)
                {
                    result = false;
                }
            }
            return result;
        }
        /// <summary>
        /// 处理订阅
        /// </summary>
        /// <param name="subscription"></param>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task<bool> HandlerSubscriptionAsync(SubscriptionInfo subscription, string eventName, string message)
        {
            bool result;
            if (subscription.IsDynamic)
            {
                result = await HandlerDynamicSubscription(subscription, eventName, message);
            }
            else
            {
                result = await HandlerNormalSubscriptionAsync(subscription, eventName, message);
            }
            _logger?.LogDebug("处理器{Name}执行完毕", subscription.HandlerType.Name);
            return result;
        }
        /// <summary>
        /// 处理订阅
        /// </summary>
        /// <param name="HandlerFunc"></param>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task<bool> HandlerSubscriptionAsync(Func<Task> HandlerFunc, string eventName, string message)
        {
            if (MateralTFMSRabbitMQConfig.EventErrorConfig.IsRetry)
            {
                await Policy.Handle<Exception>()
                    .WaitAndRetryAsync(MateralTFMSRabbitMQConfig.EventErrorConfig.RetryNumber, i =>
                    {
                        _logger?.LogInformation($"[{i}/{MateralTFMSRabbitMQConfig.EventErrorConfig.RetryNumber}]将在{MateralTFMSRabbitMQConfig.EventErrorConfig.RetryInterval.TotalSeconds}秒后重新处理事件{eventName}\r\n{message}");
                        return MateralTFMSRabbitMQConfig.EventErrorConfig.RetryInterval;
                    })
                    .ExecuteAsync(HandlerFunc);
            }
            else
            {
                await HandlerFunc();
            }
            return true;
        }
        /// <summary>
        /// 处理正常订阅
        /// </summary>
        /// <param name="subscription"></param>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task<bool> HandlerNormalSubscriptionAsync(SubscriptionInfo subscription, string eventName, string message)
        {
            using IServiceScope serviceScope = _service.CreateScope();
            IServiceProvider service = serviceScope.ServiceProvider;
            object? handler = service.GetService(subscription.HandlerType);
            if (handler == null) return false;
            Type? eventType = _subsManager.GetEventTypeByName(eventName);
            if (eventType == null) return false;
            object? integrationEvent = JsonConvert.DeserializeObject(message, eventType);
            Type concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
            MethodInfo? handlerMethodInfo = concreteType.GetMethod("HandleAsync");
            if (handlerMethodInfo == null || handlerMethodInfo.ReturnType != typeof(Task)) return false;
            if(integrationEvent is IntegrationEvent @event)
            {
                _logger?.LogInformation("处理事件: {eventName}_{EventId}", eventName, @event.ID);
            }
            else
            {
                _logger?.LogInformation("处理事件: {eventName}", eventName);
            }
            async Task HandlerFunc()
            {
                if (handlerMethodInfo == null || handlerMethodInfo.ReturnType != typeof(Task)) return;
                object? handlerObj = handlerMethodInfo.Invoke(handler, new[] { integrationEvent });
                if (handlerObj is Task handlerTask)
                {
                    await handlerTask;
                }
            }
            return await HandlerSubscriptionAsync(HandlerFunc, eventName, message);
        }
        /// <summary>
        /// 处理动态订阅
        /// </summary>
        /// <param name="subscription"></param>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task<bool> HandlerDynamicSubscription(SubscriptionInfo subscription, string eventName, string message)
        {
            using IServiceScope serviceScope = _service.CreateScope();
            IServiceProvider service = serviceScope.ServiceProvider;
            if (service.GetService(subscription.HandlerType) is not IDynamicIntegrationEventHandler handler) return false;
            dynamic eventData = JObject.Parse(message);
            _logger?.LogInformation("处理事件: {eventName}", eventName);
            async Task HandlerFunc()
            {
                await handler.HandleAsync(eventData);
            }
            return await HandlerSubscriptionAsync(HandlerFunc, eventName, message);
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
