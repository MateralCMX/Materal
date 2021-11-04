using Deploy.Common;
using Deploy.Enums;
using Deploy.ServiceImpl.Manage;
using NLog;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using Policy = Polly.Policy;

namespace Deploy.ServiceImpl.Models
{
    public class ApplicationRuntimeModel
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly Logger _logger;
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 主模块
        /// </summary>
        public string MainModule { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        private ApplicationTypeEnum _applicationType;
        /// <summary>
        /// 类型
        /// </summary>
        public ApplicationTypeEnum ApplicationType
        {
            get => _applicationType;
            set
            {
                _applicationType = value;
                _handler = ApplicationHandlerHelper.GetApplicationHandler(value);
            }

        }
        /// <summary>
        /// 运行参数
        /// </summary>
        public string RunParams { get; set; }
        /// <summary>
        /// 控制台消息
        /// </summary>
        public ICollection<string> ConsoleMessage => _handler?.ConsoleMessage;
        /// <summary>
        /// 处理器
        /// </summary>
        private IApplicationHandler _handler;
        /// <summary>
        /// 状态
        /// </summary>
        public ApplicationStatusEnum Status { get; set; }
        /// <summary>
        /// 更改状态锁
        /// </summary>
        private readonly object _changeStatusLock = new object();
        /// <summary>
        /// 构造方法
        /// </summary>
        public ApplicationRuntimeModel()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }
        /// <summary>
        /// 开始
        /// </summary>
        /// <returns></returns>
        public void Start()
        {
            Exception innerException = null;
            if (Status != ApplicationStatusEnum.Stop) throw new DeployException("应用程序尚未停止");
            lock (_changeStatusLock)
            {
                if (Status != ApplicationStatusEnum.Stop) throw new DeployException("应用程序尚未停止");
                try
                {
                    _handler.StartApplication(this);
                }
                catch (Exception exception)
                {
                    innerException = exception;
                }
            }
            if (innerException != null)
            {
                throw new DeployException("应用程序启动失败", innerException);
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public void Stop()
        {
            const int retryCount = 5;
            Exception innerException = null;
            if (Status != ApplicationStatusEnum.Running) throw new DeployException("应用程序尚未启动");
            lock (_changeStatusLock)
            {
                if (Status != ApplicationStatusEnum.Running) throw new DeployException("应用程序尚未启动");
                try
                {
                    PolicyBuilder policyBuilder = Policy.Handle<Exception>();
                    RetryPolicy policy = policyBuilder.Retry(retryCount, (exception, index) =>
                    {
                        _logger.Warn($"停止应用程序失败[{index}/{retryCount}],正在重试....");
                    });
                    policy.Execute(() =>
                    {
                        _handler.StopApplication(this);
                    });
                }
                catch (Exception exception)
                {
                    innerException = exception;
                }
            }
            if (innerException != null)
            {
                throw new DeployException("应用程序停止失败", innerException);
            }
        }
        /// <summary>
        /// 重启
        /// </summary>
        public void Restart()
        {
            Stop();
            Start();
        }
    }
}
