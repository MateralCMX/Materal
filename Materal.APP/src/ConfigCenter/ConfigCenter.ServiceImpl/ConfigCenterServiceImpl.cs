using ConfigCenter.Common;
using ConfigCenter.DataTransmitModel.ConfigCenter;
using ConfigCenter.Services;
using ConfigCenter.Services.Models.ConfigCenter;
using Materal.APP.Core;
using Materal.ConvertHelper;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ConfigCenter.ServiceImpl
{
    public class ConfigCenterServiceImpl : IConfigCenterService
    {
        private readonly ConcurrentDictionary<string, RegisterEnvironmentModel> _registers = new ConcurrentDictionary<string, RegisterEnvironmentModel>();
        public bool RegisterEnvironment(string key, RegisterEnvironmentModel model)
        {
            if (model.Key != ApplicationConfig.ServerInfo.Key) return false;
            var result = true;
            if (_registers.ContainsKey(key))
            {
                _registers[key] = model;
            }
            else
            {
                result = _registers.ToList().All(m => m.Value.Name != model.Name);
                if (!result) return false;
                result = _registers.TryAdd(key, model);
                if (result)
                {
                    ConfigCenterConsoleHelper.WriteLine($"新的环境{model.Name}注册:{model.Url}");
                }
            }
            return result;
        }

        public void UnRegisterEnvironment(string key)
        {
            if (!_registers.ContainsKey(key)) return;
            while (true)
            {
                if (!_registers.TryRemove(key, out RegisterEnvironmentModel register)) continue;
                if (register != null)
                {
                    ConfigCenterConsoleHelper.WriteLine($"环境[{register.Name}]已断开:{register.Url}");
                }
                break;
            }
        }

        public List<EnvironmentListDTO> GetEnvironmentList()
        {
            return _registers.Select(m => m.Value.CopyProperties<EnvironmentListDTO>()).ToList();
        }
    }
}
