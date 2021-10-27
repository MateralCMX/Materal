using ConfigCenter.Common;
using ConfigCenter.DataTransmitModel.Namespace;
using ConfigCenter.Services.Models.Namespace;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigCenter.Services
{
    public interface INamespaceService
    {
        /// <summary>
        /// 添加命名空间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterException"></exception>
        [DataValidation]
        Task AddNamespaceAsync(AddNamespaceModel model);
        /// <summary>
        /// 修改命名空间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterException"></exception>
        [DataValidation]
        Task EditNamespaceAsync(EditNamespaceModel model);
        /// <summary>
        /// 删除命名空间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterException"></exception>
        [DataValidation]
        Task DeleteNamespaceAsync(Guid id);
        /// <summary>
        /// 获得命名空间信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterException"></exception>
        [DataValidation]
        Task<NamespaceDTO> GetNamespaceInfoAsync(Guid id);
        /// <summary>
        /// 获得命名空间列表
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterException"></exception>
        [DataValidation]
        Task<List<NamespaceListDTO>> GetNamespaceListAsync(QueryNamespaceFilterModel filterModel);
    }
}
