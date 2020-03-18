using System;
using System.Threading.Tasks;
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
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteProject(Guid id);
        /// <summary>
        /// 删除命名空间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteNamespace(Guid id);
    }
}
