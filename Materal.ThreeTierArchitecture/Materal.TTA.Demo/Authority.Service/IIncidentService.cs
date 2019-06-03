using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.Common;
using Authority.DataTransmitModel.Incident;
using Authority.Service.Model.Incident;
namespace Authority.Service
{
    /// <summary>
    /// 事件服务
    /// </summary>
    public interface IIncidentService
    {
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="model">添加模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task AddIncidentAsync(AddIncidentModel model);
        /// <summary>
        /// 修改事件
        /// </summary>
        /// <param name="model">修改模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task EditIncidentAsync(EditIncidentModel model);
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task DeleteIncidentAsync(Guid id);
        /// <summary>
        /// 获得事件信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<IncidentDTO> GetIncidentInfoAsync(Guid id);
        /// <summary>
        /// 获得事件列表
        /// </summary>
        /// <param name="filterModel">查询模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<(List<IncidentListDTO> result, PageModel pageModel)> GetIncidentListAsync(QueryIncidentFilterModel filterModel);
    }
}
