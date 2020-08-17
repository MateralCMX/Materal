using AutoMapper;
using Materal.Common;
using Materal.ConDep.Center.DataTransmitModel.Server;
using Materal.ConDep.Center.Services;
using Materal.ConDep.Center.Services.Models.Server;
using Materal.Model;
using Materal.NetworkHelper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Materal.ConDep.Center.ServiceImpl
{
    public class ServerManageImpl : IServerManage
    {
        private readonly IMapper _mapper;
        private readonly ConcurrentDictionary<string, ServerModel> servers = new ConcurrentDictionary<string, ServerModel>();
        private readonly Timer timer = new Timer(10000);
        public ServerManageImpl(IMapper mapper)
        {
            _mapper = mapper;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            var removeServers = new List<string>();
            foreach (KeyValuePair<string, ServerModel> server in servers)
            {
                try
                {
                    var result = HttpManager.SendGet<ResultModel>(server.Value.HealthAddress);
                    if (result.ResultType != ResultTypeEnum.Success) throw new MateralConDepException("健康检查失败");
                }
                catch (Exception)
                {
                    removeServers.Add(server.Key);
                }
            }
            foreach (string removeServer in removeServers)
            {
                servers.Remove(removeServer, out _);
            }
            timer.Start();
        }

        public void RegisterServer(ServerModel model)
        {
            ServerModel server = servers.FirstOrDefault(m => m.Key.Equals(model.Name)).Value;
            if (server == null)
            {
                servers.TryAdd(model.Name, model);
            }
            else
            {
                server.Address = model.Address;
            }
            ConsoleHelper.ServerWriteLine($"新的服务实例:{model.Name}[{model.Address}] 当前总数:{servers.Count}");
        }

        public List<ServerListDTO> GetList()
        {
            List<ServerModel> models = servers.Values.OrderBy(m => m.Address).ToList();
            var result = _mapper.Map<List<ServerListDTO>>(models);
            return result;
        }
    }
}
