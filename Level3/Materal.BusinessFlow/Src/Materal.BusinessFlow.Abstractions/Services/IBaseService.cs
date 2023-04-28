using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.Utils.Model;

namespace Materal.BusinessFlow.Abstractions.Services
{
    public interface IBaseService<TDomain>
        where TDomain : class, IBaseDomain
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Guid> AddAsync(TDomain model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditAsync(TDomain model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TDomain> GetInfoAsync(Guid id);
    }
    public interface IBaseService<TDomain, TQueryModel> : IBaseService<TDomain>
        where TDomain : class, IBaseDomain
        where TQueryModel : class, new()
    {
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<List<TDomain>> GetListAsync(TQueryModel? queryModel = null);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        Task<(List<TDomain> data, PageModel pageInfo)> PagingAsync(TQueryModel? queryModel = null);
    }
    public interface IBaseService<TDomain, TRepository, TQueryModel> : IBaseService<TDomain, TQueryModel>
        where TDomain : class, IBaseDomain
        where TRepository : IBaseRepository<TDomain>
        where TQueryModel : class, new()
    {
    }
}
