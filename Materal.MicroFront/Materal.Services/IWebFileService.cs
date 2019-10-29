using Materal.Services.Models;
using System.Collections.Generic;

namespace Materal.Services
{
    /// <summary>
    /// Web文件服务
    /// </summary>
    public interface IWebFileService
    {
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <returns></returns>
        List<ServiceModel> GetServices();
        /// <summary>
        /// 删除服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        void DeleteService(string serviceName);
        /// <summary>
        /// 初始化服务
        /// </summary>
        void InitServices();
    }
}
