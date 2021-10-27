using Materal.APP.Core;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Materal.APP.Hubs.Clients
{
    public class BaseClientImpl : IBaseClient
    {
        private readonly string _url;
        private const int _retryCount = 5;
        private const int _retrySecond = 5;
        private static int RetryMillisecond => _retrySecond * 1000;
        private readonly string _consoleTitle;
        private Func<Task> _onConnectSuccess;
        public HubConnection Connection { get; }

        protected BaseClientImpl(string url)
        {
            _url = url;
            _consoleTitle = GetType().Name;
            TimeSpan[] timeSpanConfig = GetRetryTimeSpanConfig();
            IHubConnectionBuilder hubConnectionBuilder = new HubConnectionBuilder();
            hubConnectionBuilder = hubConnectionBuilder.WithUrl(_url);
            if (timeSpanConfig?.Length > 0)
            {
                hubConnectionBuilder = hubConnectionBuilder.WithAutomaticReconnect(timeSpanConfig);
            }
            Connection = hubConnectionBuilder.Build();
            Connection.Reconnected += Connection_Reconnected;
            Connection.Reconnecting += Connection_Reconnecting;
            Connection.Closed += Connection_Closed;
        }
        /// <summary>
        /// 设置连接成功之后方法
        /// </summary>
        /// <param name="func"></param>
        public void SetConnectSuccessLaterAction(Func<Task> func)
        {
            _onConnectSuccess = func;
        }
        /// <summary>
        /// 获取重试时间配置
        /// </summary>
        /// <returns></returns>
        private TimeSpan[] GetRetryTimeSpanConfig()
        {
            var result = new TimeSpan[_retryCount];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = TimeSpan.FromSeconds(_retrySecond);
            }
            return result;
        }
        /// <summary>
        /// 连接并重试
        /// </summary>
        /// <returns></returns>
        public async Task ConnectWithRetryAsync()
        {
            var source = new CancellationTokenSource();
            bool result = await ConnectWithRetryAsync(source.Token);
            if (!result) throw new MateralAPPException("连接失败");
        }
        /// <summary>
        /// 连接并重试
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> ConnectWithRetryAsync(CancellationToken token)
        {
            while (Connection.State != HubConnectionState.Connected)
            {
                try
                {
                    ConsoleHelperBase.WriteLine(_consoleTitle, $"正在与服务器{_url}建立连接.....", "Info", ConsoleColor.White);
                    await Connection.StartAsync(token);
                    if (_onConnectSuccess != null)
                    {
                        await _onConnectSuccess();
                    }
                }
                catch when (token.IsCancellationRequested)
                {
                    return false;
                }
                catch
                {
                    ConsoleHelperBase.WriteLine(_consoleTitle, $"连接失败,{_retrySecond}秒后重新连接", "Fail", ConsoleColor.Red);
                    await Task.Delay(RetryMillisecond, token);
                }
            }
            return true;
        }
        /// <summary>
        /// 连接关闭
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task Connection_Closed(Exception exception)
        {
            ConsoleHelperBase.WriteLine(_consoleTitle, $"与服务器{_url}连接断开,{_retrySecond}秒后重新连接", "Fail", ConsoleColor.Red);
            await Task.Delay(RetryMillisecond);
            await ConnectWithRetryAsync();
        }
        /// <summary>
        /// 重连中
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private Task Connection_Reconnecting(Exception exception)
        {
            ConsoleHelperBase.WriteLine(_consoleTitle, $"与服务器{_url}连接丢失,将重试{_retryCount}次,每次间隔{_retrySecond}秒", "Fail", ConsoleColor.Red);
            ConsoleHelperBase.WriteLine(_consoleTitle, $"正在重新与服务器{_url}取得联系.....", "Fail", ConsoleColor.Red);
            return Task.CompletedTask;
        }
        /// <summary>
        /// 重连成功
        /// </summary>
        /// <param name="connectionID"></param>
        /// <returns></returns>
        private async Task Connection_Reconnected(string connectionID)
        {
            ConsoleHelperBase.WriteLine(_consoleTitle, "与服务器连接成功", "Info", ConsoleColor.White);
            if (_onConnectSuccess != null)
            {
                await _onConnectSuccess();
            }
        }
    }
}
