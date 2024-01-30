namespace Materal.EventBus.RabbitMQ
{
    /// <summary>
    /// 事件总线实现
    /// </summary>
    public class RabbitMQEventBusImpl(IRabbitMQPersistentConnection persistentConnection, IOptionsMonitor<EventBusConfig> eventBusConfig, IServiceProvider serviceProvider, ILoggerFactory? loggerFactory = null) : EventBusImpl(serviceProvider, loggerFactory), IEventBus
    {
        private readonly Dictionary<string, IModel> _consumerChannels = [];
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="event"></param>
        public override void Publish(IEvent @event)
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }
            string eventName = @event.GetType().Name;
            RetryPolicy policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetryForever(count => TimeSpan.FromSeconds(eventBusConfig.CurrentValue.RetryInterval), (ex, time) =>
                {
                    Logger?.LogError(ex, $"发布事件失败: {eventName},{eventBusConfig.CurrentValue.RetryInterval}秒后重试\r\n{ex.GetErrorMessage()})");
                });
            string exchangeName = eventBusConfig.CurrentValue.GetTrueExchangeName();
            using IModel channel = persistentConnection.CreateModel(exchangeName);
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
            string exchangeName = eventBusConfig.CurrentValue.GetTrueExchangeName();
            string queueName = eventBusConfig.CurrentValue.GetTrueQueueName(eventHandlerType);
            string eventName = base.Subscribe(eventType, eventHandlerType);
            IModel channel;
            if (!_consumerChannels.TryGetValue(queueName, out IModel? consumerChannel))
            {
                channel = CreateConsumerChannelAsync(exchangeName, queueName);
                _consumerChannels.Add(queueName, channel);
                StartBasicConsume(channel, queueName);
            }
            else
            {
                channel = consumerChannel;
            }
            channel.QueueBind(queueName, exchangeName, eventName);
            return eventName;
        }
        #region 私有方法
        /// <summary>
        /// 创建消费通道
        /// </summary>
        /// <returns></returns>
        private IModel CreateConsumerChannelAsync(string exchangeName, string queueName)
        {
            Logger?.LogDebug($"创建RabbitMQ消费通道[{exchangeName}_{queueName}]...");
            IModel channel = persistentConnection.CreateModel(exchangeName, queueName);
            channel.CallbackException += (sender, args)=>
            {
                Logger?.LogWarning(args.Exception, $"重新创建RabbitMQ消费通道[{exchangeName}_{queueName}]...");
                if (sender is not IModel model) throw new EventBusException("获取消费通道失败");
                model.Dispose();
                model = CreateConsumerChannelAsync(exchangeName, queueName);
                StartBasicConsume(model, queueName);
            };
            return channel;
        }
        /// <summary>
        /// 开始消费
        /// </summary>
        private void StartBasicConsume(IModel channel, string queueName)
        {
            AsyncEventingBasicConsumer consumer = new(channel);
            consumer.Received += Consumer_ReceivedAsync;
            channel.BasicConsume(queueName, false, consumer);
        }
        /// <summary>
        /// 消费者接收到消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        private async Task Consumer_ReceivedAsync(object sender, BasicDeliverEventArgs eventArgs)
        {
            if (sender is not AsyncEventingBasicConsumer consumer) throw new EventBusException("获取消费通道失败");
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
                consumer.Model.BasicAck(eventArgs.DeliveryTag, false);
            }
            catch (Exception exception)
            {
                Logger?.LogError(exception, "错误消息: \"{Message}\"", message);
                consumer.Model.BasicReject(eventArgs.DeliveryTag, !eventBusConfig.CurrentValue.ErrorMeesageDiscard);
            }
        }
        #endregion
    }
}
