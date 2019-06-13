using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.Common;
using WeChatService.DataTransmitModel.Application;
using WeChatService.Service.Model.Application;
namespace WeChatService.Service
{
    /// <summary>
    /// 应用服务
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// 添加应用
        /// </summary>
        /// <param name="model">添加模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task AddApplicationAsync(AddApplicationModel model);
        /// <summary>
        /// 修改应用
        /// </summary>
        /// <param name="model">修改模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task EditApplicationAsync(EditApplicationModel model);
        /// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task DeleteApplicationAsync(Guid id);
        /// <summary>
        /// 获得应用信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<ApplicationDTO> GetApplicationInfoAsync(Guid id);

        /// <summary>
        /// 获得应用信息
        /// </summary>
        /// <param name="appID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<ApplicationDTO> GetApplicationInfoAsync(string appID, Guid userID);
        /// <summary>
        /// 获得应用列表
        /// </summary>
        /// <param name="filterModel">查询模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<(List<ApplicationListDTO> result, PageModel pageModel)> GetApplicationListAsync(QueryApplicationFilterModel filterModel);
    }
}
