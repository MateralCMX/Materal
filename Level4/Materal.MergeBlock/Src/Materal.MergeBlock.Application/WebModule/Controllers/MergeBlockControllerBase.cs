﻿using Materal.MergeBlock.Abstractions.WebModule;
using Materal.MergeBlock.Abstractions.WebModule.Controllers;

namespace Materal.MergeBlock.Application.WebModule.Controllers
{
    /// <summary>
    /// WebAPI控制器基类
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public abstract class MergeBlockControllerBase : ControllerBase
    {
        /// <summary>
        /// 获得客户端IP
        /// </summary>
        /// <returns></returns>
        protected string GetClientIP() => FilterHelper.GetIPAddress(HttpContext.Connection);
        /// <summary>
        /// 绑定LoginUserID
        /// </summary>
        protected void BindLoginUserID(object model)
        {
            var propertyInfos = model.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                if (!propertyInfo.CanWrite || propertyInfo.GetCustomAttribute<LoginUserIDAttribute>() is null) return;
                Guid? loginUserID = FilterHelper.GetOperatingUserID(User);
                if (propertyInfo.PropertyType == typeof(Guid) && loginUserID is not null || propertyInfo.PropertyType == typeof(Guid?))
                {
                    propertyInfo.SetValue(model, loginUserID);
                }
            }
        }
    }
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
    public abstract class MergeBlockControllerBase<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TAddModel, TEditModel, TQueryModel, TDTO, TListDTO, TService> : MergeBlockControllerBase, IMergeBlockControllerBase<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>
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
        private IMapper? _mapper;
        /// <summary>
        /// 自动映射
        /// </summary>
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
        private TService? _defaultService;
        /// <summary>
        /// 服务对象
        /// </summary>
        protected TService DefaultService => _defaultService ??= HttpContext.RequestServices.GetRequiredService<TService>();
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ResultModel<Guid>> AddAsync(TAddRequestModel requestModel)
        {
            var model = Mapper.Map<TAddModel>(requestModel);
            BindLoginUserID(model);
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
            var result = await DefaultService.AddAsync(model);
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
            var model = Mapper.Map<TEditModel>(requestModel);
            BindLoginUserID(model);
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
            var data = await DefaultService.GetInfoAsync(id);
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
            var model = Mapper.Map<TQueryModel>(requestModel);
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
            (var data, var pageInfo) = await DefaultService.GetListAsync(model);
            return PageResultModel<TListDTO>.Success(data, pageInfo, "获取成功");
        }
    }
}