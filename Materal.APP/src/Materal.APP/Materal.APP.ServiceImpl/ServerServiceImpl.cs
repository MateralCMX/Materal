using System;
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
using Materal.APP.Enums;

namespace Materal.APP.ServiceImpl
{
    public class ServerServiceImpl : IServerService
    {
        private readonly ConcurrentDictionary<string, RegisterModel> _registers = new ConcurrentDictionary<string, RegisterModel>();
        public void RegisterServer(string key, RegisterModel model)
        {
            (bool canRegister, string errorMessage) = CanRegister(key, model);
            if (!canRegister) throw new MateralAPPException(errorMessage);
            if (_registers.TryAdd(key, model))
            {
                MateralAPPConsoleHelper.WriteLine($"新的{model.ServerCategory.GetDescription()}服务[{model.Name}]注册:{model.Url}");
            }
            else
            {
                throw new MateralAPPException("注册服务失败");
            }
        }

        public void UnRegisterServer(string key)
        {
            if (!_registers.ContainsKey(key)) return;
            while (true)
            {
                if (!_registers.TryRemove(key, out RegisterModel register)) continue;
                if (register != null)
                {
                    MateralAPPConsoleHelper.WriteLine($"{register.ServerCategory.GetDescription()}[{register.Name}]已断开:{register.Url}");
                }
                break;
            }
        }

        public List<ServerListDTO> GetServerList()
        {
            return _registers.Select(m => m.Value.CopyProperties<ServerListDTO>()).ToList();
        }

        #region 私有方法
        /// <summary>
        /// 是否可以注册
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private (bool canRegister, string errorMessage) CanRegister(string key, RegisterModel model)
        {
            try
            {
                if (model.Key != ApplicationConfig.ServerInfo.Key) throw new MateralAPPException("密钥错误");
                if (_registers.ContainsKey(key)) throw new MateralAPPException("同一连接不可以重复注册");
                if (_registers.Any(m => m.Value.Name.Equals(model.Name, StringComparison.Ordinal)))
                {
                    throw new MateralAPPException("该服务名称已被注册");
                }
                if (_registers.Any(m => m.Value.Url.Equals(model.Url, StringComparison.Ordinal)))
                {
                    throw new MateralAPPException("该服务连接已被注册");
                }
                switch (model.ServerCategory)
                {
                    case ServerCategoryEnum.Authority:
                    case ServerCategoryEnum.ConfigCenter:
                        if (_registers.Any(m => m.Value.ServerCategory == model.ServerCategory))
                        {
                            throw new MateralAPPException($"{model.ServerCategory.GetDescription()}已被注册");
                        }
                        break;
                }
                return (true, string.Empty);
            }
            catch (MateralAPPException exception)
            {
                return (false, exception.Message);
            }
        }

        #endregion
    }
}
