﻿using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.Abstractions.WebModule.Controllers
{
    /// <summary>
    /// WebAPI控制器基类
    /// </summary>
    public interface IMergeBlockControllerBase
    {
    }
    /// <summary>
    /// WebAPI服务控制器基类
    /// </summary>
    /// <typeparam name="TAddRequestModel"></typeparam>
    /// <typeparam name="TEditRequestModel"></typeparam>
    /// <typeparam name="TQueryRequestModel"></typeparam>
    /// <typeparam name="TDTO"></typeparam>
    /// <typeparam name="TListDTO"></typeparam>
    public interface IMergeBlockControllerBase<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO> : IMergeBlockControllerBase
        where TAddRequestModel : class, IAddRequestModel, new()
        where TEditRequestModel : class, IEditRequestModel, new()
        where TQueryRequestModel : IQueryRequestModel, new()
        where TDTO : class, IDTO, new()
        where TListDTO : class, IListDTO, new()
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        Task<ResultModel<Guid>> AddAsync(TAddRequestModel requestModel);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        Task<ResultModel> EditAsync(TEditRequestModel requestModel);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        Task<ResultModel> DeleteAsync([Required(ErrorMessage = "唯一标识为空")] Guid id);
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        Task<ResultModel<TDTO>> GetInfoAsync([Required(ErrorMessage = "唯一标识为空")] Guid id);
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        Task<PageResultModel<TListDTO>> GetListAsync(TQueryRequestModel requestModel);
    }
}