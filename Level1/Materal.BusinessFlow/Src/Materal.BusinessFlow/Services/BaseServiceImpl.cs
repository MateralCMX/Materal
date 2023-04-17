using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.Utils.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.BusinessFlow.Services
{
    public abstract class BaseServiceImpl<TDomain, TRepository, TQueryModel> : IBaseService<TDomain, TRepository, TQueryModel>
        where TDomain : class, IBaseDomain
        where TRepository : IBaseRepository<TDomain>
        where TQueryModel : class, IQueryModel, new()
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly TRepository DefaultRepository;
        protected BaseServiceImpl(IServiceProvider serviceProvider)
        {
            UnitOfWork = serviceProvider.GetService<IUnitOfWork>() ?? throw new BusinessFlowException("获取工作单元失败");
            DefaultRepository = UnitOfWork.GetRepository<TRepository>();
        }
        public virtual async Task<Guid> AddAsync(TDomain model)
        {
            UnitOfWork.RegisterAdd(model);
            await UnitOfWork.CommitAsync();
            return model.ID;
        }
        public async Task DeleteAsync(Guid id)
        {
            TDomain domain = await DefaultRepository.FirstAsync(id);
            UnitOfWork.RegisterDelete(domain);
            await UnitOfWork.CommitAsync();
        }
        public async Task EditAsync(TDomain model)
        {
            TDomain domain = await DefaultRepository.FirstAsync(model.ID);
            model.CopyProperties(domain);
            UnitOfWork.RegisterEdit(domain);
            await UnitOfWork.CommitAsync();
        }
        public async Task<TDomain> GetInfoAsync(Guid id)
        {
            TDomain domain = await DefaultRepository.FirstAsync(id);
            return domain;
        }
        public async Task<List<TDomain>> GetListAsync(TQueryModel? queryModel = null)
        {
            List<TDomain> domains = await DefaultRepository.GetListAsync(queryModel);
            return domains;
        }

        public async Task<(List<TDomain> data, PageModel pageInfo)> PagingAsync(TQueryModel? queryModel = null)
        {
            (List<TDomain> data, PageModel pageInfo) = await DefaultRepository.PagingAsync(queryModel);
            return (data, pageInfo);
        }
    }
}
