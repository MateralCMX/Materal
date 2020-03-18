using System;
using Materal.ConfigCenter.ProtalServer.PresentationModel.ConfigServer;

namespace Materal.ConfigCenter.ProtalServer.Services
{
    public interface IConfigServerService
    {
        /// <summary>
        /// 注册新客户端
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void RegisterNewClient(RegisterConfigServerModel model);
    }
}
