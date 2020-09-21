using ConfigCenter.DataTransmitModel.Namespace;
using ConfigCenter.PresentationModel.Namespace;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigCenter.HttpManage
{
    public interface INamespaceManage
    {
        /// <summary>
        /// 添加命名空间
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> AddNamespaceAsync(AddNamespaceRequestModel requestModel);

        /// <summary>
        /// 修改命名空间
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> EditNamespaceAsync(EditNamespaceRequestModel requestModel);

        /// <summary>
        /// 删除命名空间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel> DeleteNamespaceAsync(Guid id);

        /// <summary>
        /// 获得命名空间信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel<NamespaceDTO>> GetNamespaceInfoAsync(Guid id);

        /// <summary>
        /// 获得命名空间列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel<List<NamespaceListDTO>>> GetNamespaceListAsync(QueryNamespaceFilterRequestModel requestModel);
    }
}
