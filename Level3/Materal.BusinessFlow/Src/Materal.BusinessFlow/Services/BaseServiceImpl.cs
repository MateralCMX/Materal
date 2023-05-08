using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.TTA.Common;
using Materal.Utils.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.BusinessFlow.Services
{
    public abstract class BaseServiceImpl<TDomain, TRepository, TQueryModel> : IBaseService<TDomain, TRepository, TQueryModel>
        where TDomain : class, IDomain
        where TRepository : IRepository<TDomain, Guid>
        where TQueryModel : PageRequestModel, new()
    {
        protected readonly IBusinessFlowUnitOfWork UnitOfWork;
        protected readonly TRepository DefaultRepository;
        protected BaseServiceImpl(IServiceProvider serviceProvider)
        {
            UnitOfWork = serviceProvider.GetRequiredService<IBusinessFlowUnitOfWork>();
            DefaultRepository = UnitOfWork.GetRepository<TRepository>();
        }
        public virtual async Task<Guid> AddAsync(TDomain model)
        {
            UnitOfWork.RegisterAdd(model);
            await UnitOfWork.CommitAsync();
            return model.ID;
        }
        public virtual async Task DeleteAsync(Guid id)
        {
            TDomain domain = await DefaultRepository.FirstAsync(id);
            UnitOfWork.RegisterDelete(domain);
            await UnitOfWork.CommitAsync();
        }
        public virtual async Task EditAsync(TDomain model)
        {
            TDomain domain = await DefaultRepository.FirstAsync(model.ID);
            model.CopyProperties(domain);
            UnitOfWork.RegisterEdit(domain);
            await UnitOfWork.CommitAsync();
        }
        public virtual async Task<TDomain> GetInfoAsync(Guid id)
        {
            TDomain domain = await DefaultRepository.FirstAsync(id);
            return domain;
        }
        public virtual async Task<List<TDomain>> GetListAsync(TQueryModel? queryModel = null)
        {
            queryModel ??= new TQueryModel();
            List<TDomain> domains = await DefaultRepository.FindAsync(queryModel);
            return domains;
        }
        public virtual async Task<(List<TDomain> data, PageModel pageInfo)> PagingAsync(TQueryModel? queryModel = null)
        {
            queryModel ??= new TQueryModel();
            (List<TDomain> data, PageModel pageInfo) = await DefaultRepository.PagingAsync(queryModel);
            return (data, pageInfo);
        }
    }
}
