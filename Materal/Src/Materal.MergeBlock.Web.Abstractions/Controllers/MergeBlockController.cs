using Materal.MergeBlock.Abstractions.Models;
using Materal.MergeBlock.Abstractions.Services;
using Materal.MergeBlock.GeneratorCode.Attributers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Materal.MergeBlock.Web.Abstractions.Controllers
{
    /// <summary>
    /// WebAPI控制器基类
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public abstract class MergeBlockController : ControllerBase
    {
        private IMapper? _mapper;
        /// <summary>
        /// 自动映射
        /// </summary>
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
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
            PropertyInfo[] propertyInfos = model.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                if (!propertyInfo.CanWrite || propertyInfo.GetCustomAttribute<LoginUserIDAttribute>() is null) continue;
                Guid? loginUserID = FilterHelper.GetOperatingUserID(User);
                if ((loginUserID is null || propertyInfo.PropertyType != typeof(Guid)) && propertyInfo.PropertyType != typeof(Guid?)) continue;
                propertyInfo.SetValue(model, loginUserID);
            }
        }
    }
    /// <summary>
    /// WebAPI服务控制器基类
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public abstract class MergeBlockController<TService> : MergeBlockController
        where TService : IBaseService
    {
        private TService? _defaultService;
        /// <summary>
        /// 服务对象
        /// </summary>
        protected TService DefaultService => _defaultService ??= HttpContext.RequestServices.GetRequiredService<TService>();
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
    public abstract class MergeBlockController<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TAddModel, TEditModel, TQueryModel, TDTO, TListDTO, TService> : MergeBlockController<TService>, IMergeBlockController<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>
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
        /// 添加
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ResultModel<Guid>> AddAsync(TAddRequestModel requestModel)
        {
            TAddModel model = Mapper.Map<TAddModel>(requestModel) ?? throw new MergeBlockException("映射失败");
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
            TEditModel model = Mapper.Map<TEditModel>(requestModel) ?? throw new MergeBlockException("映射失败");
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
        public virtual async Task<CollectionResultModel<TListDTO>> GetListAsync(TQueryRequestModel requestModel)
        {
            TQueryModel model = Mapper.Map<TQueryModel>(requestModel) ?? throw new MergeBlockException("映射失败");
            return await GetListAsync(model, requestModel);
        }
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        protected virtual async Task<CollectionResultModel<TListDTO>> GetListAsync(TQueryModel model, TQueryRequestModel requestModel)
        {
            (List<TListDTO>? data, RangeModel? pageInfo) = await DefaultService.GetListAsync(model);
            return CollectionResultModel<TListDTO>.Success(data, pageInfo, "获取成功");
        }
    }
}