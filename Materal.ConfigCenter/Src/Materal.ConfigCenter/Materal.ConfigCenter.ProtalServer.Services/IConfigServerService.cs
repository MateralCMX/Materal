using Materal.ConfigCenter.ProtalServer.PresentationModel.ConfigServer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.ConfigCenter.ProtalServer.Services.Models;

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
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        void DeleteProject(Guid id, string token);
        /// <summary>
        /// 删除命名空间
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        void DeleteNamespace(Guid id, string token);
        /// <summary>
        /// 获得配置服务
        /// </summary>
        /// <returns></returns>
        List<ConfigServerModel> GetConfigServers();
        /// <summary>
        /// 复制配置服务
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        Task CopyConfigServer(CopyConfigServerModel model, string token);
        /// <summary>
        /// 复制配置服务
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        Task CopyNamespace(CopyNamespaceModel model, string token);
    }
}
