using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.TTA.Common;
using Materal.Utils.Model;

namespace Materal.BusinessFlow.Abstractions.Services
{
    public interface IBaseService<TDomain, TDTO, TAddModel, TEditModel>
        where TDomain : class, IDomain
        where TDTO : class, IDTO
        where TAddModel : class, new()
        where TEditModel : class, IEditModel, new()
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Guid> AddAsync(TAddModel model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditAsync(TEditModel model);
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
        Task<TDTO> GetInfoAsync(Guid id);
    }
    public interface IBaseService<TDomain, TDTO, TAddModel, TEditModel, TQueryModel> : IBaseService<TDomain, TDTO, TAddModel, TEditModel>
        where TDomain : class, IDomain
        where TDTO : class, IDTO
        where TAddModel : class, new()
        where TEditModel : class, IEditModel, new()
        where TQueryModel : class, new()
    {
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<List<TDTO>> GetListAsync(TQueryModel? queryModel = null);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        Task<(List<TDTO> data, PageModel pageInfo)> PagingAsync(TQueryModel? queryModel = null);
    }
    public interface IBaseService<TDomain, TDTO, TRepository, TAddModel, TEditModel, TQueryModel> : IBaseService<TDomain, TDTO, TAddModel, TEditModel, TQueryModel>
        where TDomain : class, IDomain
        where TDTO : class, IDTO
        where TRepository : IRepository<TDomain>
        where TAddModel : class, new()
        where TEditModel : class, IEditModel, new()
        where TQueryModel : class, new()
    {
    }
}
