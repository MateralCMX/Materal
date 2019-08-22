using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.IO;
using System.Net.Sockets;

namespace Materal.TFMS.EventBus.RabbitMQ
{
    /// <summary>
    /// 默认RabbitMQ持久连接
    /// </summary>
    public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        /// <summary>
        /// 连接工厂
        /// </summary>
        private readonly IConnectionFactory _connectionFactory;
        /// <summary>
        /// 日志对象
        /// </summary>
        protected readonly ILogger<DefaultRabbitMQPersistentConnection> _logger;
        /// <summary>
        /// 重试次数
        /// </summary>
        private readonly int _retryCount;
        /// <summary>
        /// 连接对象 
        /// </summary>
        protected IConnection _connection;
        /// <summary>
        /// 释放标识
        /// </summary>
        protected bool _disposed;
        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;
        /// <summary>
        /// 联线RabbitMQ锁
        /// </summary>
        protected static readonly object _onlineRabbitMQLock = new object();
        /// <summary>
        /// 默认RabbitMQ持久连接
        /// </summary>
        /// <param name="connectionFactory">连接工厂</param>
        /// <param name="logger">日志对象</param>
        /// <param name="retryCount">重试次数</param>
        public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory, ILogger<DefaultRabbitMQPersistentConnection> logger, int retryCount = 5)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _retryCount = retryCount;
        }
        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException ex)
            {
                _logger?.LogCritical(ex.ToString());
            }
        }
        public bool TryConnect()
        {
            _logger?.LogInformation("正在连接RabbitMQ....");
            lock (_onlineRabbitMQLock)
            {
                RetryPolicy policy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                        {
                            _logger?.LogWarning(ex, "RabbitMQ连接超时,TimeOut={TimeOut}, ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                        }
                    );
                policy.Execute(() => _connection = _connectionFactory.CreateConnection());
                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutdown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;
                    _logger?.LogInformation("RabbitMQ已连接到{HostName}", _connection.Endpoint.HostName);
                    return true;
                }
                _logger?.LogCritical("致命错误:无法连接RabbitMQ");
                return false;
            }
        }
        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("没有可用的RabbitMQ连接");
            }
            return _connection.CreateModel();
        }
        #region 私有方法
        /// <summary>
        /// 连接阻塞
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;
            _logger?.LogWarning("RabbitMQ连接被阻塞,正在重新连接...");
            TryConnect();
        }
        /// <summary>
        /// 返回异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;
            _logger?.LogWarning("RabbitMQ发送了一个异常({ExceptionMessage}),正在重新连接...", e.Exception.Message);
            TryConnect();
        }
        /// <summary>
        /// 连接关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reason"></param>
        private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;
            _logger?.LogWarning("RabbitMQ连接被关闭,正在重新连接...");
            TryConnect();
        }
        #endregion
    }
}
