using AutoMapper;
using Materal.Common;
using Materal.Model;
using Materal.TTA.EFRepository;
using RC.Core.Common;
using RC.Core.DataTransmitModel;
using RC.Core.Domain;
using RC.Core.Services;
using RC.Core.SqliteEFRepository;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;

namespace RC.Core.ServiceImpl
{
    public abstract class BaseServiceImpl<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO, TRepository, TDomain> : IBaseService<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO>
        where TAddModel : class, IServiceModel, new()
        where TEditModel : class, IEditServiceModel, IServiceModel, new()
        where TQueryModel : PageRequestModel, IQueryServiceModel, new()
        where TDTO : class, IDTO
        where TListDTO : class, IListDTO
        where TRepository : IEFRepository<TDomain, Guid>
        where TDomain : class, IDomain, new()
    {
        protected readonly IMapper Mapper;
        protected readonly IRCUnitOfWork UnitOfWork;
        protected readonly TRepository DefaultRepository;
        public BaseServiceImpl()
        {
            UnitOfWork = MateralServices.GetService<IRCUnitOfWork>();
            DefaultRepository = MateralServices.GetService<TRepository>();
            Mapper = MateralServices.GetService<IMapper>();
        }
        public virtual async Task<Guid> AddAsync(TAddModel model)
        {
            TDomain domain = Mapper.Map<TDomain>(model);
            return await AddAsync(domain, model);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="domain"></param
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        protected virtual async Task<Guid> AddAsync(TDomain domain, TAddModel model)
        {
            UnitOfWork.RegisterAdd(domain);
            await UnitOfWork.CommitAsync();
            await ClearCacheAsync();
            return domain.ID;
        }
        public virtual async Task DeleteAsync([Required(ErrorMessage = "唯一标识为空")]Guid id)
        {
            TDomain? domainFromDB = await DefaultRepository.FirstOrDefaultAsync(id);
            if (domainFromDB == null) throw new RCException("数据不存在");
            await DeleteAsync(domainFromDB);
        }
        protected virtual async Task DeleteAsync(TDomain domain)
        {
            UnitOfWork.RegisterDelete(domain);
            await UnitOfWork.CommitAsync();
            await ClearCacheAsync();
        }
        public virtual async Task EditAsync(TEditModel model)
        {
            TDomain? domainFromDB = await DefaultRepository.FirstOrDefaultAsync(model.ID);
            if (domainFromDB == null) throw new RCException("数据不存在");
            Mapper.Map(model, domainFromDB);
            await EditAsync(domainFromDB, model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="domainFromDB"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        protected virtual async Task EditAsync(TDomain domainFromDB, TEditModel model)
        {
            domainFromDB.UpdateTime = DateTime.Now;
            UnitOfWork.RegisterEdit(domainFromDB);
            await UnitOfWork.CommitAsync();
            await ClearCacheAsync();
        }
        public virtual async Task<TDTO> GetInfoAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            TDomain? domainFromDB = await DefaultRepository.FirstOrDefaultAsync(id);
            if (domainFromDB == null) throw new RCException("数据不存在");
            return await GetInfoAsync(domainFromDB);
        }
        protected virtual async Task<TDTO> GetInfoAsync(TDomain domain)
        {
            TDTO result = Mapper.Map<TDTO>(domain);
            return await GetInfoAsync(result);
        }
        protected virtual Task<TDTO> GetInfoAsync(TDTO dto)
        {
            return Task.FromResult(dto);
        }
        public virtual async Task<(List<TListDTO> data, PageModel pageInfo)> GetListAsync(TQueryModel model)
        {
            Expression<Func<TDomain, bool>> expression = model.GetSearchExpression<TDomain>();
            return await GetListAsync(expression, model, m=>m.CreateTime, SortOrder.Descending);
        }
        protected virtual async Task<(List<TListDTO> data, PageModel pageInfo)> GetListAsync(Expression<Func<TDomain, bool>> expression, TQueryModel model, Expression<Func<TDomain, object>>? orderExpression = null, SortOrder sortOrder = SortOrder.Descending)
        {
            orderExpression ??= m => m.CreateTime;
            (List<TDomain> data, PageModel pageModel) = await DefaultRepository.PagingAsync(expression, orderExpression, sortOrder, model);
            List<TListDTO> result = Mapper.Map<List<TListDTO>>(data);
            return await GetListAsync(result, pageModel, model);
        }
        protected virtual Task<(List<TListDTO> data, PageModel pageInfo)> GetListAsync(List<TListDTO> listDto, PageModel pageInfo, TQueryModel model)
        {
            return Task.FromResult((listDto, pageInfo));
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <param name="targetRepository"></param>
        /// <returns></returns>
        protected virtual async Task ClearCacheAsync(object targetRepository)
        {
            MethodInfo? methodInfo = targetRepository.GetType().GetMethod(nameof(ICacheEFRepository<TDomain, Guid>.ClearAllCacheAsync));
            if (methodInfo != null)
            {
                object? methodResult = methodInfo.Invoke(targetRepository, null);
                if (methodResult != null && methodResult is Task methodResultTask)
                {
                    await methodResultTask;
                }
            }
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <returns></returns>
        protected virtual async Task ClearCacheAsync()
        {
            await ClearCacheAsync(DefaultRepository);
        }
    }
    public abstract class BaseServiceImpl<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO, TRepository, TViewRepository, TDomain, TViewDomain> : BaseServiceImpl<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO, TRepository, TDomain>, IBaseService<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO>
        where TAddModel : class, IServiceModel, new()
        where TEditModel : class, IEditServiceModel, IServiceModel, new()
        where TQueryModel : PageRequestModel, IQueryServiceModel, new()
        where TDTO : class, IDTO
        where TListDTO : class, IListDTO
        where TRepository : IEFRepository<TDomain, Guid>
        where TViewRepository : IEFRepository<TViewDomain, Guid>
        where TDomain : class, IDomain, new()
        where TViewDomain : class, IDomain, new()
    {
        protected readonly TViewRepository DefaultViewRepository;
        public BaseServiceImpl() : base()
        {
            DefaultViewRepository = MateralServices.GetService<TViewRepository>();
        }
        public override async Task<TDTO> GetInfoAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            TViewDomain? domainFromDB = await DefaultViewRepository.FirstOrDefaultAsync(id);
            if (domainFromDB == null) throw new RCException("数据不存在");
            return await GetInfoAsync(domainFromDB);
        }
        protected virtual async Task<TDTO> GetInfoAsync(TViewDomain domain)
        {
            TDTO result = Mapper.Map<TDTO>(domain);
            return await GetInfoAsync(result);
        }
        public override async Task<(List<TListDTO> data, PageModel pageInfo)> GetListAsync(TQueryModel model)
        {
            Expression<Func<TViewDomain, bool>> expression = model.GetSearchExpression<TViewDomain>();
            return await GetViewListAsync(expression, model, m => m.CreateTime, SortOrder.Descending);
        }
        protected virtual async Task<(List<TListDTO> data, PageModel pageInfo)> GetViewListAsync(Expression<Func<TViewDomain, bool>> expression, TQueryModel model, Expression<Func<TViewDomain, object>>? orderExpression = null, SortOrder sortOrder = SortOrder.Descending)
        {
            orderExpression ??= m => m.CreateTime;
            (List<TViewDomain> data, PageModel pageModel) = await DefaultViewRepository.PagingAsync(expression, orderExpression, sortOrder, model);
            List<TListDTO> result = Mapper.Map<List<TListDTO>>(data);
            return await GetListAsync(result, pageModel, model);
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <returns></returns>
        protected override async Task ClearCacheAsync()
        {
            await ClearCacheAsync(DefaultViewRepository);
            await base.ClearCacheAsync();
        }
    }
}
