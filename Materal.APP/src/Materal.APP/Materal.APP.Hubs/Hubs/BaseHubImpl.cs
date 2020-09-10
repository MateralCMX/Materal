using Materal.APP.Core;
using Materal.APP.Hubs.Clients;
using Microsoft.AspNetCore.SignalR.Client;

namespace Materal.APP.Hubs.Hubs
{
    public abstract class BaseHubImpl
    {
        /// <summary>
        /// 连接
        /// </summary>
        protected abstract HubConnection Connection { get; }
        /// <summary>
        /// 获得连接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <returns></returns>
        protected HubConnection GetHubConnection<T>(T client) where T : class
        {
            if (client is IBaseClient baseClient)
            {
                return baseClient.Connection;
            }
            throw new MateralAPPException("客户端未实现IBaseClient");
        }
    }
}
