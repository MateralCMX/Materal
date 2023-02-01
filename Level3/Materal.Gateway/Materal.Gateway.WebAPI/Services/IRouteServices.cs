using Materal.Gateway.WebAPI.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.Gateway.WebAPI.Services
{
    public interface IRouteServices
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="routeModel"></param>
        /// <returns></returns>
        Task AddAsync(RouteModel routeModel);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="routeModel"></param>
        /// <returns></returns>
        Task EditAsync(RouteModel routeModel);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        Task DeleteAsync(string serviceName);
        /// <summary>
        /// 获得
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        Task<RouteModel> GetAsync(string serviceName);
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        Task<List<RouteListModel>> GetListAsync(QueryRouteFilterModel filterModel);
    }
}
