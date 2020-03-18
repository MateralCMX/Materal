using Materal.ConfigCenter.ProtalServer.DataTransmitModel.Namespace;
using Materal.ConfigCenter.ProtalServer.PresentationModel.Namespace;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.ConfigCenter.ProtalServer.Services
{
    public interface INamespaceService
    {
        /// <summary>
        /// 添加命名空间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MateralConfigCenterException"></exception>
        [DataValidation]
        Task AddNamespaceAsync(AddNamespaceModel model);
        /// <summary>
        /// 修改命名空间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MateralConfigCenterException"></exception>
        [DataValidation]
        Task EditNamespaceAsync(EditNamespaceModel model);
        /// <summary>
        /// 删除命名空间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MateralConfigCenterException"></exception>
        [DataValidation]
        Task DeleteNamespaceAsync(Guid id);
        /// <summary>
        /// 获得命名空间信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MateralConfigCenterException"></exception>
        [DataValidation]
        Task<NamespaceDTO> GetNamespaceInfoAsync(Guid id);
        /// <summary>
        /// 获得命名空间列表
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        /// <exception cref="MateralConfigCenterException"></exception>
        [DataValidation]
        Task<List<NamespaceListDTO>> GetNamespaceListAsync(QueryNamespaceFilterModel filterModel);
    }
}
