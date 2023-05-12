using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.TTA.Common;
using Materal.Utils.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.BusinessFlow.Abstractions.Services
{
    public class BaseServiceImpl<TDomain, TDTO, TRepository, TAddModel, TEditModel, TQueryModel> : IBaseService<TDomain, TDTO, TRepository, TAddModel, TEditModel, TQueryModel>
        where TDomain : class, IDomain
        where TDTO : class, IDTO
        where TRepository : IRepository<TDomain, Guid>
        where TAddModel : class, new()
        where TEditModel : class, IEditModel, new()
        where TQueryModel : PageRequestModel, new()
    {
        protected readonly IBusinessFlowUnitOfWork UnitOfWork;
        protected readonly TRepository DefaultRepository;
        protected BaseServiceImpl(IServiceProvider serviceProvider)
        {
            UnitOfWork = serviceProvider.GetRequiredService<IBusinessFlowUnitOfWork>();
            DefaultRepository = UnitOfWork.GetRepository<TRepository>();
        }
        public virtual async Task<Guid> AddAsync(TAddModel model)
        {
            model.Validation();
            TDomain domain = model.CopyProperties<TDomain>();
            UnitOfWork.RegisterAdd(domain);
            await UnitOfWork.CommitAsync();
            return domain.ID;
        }
        public virtual async Task DeleteAsync(Guid id)
        {
            TDomain domain = await DefaultRepository.FirstAsync(id);
            UnitOfWork.RegisterDelete(domain);
            await UnitOfWork.CommitAsync();
        }
        public virtual async Task EditAsync(TEditModel model)
        {
            model.Validation();
            TDomain domain = await DefaultRepository.FirstAsync(model.ID);
            model.CopyProperties(domain);
            domain.Validation();
            UnitOfWork.RegisterEdit(domain);
            await UnitOfWork.CommitAsync();
        }
        public virtual async Task<TDTO> GetInfoAsync(Guid id)
        {
            TDomain domain = await DefaultRepository.FirstAsync(id);
            if (domain is not TDTO result)
            {
                result = domain.CopyProperties<TDTO>();
            }
            return result;
        }
        public virtual async Task<List<TDTO>> GetListAsync(TQueryModel? queryModel = null)
        {
            queryModel ??= new TQueryModel();
            List<TDomain> domains = await DefaultRepository.FindAsync(queryModel, m => m.CreateTime, SortOrderEnum.Descending);
            if (domains is not List<TDTO> result)
            {
                result = domains.Select(m => m.CopyProperties<TDTO>()).ToList();
            }
            return result;
        }
        public virtual async Task<(List<TDTO> data, PageModel pageInfo)> PagingAsync(TQueryModel? queryModel = null)
        {
            queryModel ??= new TQueryModel();
            (List<TDomain> domains, PageModel pageInfo) = await DefaultRepository.PagingAsync(queryModel, m => m.CreateTime, SortOrderEnum.Descending);
            if (domains is not List<TDTO> result)
            {
                result = domains.Select(m => m.CopyProperties<TDTO>()).ToList();
            }
            return (result, pageInfo);
        }
    }
}
