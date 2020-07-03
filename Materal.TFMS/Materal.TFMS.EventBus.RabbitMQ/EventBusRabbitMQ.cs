using Materal.TFMS.EventBus.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        private readonly ILogger<EventBusRabbitMQ> _logger;
        /// <summary>
        /// 订阅管理器
        /// </summary>
        private readonly IEventBusSubscriptionsManager _subsManager;
        /// <summary>
        /// 重试次数
        /// </summary>
        private readonly int _retryCount;
        /// <summary>
        /// 依赖注入容器
        /// </summary>
        private readonly IServiceProvider _service;
        /// <summary>
        /// 消费通道
        /// </summary>
        private IModel _consumerChannel;
        /// <summary>
        /// RabbitMQ事件总线
        /// </summary>
        /// <param name="persistentConnection">RabbitMQ持久连接</param>
        /// <param name="logger">日志</param>
        /// <param name="service">生命周期</param>
        /// <param name="subsManager">订阅管理器</param>
        /// <param name="queueName">队列名称</param>
        /// <param name="exchangeName">交换机名称</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="autoListening"></param>
        public EventBusRabbitMQ(IRabbitMQPersistentConnection persistentConnection, ILogger<EventBusRabbitMQ> logger, IServiceProvider service, IEventBusSubscriptionsManager subsManager, string queueName, string exchangeName = "MateralTFMSEventBusExchange", int retryCount = 5, bool autoListening = true)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
            _queueName = queueName;
            _service = service;
            _retryCount = retryCount;
            _exchangeName = exchangeName;
            _consumerChannel = CreateConsumerChannel();
            _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
            if (autoListening)
            {
                StartListening();
            }
        }
        public void StartListening()
        {
            StartBasicConsume();
        }
        public void Publish(IntegrationEvent @event)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            RetryPolicy policy = Policy.Handle<BrokerUnreachableException>().Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger?.LogError(ex, "发布事件失败: EventId={EventId} Timeout={Timeout} ({ExceptionMessage})", @event.ID, $"{time.TotalSeconds:n1}", ex.Message);
                });
            string eventName = @event.GetType().Name;
            _logger?.LogTrace("创建RabbitMQ发布事件通道: {EventId} ({EventName})", @event.ID, eventName);
            IModel channel = _persistentConnection.CreateModel();
            channel.ExchangeDeclare(_exchangeName, "direct");
            string message = JsonConvert.SerializeObject(@event);
            byte[] body = Encoding.UTF8.GetBytes(message);
            policy.Execute(() =>
            {
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2;
                _logger?.LogTrace("发布事件: {EventId}", @event.ID);
                channel.BasicPublish(_exchangeName, eventName, properties, body);
                channel.Dispose();
            });
        }
        public void Subscribe<T, THandler>() where T : IntegrationEvent where THandler : IIntegrationEventHandler<T>
        {
            string eventName = _subsManager.GetEventKey<T>();
            DoInternalSubscription(eventName);
            _logger?.LogInformation("订阅事件{EventName},处理器为{EventHandler}", eventName, typeof(THandler).GetGenericTypeName());
            _subsManager.AddSubscription<T, THandler>();
        }
        public void Unsubscribe<T, THandler>() where T : IntegrationEvent where THandler : IIntegrationEventHandler<T>
        {
            string eventName = _subsManager.GetEventKey<T>();
            _logger?.LogInformation("取消订阅事件{EventName}", eventName);
            _subsManager.RemoveSubscription<T, THandler>();
        }
        public void SubscribeDynamic<THandler>(string eventName) where THandler : IDynamicIntegrationEventHandler
        {
            _logger?.LogInformation("订阅动态事件{EventName},处理器为{EventHandler}", eventName, typeof(THandler).GetGenericTypeName());
            DoInternalSubscription(eventName);
            _subsManager.AddDynamicSubscription<THandler>(eventName);
        }
        public void UnsubscribeDynamic<THandler>(string eventName) where THandler : IDynamicIntegrationEventHandler
        {
            _logger?.LogInformation("取消订阅动态事件{EventName}", eventName);
            _subsManager.RemoveDynamicSubscription<THandler>(eventName);
        }
        public void Dispose()
        {
            _consumerChannel?.Dispose();
            _subsManager.Clear();
        }
        #region 私有方法
        /// <summary>
        /// 创建消费通道
        /// </summary>
        /// <returns></returns>
        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            _logger?.LogTrace("创建RabbitMQ消费通道...");
            IModel channel = _persistentConnection.CreateModel();
            channel.ExchangeDeclare(_exchangeName, "direct");
            channel.QueueDeclare(_queueName, true, false, false, null);
            channel.CallbackException += (sender, ea) =>
            {
                _logger?.LogWarning(ea.Exception, "重新创建RabbitMQ消费通道...");
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
                StartBasicConsume();
            };
            return channel;
        }
        /// <summary>
        /// 开始消费
        /// </summary>
        private void StartBasicConsume()
        {
            _logger?.LogTrace("RabbitMQ开始消费消息...");
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
            string eventName = eventArgs.RoutingKey;
            string message = Encoding.UTF8.GetString(eventArgs.Body);
            try
            {
                if (message.ToLowerInvariant().Contains("throw-fake-exception"))
                {
                    throw new InvalidOperationException($"异常请求: \"{message}\"");
                }
                if (await TryProcessEvent(eventName, message))
                {
                    _consumerChannel.BasicAck(eventArgs.DeliveryTag, false);
                }
                else
                {
                    _logger?.LogError(new MateralTFMSException("消息处理失败"), "错误消息: \"{Message}\"", message);
                    _consumerChannel.BasicReject(eventArgs.DeliveryTag, !MateralTFMSRabbitMQConfig.EventErrorConfig.Discard);
                }
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, "错误消息: \"{Message}\"", message);
                _consumerChannel.BasicReject(eventArgs.DeliveryTag, !MateralTFMSRabbitMQConfig.EventErrorConfig.Discard);
            }
        }
        /// <summary>
        /// 处理错误消息
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        private async Task HandlerErrorMessageAsync(Exception exception, Func<Task> handler)
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
            _logger?.LogTrace("处理事件: {EventName}", eventName);
            var result = false;
            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                IEnumerable<SubscriptionInfo> subscriptions = _subsManager.GetHandlersForEvent(eventName);
                foreach (SubscriptionInfo subscription in subscriptions)
                {
                    if (subscription.IsDynamic)
                    {
                        if (!(_service.GetService(subscription.HandlerType) is IDynamicIntegrationEventHandler handler)) continue;
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
                        object handler = _service.GetService(subscription.HandlerType);
                        if (handler == null) continue;
                        Type eventType = _subsManager.GetEventTypeByName(eventName);
                        object integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                        Type concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        MethodInfo handlerMethodInfo = concreteType.GetMethod("HandleAsync");
                        if (handlerMethodInfo == null || handlerMethodInfo.ReturnType != typeof(Task)) continue;
                        try
                        {
                            await (Task)handlerMethodInfo.Invoke(handler, new[] { integrationEvent });
                        }
                        catch (Exception exception)
                        {
                            await HandlerErrorMessageAsync(exception, async () => await (Task)handlerMethodInfo.Invoke(handler, new[] { integrationEvent }));
                        }
                    }
                    _logger?.LogTrace("处理器{EventHandlerName}执行完毕", subscription.HandlerType.Name);
                }
                result= true;
            }
            else
            {
                _logger?.LogError("未找到订阅事件: {EventName}", eventName);
            }
            return result;
        }
        /// <summary>
        /// 事件移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventName"></param>
        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            using (IModel channel = _persistentConnection.CreateModel())
            {
                channel.QueueUnbind(_queueName, _exchangeName, eventName);
                if (!_subsManager.IsEmpty) return;
                _queueName = string.Empty;
                _consumerChannel.Close();
            }
        }
        /// <summary>
        /// 内部订阅
        /// </summary>
        /// <param name="eventName"></param>
        private void DoInternalSubscription(string eventName)
        {
            bool containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (containsKey) return;
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            using (IModel channel = _persistentConnection.CreateModel())
            {
                channel.QueueBind(_queueName, _exchangeName, eventName);
            }
        }
        #endregion
    }
}
