namespace Materal.EventBus.RabbitMQ
{
    /// <summary>
    /// 事件总线实现
    /// </summary>
    public class RabbitMQEventBusImpl : EventBusImpl, IEventBus
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly IOptionsMonitor<EventBusConfig> _eventBusConfig;
        private IModel? _consumerChannel;
        /// <summary>
        /// 构造方法
        /// </summary>
        public RabbitMQEventBusImpl(IRabbitMQPersistentConnection persistentConnection, IOptionsMonitor<EventBusConfig> eventBusConfig, IServiceProvider serviceProvider, ILoggerFactory? loggerFactory = null) : base(serviceProvider, loggerFactory)
        {
            _persistentConnection = persistentConnection;
            _eventBusConfig = eventBusConfig;
            _consumerChannel = CreateConsumerChannelAsync();
            StartBasicConsume();
        }
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="event"></param>
        public override void Publish(IEvent @event)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            string eventName = @event.GetType().Name;
            RetryPolicy policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetryForever(count => TimeSpan.FromSeconds(_eventBusConfig.CurrentValue.RetryInterval), (ex, time) =>
                {
                    Logger?.LogError(ex, $"发布事件失败: {eventName},{_eventBusConfig.CurrentValue.RetryInterval}秒后重试\r\n{ex.GetErrorMessage()})");
                });
            string exchangeName = _eventBusConfig.CurrentValue.GetTrueExchangeName();
            using IModel channel = _persistentConnection.CreateModel(exchangeName);
            string message = @event.ToJson();
            byte[] body = Encoding.UTF8.GetBytes(message);
            policy.Execute(() =>
            {
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2;
                Logger?.LogDebug($"发布事件: {eventName}");
                channel.BasicPublish(exchangeName, eventName, properties, body);
                channel.Dispose();
            });
        }
        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="eventHandlerType"></param>
        /// <exception cref="EventBusException"></exception>
        public override string Subscribe(Type eventType, Type eventHandlerType)
        {
            string exchangeName = _eventBusConfig.CurrentValue.GetTrueExchangeName();
            string queueName = _eventBusConfig.CurrentValue.GetTrueQueueName();
            string eventName = base.Subscribe(eventType, eventHandlerType);
            using IModel channel = _persistentConnection.CreateModel(exchangeName, queueName);
            channel.QueueBind(queueName, exchangeName, eventName);
            return eventName;
        }
        #region 私有方法
        /// <summary>
        /// 创建消费通道
        /// </summary>
        /// <returns></returns>
        private IModel CreateConsumerChannelAsync()
        {
            string exchangeName = _eventBusConfig.CurrentValue.GetTrueExchangeName();
            string queueName = _eventBusConfig.CurrentValue.GetTrueQueueName();
            Logger?.LogDebug($"创建RabbitMQ消费通道[{exchangeName}_{queueName}]...");
            IModel channel = _persistentConnection.CreateModel(exchangeName, queueName);
            channel.CallbackException += (sender, ea) =>
            {
                Logger?.LogWarning(ea.Exception, $"重新创建RabbitMQ消费通道[{exchangeName}_{queueName}]...");
                _consumerChannel?.Dispose();
                _consumerChannel = CreateConsumerChannelAsync();
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
                AsyncEventingBasicConsumer consumer = new(_consumerChannel);
                consumer.Received += Consumer_ReceivedAsync;
                string queueName = _eventBusConfig.CurrentValue.GetTrueQueueName();
                _consumerChannel.BasicConsume(queueName, false, consumer);
                Logger?.LogInformation("EventBus已启动");
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
            string message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            try
            {
                if (message.Contains("throw-fake-exception", StringComparison.InvariantCultureIgnoreCase)) throw new EventBusException($"接收到异常消息: \"{message}\"");
                SubscriptionInfo? subscriptionInfo = GetSubscriptionInfoForEvent(eventName);
                if(subscriptionInfo is null || subscriptionInfo.IsEmpty) throw new EventBusException($"未订阅事件{eventName}");
                object eventObj = message.JsonToObject(subscriptionInfo.EventType);
                if(eventObj is not IEvent @event) throw new EventBusException("消息不是事件");
                if (!await TryProcessEvent(@event)) throw new EventBusException("消息处理失败");
                _consumerChannel?.BasicAck(eventArgs.DeliveryTag, false);
            }
            catch (Exception exception)
            {
                Logger?.LogError(exception, "错误消息: \"{Message}\"", message);
                _consumerChannel?.BasicReject(eventArgs.DeliveryTag, !_eventBusConfig.CurrentValue.ErrorMeesageDiscard);
            }
        }
        #endregion
    }
}
