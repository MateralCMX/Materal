using System;
using System.Threading.Tasks;
using Deploy.Common;
using Materal.APP.Core;
using Materal.APP.Hubs.Clients;
using Microsoft.AspNetCore.SignalR.Client;

namespace Deploy.HubImpl.ServerHub
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
                DeployConsoleHelper.WriteLine("注册成功");
            }
            else
            {
                DeployConsoleHelper.WriteLine(message, "注册失败", ConsoleColor.Red);
            }
            return Task.CompletedTask;
        }
    }
}
