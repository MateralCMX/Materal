using AutoMapper;
using Materal.Abstractions;
using Materal.BaseCore.DataTransmitModel;
using Materal.BaseCore.PresentationModel;
using Materal.BaseCore.Services;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Materal.BaseCore.WebAPI.Controllers
{
    /// <summary>
    /// WebAPI服务控制器基类
    /// </summary>
    /// <typeparam name="TAddRequestModel"></typeparam>
    /// <typeparam name="TEditRequestModel"></typeparam>
    /// <typeparam name="TQueryRequestModel"></typeparam>
    /// <typeparam name="TAddModel"></typeparam>
    /// <typeparam name="TEditModel"></typeparam>
    /// <typeparam name="TQueryModel"></typeparam>
    /// <typeparam name="TDTO"></typeparam>
    /// <typeparam name="TListDTO"></typeparam>
    /// <typeparam name="TService"></typeparam>
    public abstract class MateralCoreWebAPIServiceControllerBase<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TAddModel, TEditModel, TQueryModel, TDTO, TListDTO, TService> : MateralCoreWebAPIControllerBase
        where TAddRequestModel : class, IAddRequestModel, new()
        where TEditRequestModel : class, IEditRequestModel, new()
        where TQueryRequestModel : IQueryRequestModel, new()
        where TAddModel : class, IAddServiceModel, new()
        where TEditModel : class, IEditServiceModel, new()
        where TQueryModel : IQueryServiceModel, new()
        where TDTO : class, IDTO, new()
        where TListDTO : class, IListDTO, new()
        where TService : IBaseService<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO>
    {
        /// <summary>
        /// 自动映射
        /// </summary>
        protected readonly IMapper Mapper;
        /// <summary>
        /// 服务对象
        /// </summary>
        protected readonly TService DefaultService;
        /// <summary>
        /// 构造方法
        /// </summary>
        protected MateralCoreWebAPIServiceControllerBase()
        {
            Mapper = MateralServices.GetService<IMapper>();
            DefaultService = MateralServices.GetService<TService>(); ;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ResultModel<Guid>> AddAsync(TAddRequestModel requestModel)
        {
            TAddModel model = Mapper.Map<TAddModel>(requestModel);
            return await AddAsync(model, requestModel);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        protected virtual async Task<ResultModel<Guid>> AddAsync(TAddModel model, TAddRequestModel requestModel)
        {
            Guid result = await DefaultService.AddAsync(model);
            return ResultModel<Guid>.Success(result, "添加成功");
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public virtual async Task<ResultModel> EditAsync(TEditRequestModel requestModel)
        {
            TEditModel model = Mapper.Map<TEditModel>(requestModel);
            return await EditAsync(model, requestModel);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        protected virtual async Task<ResultModel> EditAsync(TEditModel model, TEditRequestModel requestModel)
        {
            await DefaultService.EditAsync(model);
            return ResultModel.Success("修改成功");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public virtual async Task<ResultModel> DeleteAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            await DefaultService.DeleteAsync(id);
            return ResultModel.Success("删除成功");
        }
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ResultModel<TDTO>> GetInfoAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            TDTO data = await DefaultService.GetInfoAsync(id);
            return ResultModel<TDTO>.Success(data, "获取成功");
        }
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<PageResultModel<TListDTO>> GetListAsync(TQueryRequestModel requestModel)
        {
            TQueryModel model = Mapper.Map<TQueryModel>(requestModel);
            return await GetListAsync(model, requestModel);
        }
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        protected virtual async Task<PageResultModel<TListDTO>> GetListAsync(TQueryModel model, TQueryRequestModel requestModel)
        {
            (List<TListDTO> data, PageModel pageInfo) = await DefaultService.GetListAsync(model);
            return PageResultModel<TListDTO>.Success(data, pageInfo, "获取成功");
        }
        public override void Dispose()
        {
            DefaultService.Dispose();
        }
    }
}