using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Services;
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
    public class BusinessFlowServiceControllerBase<TDomain, TService, TQueryModel, TAddModel, TEditModel> : BusinessFlowControllerBase
        where TDomain : class, IDomain
        where TService : IBaseService<TDomain, TQueryModel>
        where TQueryModel : class, new()
        where TAddModel : class
        where TEditModel : class
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
        public async Task<ResultModel<TDomain>> GetInfoAsync([Required] Guid id)
        {
            TDomain result = await DefaultService.GetInfoAsync(id);
            return ResultModel<TDomain>.Success(result, "查询成功");
        }
        /// <summary>
        /// 获得所有列表
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel<List<TDomain>>> GetAllListAsync(TQueryModel queryModel)
        {
            List<TDomain> result = await DefaultService.GetListAsync(queryModel);
            return ResultModel<List<TDomain>>.Success(result, "查询成功");
        }
        /// <summary>
        /// 获得分页列表
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResultModel<TDomain>> GetListAsync(TQueryModel queryModel)
        {
            (List<TDomain> result, PageModel pageInfo) = await DefaultService.PagingAsync(queryModel);
            return PageResultModel<TDomain>.Success(result, pageInfo, "查询成功");
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<Guid>> AddAsync(TAddModel model)
        {
            TDomain domain = model.CopyProperties<TDomain>();
            Guid result = await DefaultService.AddAsync(domain);
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
            TDomain domain = model.CopyProperties<TDomain>();
            await DefaultService.EditAsync(domain);
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