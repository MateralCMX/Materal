using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Materal.APP.Hubs.Clients
{
    public interface IBaseClient
    {
        /// <summary>
        /// 连接
        /// </summary>
        HubConnection Connection { get; }
        /// <summary>
        /// 设置连接成功之后方法
        /// </summary>
        /// <param name="func"></param>
        void SetConnectSuccessLaterAction(Func<Task> func);
        /// <summary>
        /// 连接并重试
        /// </summary>
        /// <returns></returns>
        Task ConnectWithRetryAsync();
        /// <summary>
        /// 连接并重试
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> ConnectWithRetryAsync(CancellationToken token);
    }
}
