using Materal.MergeBlock.Abstractions.Models;
using Materal.MergeBlock.Web.Abstractions.ControllerHttpHelper;
using Materal.MergeBlock.Web.Abstractions.Controllers;
using Materal.Utils.Model;
using System.ComponentModel.DataAnnotations;

namespace Materal.MergeBlock.Web.Abstractions
{
    /// <summary>
    /// 控制器基类访问器
    /// </summary>
    /// <param name="serviceProvider"></param>
    public abstract class BaseControllerAccessor(IServiceProvider serviceProvider) : IMergeBlockController
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public abstract string ProjectName { get; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public abstract string ModuleName { get; }
        /// <summary>
        /// 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; } = serviceProvider;
        /// <summary>
        /// 控制器HTTP帮助类
        /// </summary>
        protected readonly IControllerHttpHelper HttpHelper = serviceProvider.GetService<IControllerHttpHelper>() ?? typeof(DefaultControllerHttpHelper).Instantiation<IControllerHttpHelper>(serviceProvider);
    }
    /// <summary>
    /// 控制器基类访问器
    /// </summary>
    /// <param name="serviceProvider"></param>
    public abstract class BaseControllerAccessor<TController, TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>(IServiceProvider serviceProvider) : BaseControllerAccessor(serviceProvider), IMergeBlockController<TAddRequestModel, TEditRequestModel, TQueryRequestModel, TDTO, TListDTO>
        where TController : IMergeBlockController
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
        public async Task<ResultModel<Guid>> AddAsync(TAddRequestModel requestModel)
            => await HttpHelper.SendAsync<TController, ResultModel<Guid>>(ProjectName, ModuleName, nameof(AddAsync), [], requestModel);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultModel> DeleteAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
            => await HttpHelper.SendAsync<TController, ResultModel>(ProjectName, ModuleName, nameof(DeleteAsync), new() { [nameof(id)] = id.ToString() });
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<ResultModel> EditAsync(TEditRequestModel requestModel)
            => await HttpHelper.SendAsync<TController, ResultModel>(ProjectName, ModuleName, nameof(EditAsync), [], requestModel);
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultModel<TDTO>> GetInfoAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
            => await HttpHelper.SendAsync<TController, ResultModel<TDTO>>(ProjectName, ModuleName, nameof(GetInfoAsync), new() { [nameof(id)] = id.ToString() });
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<CollectionResultModel<TListDTO>> GetListAsync(TQueryRequestModel requestModel)
            => await HttpHelper.SendAsync<TController, CollectionResultModel<TListDTO>>(ProjectName, ModuleName, nameof(GetListAsync), [], requestModel);
    }
}
