namespace Materal.EventBus.RabbitMQ
{
    /// <summary>
    /// 默认RabbitMQ持久连接
    /// </summary>
    public class RabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger? _logger;
        /// <summary>
        /// 日志
        /// </summary>
        private readonly IOptionsMonitor<EventBusConfig> _eventBusConfig;
        /// <summary>
        /// 连接对象 
        /// </summary>
        protected IConnection? Connection;
        /// <summary>
        /// 释放标识
        /// </summary>
        protected bool Disposed;
        /// <summary>
        /// 是否已连接
        /// </summary>
        public bool IsConnected => Connection != null && Connection.IsOpen && !Disposed;
        /// <summary>
        /// 连接RabbitMQ锁
        /// </summary>
        protected static readonly object OnlineRabbitMQLock = new();
        /// <summary>
        /// 构造方法
        /// </summary>
        public RabbitMQPersistentConnection(IOptionsMonitor<EventBusConfig> eventBusConfig, ILoggerFactory? loggerFactory = null)
        {
            _eventBusConfig = eventBusConfig;
            _eventBusConfig.OnChange(config => ReConnect());
            _logger = loggerFactory?.CreateLogger("EventBus");
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            try
            {
                Disconnect();
                GC.SuppressFinalize(this);
            }
            catch (IOException ex)
            {
                _logger?.LogError(ex, "连接释放失败");
                Disposed = false;
            }
        }
        /// <summary>
        /// 重新连接
        /// </summary>
        /// <returns></returns>
        public bool ReConnect() => Disconnect() && TryConnect();
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        public bool Disconnect()
        {
            if (!IsConnected || Connection is null) return true;
            _logger?.LogDebug("正在断开RabbitMQ....");
            Connection?.Dispose();
            Connection = null;
            _logger?.LogDebug("RabbitMQ已断开....");
            return true;
        }
        /// <summary>
        /// 尝试连接
        /// </summary>
        /// <returns></returns>
        public bool TryConnect()
        {
            if (IsConnected) return true;
            lock (OnlineRabbitMQLock)
            {
                if (IsConnected) return true;
                RetryPolicy policy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetryForever(_ => TimeSpan.FromSeconds(_eventBusConfig.CurrentValue.RetryInterval), (ex, time) =>
                        {
                            _logger?.LogWarning(ex, $"RabbitMQ连接失败,{_eventBusConfig.CurrentValue.RetryInterval}秒后重试\r\n{ex.GetErrorMessage()}");
                        }
                    );
                policy.Execute(() =>
                {
                    ConnectionFactory connectionFactory = new()
                    {
                        HostName = _eventBusConfig.CurrentValue.HostName,
                        Port = _eventBusConfig.CurrentValue.Port,
                        UserName = _eventBusConfig.CurrentValue.UserName,
                        Password = _eventBusConfig.CurrentValue.Password,
                        DispatchConsumersAsync = true
                    };
                    _logger?.LogDebug($"正在连接RabbitMQ[{connectionFactory.HostName}:{connectionFactory.Port}]....");
                    return Connection = connectionFactory.CreateConnection();
                });
                if (IsConnected && Connection != null)
                {
                    Connection.ConnectionShutdown += OnConnectionShutdown;
                    Connection.CallbackException += OnCallbackException;
                    Connection.ConnectionBlocked += OnConnectionBlocked;
                    _logger?.LogInformation($"RabbitMQ已连接到{Connection.Endpoint.HostName}");
                    return true;
                }
                _logger?.LogError("无法连接到RabbitMQ");
                return false;
            }
        }
        /// <summary>
        /// 创建通道
        /// </summary>
        /// <returns></returns>
        /// <exception cref="EventBusException"></exception>
        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                TryConnect();
            }
            if (!IsConnected || Connection is null) throw new EventBusException("没有可用的RabbitMQ连接");
            IModel result = Connection.CreateModel();
            return result;
        }
        /// <summary>
        /// 创建通道
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <returns></returns>
        /// <exception cref="EventBusException"></exception>
        public IModel CreateModel(string exchangeName)
        {
            IModel result = CreateModel();
            result.TryCreateExchange(exchangeName);
            return result;
        }
        /// <summary>
        /// 创建通道
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="queueName"></param>
        /// <returns></returns>
        /// <exception cref="EventBusException"></exception>
        public IModel CreateModel(string exchangeName, string queueName)
        {
            IModel result = CreateModel(exchangeName);
            result.TryCreateQueue(queueName);
            return result;
        }
        #region 私有方法
        /// <summary>
        /// 连接阻塞
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
        {
            if (Disposed) return;
            if (Connection is not null)
            {
                Connection.ConnectionShutdown -= OnConnectionShutdown;
                Connection.CallbackException -= OnCallbackException;
                Connection.ConnectionBlocked -= OnConnectionBlocked;
            }
            _logger?.LogWarning("RabbitMQ连接被阻塞,正在重新连接...");
            TryConnect();
        }
        /// <summary>
        /// 返回异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCallbackException(object? sender, CallbackExceptionEventArgs e)
        {
            if (Disposed) return;
            if (Connection is not null)
            {
                Connection.ConnectionShutdown -= OnConnectionShutdown;
                Connection.CallbackException -= OnCallbackException;
                Connection.ConnectionBlocked -= OnConnectionBlocked;
            }
            _logger?.LogWarning($"RabbitMQ发送了一个异常({e.Exception.GetErrorMessage()}),正在重新连接...");
            TryConnect();
        }
        /// <summary>
        /// 连接关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reason"></param>
        private void OnConnectionShutdown(object? sender, ShutdownEventArgs reason)
        {
            if (Disposed) return;
            if(Connection is not null)
            {
                Connection.ConnectionShutdown -= OnConnectionShutdown;
                Connection.CallbackException -= OnCallbackException;
                Connection.ConnectionBlocked -= OnConnectionBlocked;
            }
            _logger?.LogWarning("RabbitMQ连接被关闭,正在重新连接...");
            TryConnect();
        }
        #endregion
    }
}
