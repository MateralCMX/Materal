using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Materal.Services.Models;

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
        /// 修改服务
        /// </summary>
        /// <param name="serviceModel"></param>
        Task EditServiceAsync(ServiceModel serviceModel);

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="serviceModel"></param>
        Task AddServiceAsync(ServiceModel serviceModel);
        /// <summary>
        /// 是否拥有服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        bool HasService(string serviceName);
    }
}
