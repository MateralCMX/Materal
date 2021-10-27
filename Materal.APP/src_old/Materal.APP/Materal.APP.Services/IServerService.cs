using Materal.APP.Services.Models.Server;
using System.Collections.Generic;
using Materal.APP.DataTransmitModel;

namespace Materal.APP.Services
{
    public interface IServerService
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        void RegisterServer(string key, RegisterModel model);
        /// <summary>
        /// 反注册服务
        /// </summary>
        /// <param name="key"></param>
        void UnRegisterServer(string key);
        /// <summary>
        /// 获得服务列表
        /// </summary>
        /// <returns></returns>
        List<ServerListDTO> GetServerList();
    }
}
