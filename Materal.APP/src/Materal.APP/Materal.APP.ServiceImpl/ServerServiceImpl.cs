using Materal.APP.Common;
using Materal.APP.Core;
using Materal.APP.DataTransmitModel;
using Materal.APP.Services;
using Materal.APP.Services.Models.Server;
using Materal.Common;
using Materal.ConvertHelper;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Materal.APP.ServiceImpl
{
    public class ServerServiceImpl : IServerService
    {
        private readonly ConcurrentDictionary<string, RegisterModel> _registers = new ConcurrentDictionary<string, RegisterModel>();
        public bool RegisterServer(string key, RegisterModel model)
        {
            if (model.Key != ApplicationConfig.ServerInfo.Key) return false;
            var result = true;
            if (_registers.ContainsKey(key))
            {
                _registers[key] = model;
            }
            else
            {
                result = _registers.TryAdd(key, model);
                if (result)
                {
                    MateralAPPConsoleHelper.WriteLine($"新的{model.ServerCategory.GetDescription()}注册:{model.Url}");
                }
            }
            return result;
        }

        public void UnRegisterServer(string key)
        {
            if (!_registers.ContainsKey(key)) return;
            while (true)
            {
                if (!_registers.TryRemove(key, out RegisterModel register)) continue;
                if (register != null)
                {
                    MateralAPPConsoleHelper.WriteLine($"{register.ServerCategory.GetDescription()}已断开:{register.Url}");
                }
                break;
            }
        }

        public List<ServerListDTO> GetServerList()
        {
            return _registers.Select(m => m.Value.CopyProperties<ServerListDTO>()).ToList();
        }
    }
}
