using Deploy.DataTransmitModel.DefaultData;
using Deploy.PresentationModel.DefaultData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.Model;

namespace Deploy.HttpManage
{
    public interface IDefaultDataManage
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> AddAsync(AddDefaultDataRequestModel requestModel);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> EditAsync(EditDefaultDataRequestModel requestModel);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel> DeleteAsync(Guid id);
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel<DefaultDataDTO>> GetInfoAsync(Guid id);
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<PageResultModel<DefaultDataListDTO>> GetListAsync(QueryDefaultDataFilterRequestModel requestModel);
    }
}
