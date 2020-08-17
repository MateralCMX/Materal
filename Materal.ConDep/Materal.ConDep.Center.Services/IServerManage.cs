using System.Collections.Generic;
using Materal.ConDep.Center.DataTransmitModel.Server;
using Materal.ConDep.Center.Services.Models.Server;

namespace Materal.ConDep.Center.Services
{
    public interface IServerManage
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void RegisterServer(ServerModel model);
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        List<ServerListDTO> GetList();
    }
}
