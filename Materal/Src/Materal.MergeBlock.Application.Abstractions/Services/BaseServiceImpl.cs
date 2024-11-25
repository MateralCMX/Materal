using AspectCore.DependencyInjection;

namespace Materal.MergeBlock.Application.Abstractions.Services
{
    /// <summary>
    /// 基础服务实现
    /// </summary>
    public abstract class BaseServiceImpl : IBaseService
    {
        private Guid? _loginUserID;
        /// <summary>
        /// 登录用户唯一标识
        /// </summary>
        public Guid LoginUserID { get => _loginUserID ?? throw new MergeBlockException("没有登录用户"); set => _loginUserID = value; }
        private IMapper? _mapper;
        /// <summary>
        /// 映射器
        /// </summary>
        [FromServiceContext]
        protected IMapper Mapper { get => _mapper ?? throw new MergeBlockException("未设置映射器"); set => _mapper = value; }
    }
    /// <summary>
    /// 基础服务实现
    /// </summary>
    /// <typeparam name="TUnitOfWork"></typeparam>
    public abstract class BaseServiceImpl<TUnitOfWork> : BaseServiceImpl, IBaseService
        where TUnitOfWork : IMergeBlockUnitOfWork
    {
        private TUnitOfWork? _unitOfWork;
        /// <summary>
        /// 工作单元
        /// </summary>
        [FromServiceContext]
        protected TUnitOfWork UnitOfWork { get => _unitOfWork ?? throw new MergeBlockException("未设置工作单元"); set => _unitOfWork = value; }
    }
    /// <summary>
    /// 基础服务实现
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    /// <typeparam name="TDomain"></typeparam>
    /// <typeparam name="TUnitOfWork"></typeparam>
    public abstract class BaseServiceImpl<TRepository, TDomain, TUnitOfWork> : BaseServiceImpl<TUnitOfWork>, IBaseService
        where TRepository : class, IEFRepository<TDomain, Guid>, IRepository
        where TDomain : class, IDomain, new()
        where TUnitOfWork : IMergeBlockUnitOfWork
    {
        private TRepository? _defaultRepository;
        /// <summary>
        /// 默认仓储
        /// </summary>
        protected TRepository DefaultRepository => _defaultRepository ??= UnitOfWork.GetRepository<TRepository>();
    }
    /// <summary>
    /// 基础服务实现
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    /// <typeparam name="TViewRepository"></typeparam>
    /// <typeparam name="TDomain"></typeparam>
    /// <typeparam name="TViewDomain"></typeparam>
    /// <typeparam name="TUnitOfWork"></typeparam>
    public abstract class BaseServiceImpl<TRepository, TViewRepository, TDomain, TViewDomain, TUnitOfWork> : BaseServiceImpl<TRepository, TDomain, TUnitOfWork>, IBaseService
        where TRepository : class, IEFRepository<TDomain, Guid>, IRepository
        where TViewRepository : class, IEFRepository<TViewDomain, Guid>, IRepository
        where TDomain : class, IDomain, new()
        where TViewDomain : class, IDomain, new()
        where TUnitOfWork : IMergeBlockUnitOfWork
    {
        private TViewRepository? _defaultViewRepository;
        /// <summary>
        /// 默认视图仓储
        /// </summary>
        protected TViewRepository DefaultViewRepository => _defaultViewRepository ??= UnitOfWork.GetRepository<TViewRepository>();
    }
    /// <summary>
    /// 基础服务实现
    /// </summary>
    /// <typeparam name="TAddModel"></typeparam>
    /// <typeparam name="TEditModel"></typeparam>
    /// <typeparam name="TQueryModel"></typeparam>
    /// <typeparam name="TDTO"></typeparam>
    /// <typeparam name="TListDTO"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    /// <typeparam name="TDomain"></typeparam>
    /// <typeparam name="TUnitOfWork"></typeparam>
    public abstract class BaseServiceImpl<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO, TRepository, TDomain, TUnitOfWork> : BaseServiceImpl<TRepository, TDomain, TUnitOfWork>, IBaseService<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO>
        where TAddModel : class, IAddServiceModel, new()
        where TEditModel : class, IEditServiceModel, new()
        where TQueryModel : PageRequestModel, IQueryServiceModel, new()
        where TDTO : class, IDTO
        where TListDTO : class, IListDTO
        where TRepository : class, IEFRepository<TDomain, Guid>, IRepository
        where TDomain : class, IDomain, new()
        where TUnitOfWork : IMergeBlockUnitOfWork
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        public virtual async Task<Guid> AddAsync(TAddModel model)
        {
            TDomain domain = Mapper.Map<TDomain>(model) ?? throw new MergeBlockException("映射失败");
            return await AddAsync(domain, model);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        protected virtual async Task<Guid> AddAsync(TDomain domain, TAddModel model)
        {
            if (domain is IIndexDomain indexDomain)
            {
                MethodInfo? getMaxIndexAsyncMethodInfo = DefaultRepository.GetType().GetMethod("GetMaxIndexAsync");
                if (getMaxIndexAsyncMethodInfo != null)
                {
                    ParameterInfo[] parameterInfos = getMaxIndexAsyncMethodInfo.GetParameters();
                    object?[] args = new object?[parameterInfos.Length];
                    Type domainType = domain.GetType();
                    for (int i = 0; i < parameterInfos.Length; i++)
                    {
                        string? name = parameterInfos[i].Name;
                        if (name == null || string.IsNullOrWhiteSpace(name)) continue;
                        name = name.FirstUpper();
                        PropertyInfo? domainPropertyInfo = domainType.GetProperty(name);
                        if (domainPropertyInfo == null) continue;
                        args[i] = domainPropertyInfo.GetValue(domain);
                    }
                    object? maxIndexResult = getMaxIndexAsyncMethodInfo.Invoke(DefaultRepository, args);
                    if (maxIndexResult != null && maxIndexResult is Task<int> taskMaxIndexResult)
                    {
                        indexDomain.Index = await taskMaxIndexResult + 1;
                    }
                }
            }
            UnitOfWork.RegisterAdd(domain);
            await UnitOfWork.CommitAsync();
            await ClearCacheAsync();
            return domain.ID;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        public virtual async Task DeleteAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            TDomain domainFromDB = await DefaultRepository.FirstOrDefaultAsync(id) ?? throw new MergeBlockModuleException("数据不存在");
            await DeleteAsync(domainFromDB);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        protected virtual async Task DeleteAsync(TDomain domain)
        {
            UnitOfWork.RegisterDelete(domain);
            await UnitOfWork.CommitAsync();
            await ClearCacheAsync();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        public virtual async Task EditAsync(TEditModel model)
        {
            TDomain domainFromDB = await DefaultRepository.FirstOrDefaultAsync(model.ID) ?? throw new MergeBlockModuleException("数据不存在");
            Mapper.Map(model, domainFromDB);
            await EditAsync(domainFromDB, model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="domainFromDB"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        protected virtual async Task EditAsync(TDomain domainFromDB, TEditModel model)
        {
            UnitOfWork.RegisterEdit(domainFromDB);
            await UnitOfWork.CommitAsync();
            await ClearCacheAsync();
        }
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        public virtual async Task<TDTO> GetInfoAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            TDomain domainFromDB = await DefaultRepository.FirstOrDefaultAsync(id) ?? throw new MergeBlockModuleException("数据不存在");
            return await GetInfoAsync(domainFromDB);
        }
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        protected virtual async Task<TDTO> GetInfoAsync(TDomain domain)
        {
            TDTO result = Mapper.Map<TDTO>(domain) ?? throw new MergeBlockException("映射失败");
            return await GetInfoAsync(result);
        }
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        protected virtual Task<TDTO> GetInfoAsync(TDTO dto) => Task.FromResult(dto);
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<(List<TListDTO> data, RangeModel rangeInfo)> GetListAsync(TQueryModel model)
        {
            Expression<Func<TDomain, bool>> expression = model.GetSearchExpression<TDomain>();
            expression = ServiceImplHelper.GetSearchTreeDomainExpression(expression, model);
            return await GetListAsync(expression, model);
        }
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        protected virtual async Task<(List<TListDTO> data, RangeModel rangeInfo)> GetListAsync(Expression<Func<TDomain, bool>> expression, TQueryModel model, Expression<Func<TDomain, object>>? orderExpression = null, SortOrderEnum sortOrder = SortOrderEnum.Descending)
        {
            if (orderExpression == null)
            {
                (orderExpression, sortOrder) = GetDefaultOrderInfo<TDomain>(model);
            }
            (List<TDomain> data, RangeModel pageModel) = await DefaultRepository.RangeAsync(expression, orderExpression, sortOrder, model);
            List<TListDTO> result = Mapper.Map<List<TListDTO>>(data) ?? throw new MergeBlockException("映射失败");
            return await GetListAsync(result, pageModel, model);
        }
        /// <summary>
        /// 获得默认排序信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected (Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder) GetDefaultOrderInfo<T>(TQueryModel model)
            where T : class, IDomain
        {
            Expression<Func<T, object>>? result = model.GetSortExpression<T>();
            SortOrderEnum sortOrder;
            if (result != null)
            {
                sortOrder = model.IsAsc ? SortOrderEnum.Ascending : SortOrderEnum.Descending;
            }
            else
            {
                if (typeof(T).GetInterfaces().Contains(typeof(IIndexDomain)))
                {
                    result = m => ((IIndexDomain)m).Index;
                    sortOrder = SortOrderEnum.Ascending;
                }
                else
                {
                    result = m => m.CreateTime;
                    sortOrder = SortOrderEnum.Descending;
                }
            }
            return (result, sortOrder);
        }
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="listDto"></param>
        /// <param name="rangeInfo"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual Task<(List<TListDTO> data, RangeModel rangeInfo)> GetListAsync(List<TListDTO> listDto, RangeModel rangeInfo, TQueryModel model) => Task.FromResult((listDto, rangeInfo));
        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <param name="targetRepository"></param>
        /// <returns></returns>
        protected virtual async Task ClearCacheAsync(object targetRepository)
        {
            if (targetRepository is not ICacheRepository<TDomain> cacheRepository) return;
            await cacheRepository.ClearAllCacheAsync();
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
    /// <summary>
    /// 基础服务实现
    /// </summary>
    /// <typeparam name="TAddModel"></typeparam>
    /// <typeparam name="TEditModel"></typeparam>
    /// <typeparam name="TQueryModel"></typeparam>
    /// <typeparam name="TDTO"></typeparam>
    /// <typeparam name="TListDTO"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    /// <typeparam name="TViewRepository"></typeparam>
    /// <typeparam name="TDomain"></typeparam>
    /// <typeparam name="TViewDomain"></typeparam>
    /// <typeparam name="TUnitOfWork"></typeparam>
    public abstract class BaseServiceImpl<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO, TRepository, TViewRepository, TDomain, TViewDomain, TUnitOfWork> : BaseServiceImpl<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO, TRepository, TDomain, TUnitOfWork>, IBaseService<TAddModel, TEditModel, TQueryModel, TDTO, TListDTO>
        where TAddModel : class, IAddServiceModel, new()
        where TEditModel : class, IEditServiceModel, new()
        where TQueryModel : PageRequestModel, IQueryServiceModel, new()
        where TDTO : class, IDTO
        where TListDTO : class, IListDTO
        where TRepository : class, IEFRepository<TDomain, Guid>, IRepository
        where TViewRepository : class, IEFRepository<TViewDomain, Guid>, IRepository
        where TDomain : class, IDomain, new()
        where TViewDomain : class, IDomain, new()
        where TUnitOfWork : IMergeBlockUnitOfWork
    {
        private TViewRepository? _defaultViewRepository;
        /// <summary>
        /// 默认视图仓储
        /// </summary>
        protected TViewRepository DefaultViewRepository => _defaultViewRepository ??= UnitOfWork.GetRepository<TViewRepository>();
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        public override async Task<TDTO> GetInfoAsync([Required(ErrorMessage = "唯一标识为空")] Guid id)
        {
            TViewDomain domainFromDB = await DefaultViewRepository.FirstOrDefaultAsync(id) ?? throw new MergeBlockModuleException("数据不存在");
            return await GetInfoAsync(domainFromDB);
        }
        /// <summary>
        /// 获得信息
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        protected virtual async Task<TDTO> GetInfoAsync(TViewDomain domain)
        {
            TDTO result = Mapper.Map<TDTO>(domain) ?? throw new MergeBlockException("映射失败");
            return await GetInfoAsync(result);
        }
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<(List<TListDTO> data, RangeModel rangeInfo)> GetListAsync(TQueryModel model)
        {
            Expression<Func<TViewDomain, bool>> expression = model.GetSearchExpression<TViewDomain>();
            expression = ServiceImplHelper.GetSearchTreeDomainExpression(expression, model);
            return await GetViewListAsync(expression, model);
        }
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        protected virtual async Task<(List<TListDTO> data, RangeModel rangeInfo)> GetViewListAsync(Expression<Func<TViewDomain, bool>> expression, TQueryModel model, Expression<Func<TViewDomain, object>>? orderExpression = null, SortOrderEnum sortOrder = SortOrderEnum.Descending)
        {
            if (orderExpression == null)
            {
                (orderExpression, sortOrder) = GetDefaultOrderInfo<TViewDomain>(model);
            }
            (List<TViewDomain> data, RangeModel rangeModel) = await DefaultViewRepository.RangeAsync(expression, orderExpression, sortOrder, model.SkipInt, model.TakeInt);
            List<TListDTO> result = Mapper.Map<List<TListDTO>>(data) ?? throw new MergeBlockException("映射失败");
            return await GetListAsync(result, rangeModel, model);
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
