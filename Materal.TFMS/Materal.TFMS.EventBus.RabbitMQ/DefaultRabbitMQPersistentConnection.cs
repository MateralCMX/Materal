using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

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
        protected readonly ILogger<DefaultRabbitMQPersistentConnection> Logger;
        /// <summary>
        /// 重试次数
        /// </summary>
        private readonly int _retryCount;
        /// <summary>
        /// 连接对象 
        /// </summary>
        protected IConnection Connection;
        /// <summary>
        /// 释放标识
        /// </summary>
        protected bool Disposed;
        public bool IsConnected => Connection != null && Connection.IsOpen && !Disposed;
        /// <summary>
        /// 联线RabbitMQ锁
        /// </summary>
        protected static readonly object OnlineRabbitMQLock = new object();
        /// <summary>
        /// 默认RabbitMQ持久连接
        /// </summary>
        /// <param name="connectionFactory">连接工厂</param>
        /// <param name="logger">日志对象</param>
        /// <param name="retryCount">重试次数</param>
        public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory, ILogger<DefaultRabbitMQPersistentConnection> logger, int retryCount = 5)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _retryCount = retryCount;
        }
        public void Dispose()
        {
            if (Disposed) return;

            Disposed = true;

            try
            {
                Connection.Dispose();
            }
            catch (IOException ex)
            {
                Logger?.LogCritical(ex.ToString());
            }
        }
        public bool TryConnect()
        {
            Logger?.LogInformation("正在连接RabbitMQ....");
            lock (OnlineRabbitMQLock)
            {
                RetryPolicy policy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                        {
                            Logger?.LogWarning(ex, "RabbitMQ连接超时,TimeOut={TimeOut}, ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                        }
                    );
                policy.Execute(() => Connection = _connectionFactory.CreateConnection());
                if (IsConnected)
                {
                    Connection.ConnectionShutdown += OnConnectionShutdown;
                    Connection.CallbackException += OnCallbackException;
                    Connection.ConnectionBlocked += OnConnectionBlocked;
                    Logger?.LogInformation("RabbitMQ已连接到{HostName}", Connection.Endpoint.HostName);
                    return true;
                }
                Logger?.LogCritical("致命错误:无法连接RabbitMQ");
                return false;
            }
        }
        public async Task<IModel> CreateModelAsync()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("没有可用的RabbitMQ连接");
            }
            IModel result = await Task.Run(() => Connection.CreateModel());
            return result;
        }
        #region 私有方法
        /// <summary>
        /// 连接阻塞
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (Disposed) return;
            Logger?.LogWarning("RabbitMQ连接被阻塞,正在重新连接...");
            TryConnect();
        }
        /// <summary>
        /// 返回异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (Disposed) return;
            Logger?.LogWarning("RabbitMQ发送了一个异常({ExceptionMessage}),正在重新连接...", e.Exception.Message);
            TryConnect();
        }
        /// <summary>
        /// 连接关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reason"></param>
        private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (Disposed) return;
            Logger?.LogWarning("RabbitMQ连接被关闭,正在重新连接...");
            TryConnect();
        }
        #endregion
    }
}
