using Authority.Common;
using Materal.APP.Core;
using Materal.APP.Hubs.Clients;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Authority.HubImpl.ServerHub
{
    public class ServerClientImpl : BaseClientImpl, IServerClient
    {
        public ServerClientImpl() : base($"{ApplicationConfig.ServerInfo.PublicUrl ?? ApplicationConfig.ServerInfo.Url}/ServerHub")
        {
            Connection.On<bool, string>(nameof(RegisterResult), RegisterResult);
        }

        public Task RegisterResult(bool isSuccess, string message)
        {
            if (isSuccess)
            {
                AuthorityConsoleHelper.WriteLine("注册成功");
            }
            else
            {
                AuthorityConsoleHelper.WriteLine(message, "注册失败", ConsoleColor.Red);
            }
            return Task.CompletedTask;
        }
    }
}
