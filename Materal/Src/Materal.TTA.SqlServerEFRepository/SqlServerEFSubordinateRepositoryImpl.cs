namespace Materal.TTA.SqlServerEFRepository
{
    /// <summary>
    /// SqlServerEF读写分离仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class SqlServerEFSubordinateRepositoryImpl<T, TPrimaryKeyType, TDBContext> : SqlServerEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>, IEFSubordinateRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TDBContext : DbContext
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 从属数据库
        /// </summary>
        protected readonly TDBContext SubordinateDB;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="subordinateConfigs"></param>
        /// <param name="optionAction"></param>
        protected SqlServerEFSubordinateRepositoryImpl(TDBContext dbContext, IEnumerable<SqlServerSubordinateConfigModel> subordinateConfigs, Action<DbContextOptionsBuilder, string> optionAction) : base(dbContext)
        {
            Type type = typeof(TDBContext);
            SqlServerSubordinateConfigModel config = SqlServerEFSubordinateRepositoryImpl<T, TPrimaryKeyType, TDBContext>.GetConfig(subordinateConfigs.ToList());
            DbContextOptions<TDBContext> contextOptions = new DbContextOptions<TDBContext>();
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder(contextOptions);
            optionAction(optionsBuilder, config.ConnectionString);
            DbContextOptions<TDBContext> arg = (DbContextOptions<TDBContext>)optionsBuilder.Options;
            SubordinateDB = (TDBContext)type.Instantiation(new object[] { arg });
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool ExistedFromSubordinate(TPrimaryKeyType id) => GetSubordinateResult(queryable => queryable.Any(m => m.ID.Equals(id)));
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistedFromSubordinateAsync(TPrimaryKeyType id) => await GetSubordinateResultAsync(async queryable => await queryable.AnyAsync(m => m.ID.Equals(id)));
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool ExistedFromSubordinate(Expression<Func<T, bool>> expression) => GetSubordinateResult(queryable => queryable.Any(expression));
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<bool> ExistedFromSubordinateAsync(Expression<Func<T, bool>> expression) => await GetSubordinateResultAsync(async queryable => await queryable.AnyAsync(expression));
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public bool ExistedFromSubordinate(FilterModel filterModel) => ExistedFromSubordinate(filterModel.GetSearchExpression<T>());
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public async Task<bool> ExistedFromSubordinateAsync(FilterModel filterModel) => await ExistedFromSubordinateAsync(filterModel.GetSearchExpression<T>());
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual int CountFromSubordinate(Expression<Func<T, bool>> expression) => GetSubordinateResult(queryable => queryable.Count(expression));
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<int> CountFromSubordinateAsync(Expression<Func<T, bool>> expression) => await GetSubordinateResultAsync(async queryable => await queryable.CountAsync(expression));
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public int CountFromSubordinate(FilterModel filterModel) => CountFromSubordinate(filterModel.GetSearchExpression<T>());
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public async Task<int> CountFromSubordinateAsync(FilterModel filterModel) => await CountFromSubordinateAsync(filterModel.GetSearchExpression<T>());
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual List<T> FindFromSubordinate(Expression<Func<T, bool>> expression) => FindFromSubordinate(expression, null, SortOrderEnum.Unspecified).ToList();
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public List<T> FindFromSubordinate(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression) => FindFromSubordinate(expression, orderExpression, SortOrderEnum.Ascending);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<T> FindFromSubordinate(Expression<Func<T, bool>> expression, Expression<Func<T, object>>? orderExpression, SortOrderEnum sortOrder)
        {
            orderExpression ??= m => m.ID;
            return GetSubordinateResult(queryable =>
            {
                List<T> result = sortOrder switch
                {
                    SortOrderEnum.Ascending => queryable.OrderBy(orderExpression).Where(expression).ToList(),
                    SortOrderEnum.Descending => queryable.OrderByDescending(orderExpression).Where(expression).ToList(),
                    _ => queryable.Where(expression).ToList(),
                };
                return result;
            });
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindFromSubordinateAsync(Expression<Func<T, bool>> expression) => await FindFromSubordinateAsync(expression, null, SortOrderEnum.Unspecified);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public async Task<List<T>> FindFromSubordinateAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression) => await FindFromSubordinateAsync(expression, orderExpression, SortOrderEnum.Ascending);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public async Task<List<T>> FindFromSubordinateAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>>? orderExpression, SortOrderEnum sortOrder)
        {
            orderExpression ??= m => m.ID;
            return await GetSubordinateResultAsync(async queryable =>
            {
                List<T> result = sortOrder switch
                {
                    SortOrderEnum.Ascending => await queryable.OrderBy(orderExpression).Where(expression).ToListAsync(),
                    SortOrderEnum.Descending => await queryable.OrderByDescending(orderExpression).Where(expression).ToListAsync(),
                    _ => await queryable.Where(expression).ToListAsync(),
                };
                return result;
            });
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public List<T> FindFromSubordinate(FilterModel filterModel) => FindFromSubordinate(filterModel.GetSearchExpression<T>());
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public List<T> FindFromSubordinate(FilterModel filterModel, Expression<Func<T, object>> orderExpression) => FindFromSubordinate(filterModel.GetSearchExpression<T>(), orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<T> FindFromSubordinate(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder) => FindFromSubordinate(filterModel.GetSearchExpression<T>(), orderExpression, sortOrder);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public async Task<List<T>> FindFromSubordinateAsync(FilterModel filterModel) => await FindFromSubordinateAsync(filterModel.GetSearchExpression<T>());
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public async Task<List<T>> FindFromSubordinateAsync(FilterModel filterModel, Expression<Func<T, object>> orderExpression) => await FindFromSubordinateAsync(filterModel.GetSearchExpression<T>(), orderExpression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public async Task<List<T>> FindFromSubordinateAsync(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder) => await FindFromSubordinateAsync(filterModel.GetSearchExpression<T>(), orderExpression, sortOrder);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public T FirstFromSubordinate(FilterModel filterModel) => FirstOrDefaultFromSubordinate(filterModel) ?? throw new TTAException("数据不存在");
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public async Task<T> FirstFromSubordinateAsync(FilterModel filterModel) => await FirstOrDefaultFromSubordinateAsync(filterModel) ?? throw new TTAException("数据不存在");
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public T FirstFromSubordinate(Expression<Func<T, bool>> expression) => FirstOrDefaultFromSubordinate(expression) ?? throw new TTAException("数据不存在");
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public async Task<T> FirstFromSubordinateAsync(Expression<Func<T, bool>> expression) => await FirstOrDefaultFromSubordinateAsync(expression) ?? throw new TTAException("数据不存在");
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T? FirstOrDefaultFromSubordinate(TPrimaryKeyType id) => GetSubordinateResult(queryable => queryable.FirstOrDefault(m => m.ID.Equals(id)));
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T?> FirstOrDefaultFromSubordinateAsync(TPrimaryKeyType id) => await GetSubordinateResultAsync(async queryable => await queryable.FirstOrDefaultAsync(m => m.ID.Equals(id)));
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public T? FirstOrDefaultFromSubordinate(FilterModel filterModel) => FirstOrDefaultFromSubordinate(filterModel.GetSearchExpression<T>());
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public async Task<T?> FirstOrDefaultFromSubordinateAsync(FilterModel filterModel) => await FirstOrDefaultFromSubordinateAsync(filterModel.GetSearchExpression<T>());
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public T? FirstOrDefaultFromSubordinate(Expression<Func<T, bool>> expression) => GetSubordinateResult(queryable => queryable.FirstOrDefault(expression));
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<T?> FirstOrDefaultFromSubordinateAsync(Expression<Func<T, bool>> expression) => await GetSubordinateResultAsync(async queryable => await queryable.FirstOrDefaultAsync(expression));
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public (List<T> data, PageModel pageInfo) PagingFromSubordinate(PageRequestModel pageRequestModel) => PagingFromSubordinate(pageRequestModel.GetSearchExpression<T>(), pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public (List<T> data, PageModel pageInfo) PagingFromSubordinate(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression) => PagingFromSubordinate(pageRequestModel.GetSearchExpression<T>(), orderExpression, pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public (List<T> data, PageModel pageInfo) PagingFromSubordinate(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder) => PagingFromSubordinate(pageRequestModel.GetSearchExpression<T>(), orderExpression, sortOrder, pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel) => PagingFromSubordinate(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, long pageIndex, long pageSize) => PagingFromSubordinate(filterExpression, m => m.ID, pageIndex, pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel) => PagingFromSubordinate(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder, PageRequestModel pageRequestModel) => PagingFromSubordinate(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, long pageIndex, long pageSize) => PagingFromSubordinate(filterExpression, orderExpression, SortOrderEnum.Ascending, pageIndex, pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual (List<T> data, PageModel pageInfo) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder, long pageIndex, long pageSize)
        {
            return GetSubordinateResult(queryable =>
            {
                queryable = queryable.Where(filterExpression);
                PageModel pageModel = new(pageIndex, pageSize, queryable.Count());
                List<T> result = sortOrder switch
                {
                    SortOrderEnum.Ascending => queryable.OrderBy(orderExpression).Skip(pageModel.SkipInt).Take(pageModel.TakeInt).ToList(),
                    SortOrderEnum.Descending => queryable.OrderByDescending(orderExpression).Skip(pageModel.SkipInt).Take(pageModel.TakeInt).ToList(),
                    _ => queryable.Skip(pageModel.SkipInt).Take(pageModel.TakeInt).ToList(),
                };
                return (result, pageModel);
            });
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public async Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(PageRequestModel pageRequestModel) => await PagingFromSubordinateAsync(pageRequestModel.GetSearchExpression<T>(), pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public async Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression) => await PagingFromSubordinateAsync(pageRequestModel.GetSearchExpression<T>(), orderExpression, pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public async Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder) => await PagingFromSubordinateAsync(pageRequestModel.GetSearchExpression<T>(), orderExpression, sortOrder, pageRequestModel);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel) => await PagingFromSubordinateAsync(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, long pageIndex, long pageSize) => await PagingFromSubordinateAsync(filterExpression, m => m.ID, pageIndex, pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel) => await PagingFromSubordinateAsync(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder, PageRequestModel pageRequestModel) => await PagingFromSubordinateAsync(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, long pageIndex, long pageSize) => await PagingFromSubordinateAsync(filterExpression, orderExpression, SortOrderEnum.Ascending, pageIndex, pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, PageModel pageInfo)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder, long pageIndex, long pageSize)
        {
            return await GetSubordinateResultAsync(async queryable =>
            {
                queryable = queryable.Where(filterExpression);
                PageModel pageModel = new(pageIndex, pageSize, await queryable.CountAsync());
                List<T> result = sortOrder switch
                {
                    SortOrderEnum.Ascending => await queryable.OrderBy(orderExpression).Skip(pageModel.SkipInt).Take(pageModel.TakeInt).ToListAsync(),
                    SortOrderEnum.Descending => await queryable.OrderByDescending(orderExpression).Skip(pageModel.SkipInt).Take(pageModel.TakeInt).ToListAsync(),
                    _ => await queryable.Skip(pageModel.SkipInt).Take(pageModel.TakeInt).ToListAsync(),
                };
                return (result, pageModel);
            });
        }
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public (List<T> data, RangeModel rangeInfo) RangeFromSubordinate(RangeRequestModel rangeRequestModel) => RangeFromSubordinate(rangeRequestModel.GetSearchExpression<T>(), rangeRequestModel);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public (List<T> data, RangeModel rangeInfo) RangeFromSubordinate(RangeRequestModel rangeRequestModel, Expression<Func<T, object>> orderExpression) => RangeFromSubordinate(rangeRequestModel.GetSearchExpression<T>(), orderExpression, rangeRequestModel);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public (List<T> data, RangeModel rangeInfo) RangeFromSubordinate(RangeRequestModel rangeRequestModel, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder) => RangeFromSubordinate(rangeRequestModel.GetSearchExpression<T>(), orderExpression, sortOrder, rangeRequestModel);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) RangeFromSubordinate(Expression<Func<T, bool>> filterExpression, RangeRequestModel rangeRequestModel) => RangeFromSubordinate(filterExpression, rangeRequestModel.Skip, rangeRequestModel.Take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) RangeFromSubordinate(Expression<Func<T, bool>> filterExpression, long skip, long take) => RangeFromSubordinate(filterExpression, m => m.ID, skip, take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) RangeFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, RangeRequestModel rangeRequestModel) => RangeFromSubordinate(filterExpression, orderExpression, rangeRequestModel.Skip, rangeRequestModel.Take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) RangeFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel) => RangeFromSubordinate(filterExpression, orderExpression, sortOrder, rangeRequestModel.Skip, rangeRequestModel.Take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) RangeFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, long skip, long take) => RangeFromSubordinate(filterExpression, orderExpression, SortOrderEnum.Ascending, skip, take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual (List<T> data, RangeModel rangeInfo) RangeFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder, long skip, long take)
        {
            return GetSubordinateResult(queryable =>
            {
                queryable = queryable.Where(filterExpression);
                RangeModel rangeModel = new(skip, take, queryable.Count());
                List<T> result = sortOrder switch
                {
                    SortOrderEnum.Ascending => queryable.OrderBy(orderExpression).Skip(rangeModel.SkipInt).Take(rangeModel.TakeInt).ToList(),
                    SortOrderEnum.Descending => queryable.OrderByDescending(orderExpression).Skip(rangeModel.SkipInt).Take(rangeModel.TakeInt).ToList(),
                    _ => queryable.Skip(rangeModel.SkipInt).Take(rangeModel.TakeInt).ToList(),
                };
                return (result, rangeModel);
            });
        }
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public async Task<(List<T> data, RangeModel rangeInfo)> RangeFromSubordinateAsync(RangeRequestModel rangeRequestModel) => await RangeFromSubordinateAsync(rangeRequestModel.GetSearchExpression<T>(), rangeRequestModel);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <returns></returns>
        public async Task<(List<T> data, RangeModel rangeInfo)> RangeFromSubordinateAsync(RangeRequestModel rangeRequestModel, Expression<Func<T, object>> orderExpression) => await RangeFromSubordinateAsync(rangeRequestModel.GetSearchExpression<T>(), orderExpression, rangeRequestModel);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="rangeRequestModel"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public async Task<(List<T> data, RangeModel rangeInfo)> RangeFromSubordinateAsync(RangeRequestModel rangeRequestModel, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder) => await RangeFromSubordinateAsync(rangeRequestModel.GetSearchExpression<T>(), orderExpression, sortOrder, rangeRequestModel);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, RangeRequestModel rangeRequestModel) => await RangeFromSubordinateAsync(filterExpression, rangeRequestModel.Skip, rangeRequestModel.Take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, long skip, long take) => await RangeFromSubordinateAsync(filterExpression, m => m.ID, skip, take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, RangeRequestModel rangeRequestModel) => await RangeFromSubordinateAsync(filterExpression, orderExpression, rangeRequestModel.Skip, rangeRequestModel.Take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="rangeRequestModel"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder, RangeRequestModel rangeRequestModel) => await RangeFromSubordinateAsync(filterExpression, orderExpression, sortOrder, rangeRequestModel.Skip, rangeRequestModel.Take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, long skip, long take) => await RangeFromSubordinateAsync(filterExpression, orderExpression, SortOrderEnum.Ascending, skip, take);
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<(List<T> data, RangeModel rangeInfo)> RangeFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrderEnum sortOrder, long skip, long take)
        {
            return await GetSubordinateResultAsync(async queryable =>
            {
                queryable = queryable.Where(filterExpression);
                RangeModel rangeModel = new(skip, take, await queryable.CountAsync());
                List<T> result = sortOrder switch
                {
                    SortOrderEnum.Ascending => await queryable.OrderBy(orderExpression).Skip(rangeModel.SkipInt).Take(rangeModel.TakeInt).ToListAsync(),
                    SortOrderEnum.Descending => await queryable.OrderByDescending(orderExpression).Skip(rangeModel.SkipInt).Take(rangeModel.TakeInt).ToListAsync(),
                    _ => await queryable.Skip(rangeModel.SkipInt).Take(rangeModel.TakeInt).ToListAsync(),
                };
                return (result, rangeModel);
            });
        }
        #region 私有方法
        /// <summary>
        /// 获得配置
        /// </summary>
        /// <param name="subordinateConfigs"></param>
        /// <returns></returns>
        private static SqlServerSubordinateConfigModel GetConfig(IReadOnlyList<SqlServerSubordinateConfigModel> subordinateConfigs)
        {
            int index = new Random(EFSubordinateRepositoryHelper.ConnectionSeed).Next(0, subordinateConfigs.Count);
            if (EFSubordinateRepositoryHelper.ConnectionSeed == int.MaxValue)
            {
                EFSubordinateRepositoryHelper.ConnectionSeed = int.MinValue;
            }
            else
            {
                EFSubordinateRepositoryHelper.ConnectionSeed++;
            }
            return subordinateConfigs[index];
        }
        /// <summary>
        /// 获得从属库返回值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        private async Task<TResult> GetSubordinateResultAsync<TResult>(Func<IQueryable<T>, Task<TResult>> func)
        {
            if (SubordinateDB == null) throw new MateralException("没有从属数据库");
            IQueryable<T> queryable = GetSubordinateQueryable(SubordinateDB);
            TResult? result = await func(queryable);
            return result;
        }
        /// <summary>
        /// 获得从属库返回值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        private TResult GetSubordinateResult<TResult>(Func<IQueryable<T>, TResult> func)
        {
            if (SubordinateDB == null) throw new MateralException("没有从属数据库");
            IQueryable<T> queryable = GetSubordinateQueryable(SubordinateDB);
            TResult? result = func(queryable);
            return result;
        }
        /// <summary>
        /// 获得从库查询对象
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static DbSet<T> GetSubordinateQueryable(TDBContext context) => context.Set<T>();
        #endregion
    }
}
