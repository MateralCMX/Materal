using Deploy.DataTransmitModel.DefaultData;
using Deploy.Services.Models.DefaultData;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.Services
{
    public interface IDefaultDataService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [DataValidation]
        Task AddAsync(AddDefaultDataModel model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [DataValidation]
        Task EditAsync(EditDefaultDataModel model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataValidation]
        Task DeleteAsync(Guid id);
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DataValidation]
        Task<DefaultDataDTO> GetInfoAsync(Guid id);
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [DataValidation]
        Task<(List<DefaultDataListDTO> defaultDataList, PageModel pageModel)> GetListAsync(QueryDefaultDataFilterModel model);
    }
}
