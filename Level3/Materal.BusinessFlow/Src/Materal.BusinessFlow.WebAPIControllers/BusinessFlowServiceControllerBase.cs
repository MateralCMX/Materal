using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.WebAPIControllers.Controllers
{
    /// <summary>
    /// 业务流服务控制器基类
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TQueryModel"></typeparam>
    public class BusinessFlowServiceControllerBase<TDomain, TDTO, TService, TAddModel, TEditModel, TQueryModel> : BusinessFlowControllerBase
        where TDomain : class, IDomain
        where TDTO : class, IDTO
        where TService : IBaseService<TDomain, TDTO, TAddModel, TEditModel, TQueryModel>
        where TQueryModel : class, new()
        where TAddModel : class, new()
        where TEditModel : class, IEditModel, new()
    {
        protected readonly TService DefaultService;
        /// <summary>
        /// 构造方法
        /// </summary>
        protected BusinessFlowServiceControllerBase(IServiceProvider service)
        {
            DefaultService = service.GetRequiredService<TService>();
        }
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<TDTO>> GetInfoAsync([Required] Guid id)
        {
            TDTO result = await DefaultService.GetInfoAsync(id);
            return ResultModel<TDTO>.Success(result, "查询成功");
        }
        /// <summary>
        /// 获得所有列表
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel<List<TDTO>>> GetAllListAsync(TQueryModel queryModel)
        {
            List<TDTO> result = await DefaultService.GetListAsync(queryModel);
            return ResultModel<List<TDTO>>.Success(result, "查询成功");
        }
        /// <summary>
        /// 获得分页列表
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResultModel<TDTO>> GetListAsync(TQueryModel queryModel)
        {
            (List<TDTO> result, PageModel pageInfo) = await DefaultService.PagingAsync(queryModel);
            return PageResultModel<TDTO>.Success(result, pageInfo, "查询成功");
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<Guid>> AddAsync(TAddModel model)
        {
            Guid result = await DefaultService.AddAsync(model);
            return ResultModel<Guid>.Success(result, "添加成功");
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditAsync(TEditModel model)
        {
            await DefaultService.EditAsync(model);
            return ResultModel.Success("修改成功");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultModel> DeleteAsync([Required]Guid id)
        {
            await DefaultService.DeleteAsync(id);
            return ResultModel.Success("删除成功");
        }
    }
}