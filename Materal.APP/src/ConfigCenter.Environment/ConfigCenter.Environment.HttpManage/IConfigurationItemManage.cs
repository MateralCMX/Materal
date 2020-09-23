using ConfigCenter.Environment.DataTransmitModel.ConfigurationItem;
using ConfigCenter.Environment.PresentationModel.ConfigurationItem;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigCenter.Environment.HttpManage
{
    public interface IConfigurationItemManage
    {
        /// <summary>
        /// 添加配置项
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> AddConfigurationItemAsync(AddConfigurationItemRequestModel requestModel);

        /// <summary>
        /// 修改配置项
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> EditConfigurationItemAsync(EditConfigurationItemRequestModel requestModel);

        /// <summary>
        /// 删除配置项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel> DeleteConfigurationItemAsync(Guid id);

        /// <summary>
        /// 获得配置项信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel<ConfigurationItemDTO>> GetConfigurationItemInfoAsync(Guid id);

        /// <summary>
        /// 获得配置项列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel<List<ConfigurationItemListDTO>>> GetConfigurationItemListAsync(QueryConfigurationItemFilterRequestModel requestModel);
    }
}
