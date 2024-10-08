using System.Data;

namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// EF仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class EFRepositoryImpl<TEntity, TPrimaryKeyType, TDBContext>(TDBContext dbContext) : CommonRepositoryImpl<TEntity, TPrimaryKeyType>, IEFRepository<TEntity, TPrimaryKeyType>
        where TEntity : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected TDBContext DBContext { get; private set; } = dbContext;
        /// <summary>
        /// 实体对象
        /// </summary>
        protected virtual DbSet<TEntity> DBSet => DBContext.Set<TEntity>();
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public override bool Existed(Expression<Func<TEntity, bool>> expression) => DBSet.Any(expression);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public override async Task<bool> ExistedAsync(Expression<Func<TEntity, bool>> expression) => await DBSet.AnyAsync(expression);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public override int Count(Expression<Func<TEntity, bool>> expression) => DBSet.Count(expression);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression) => await DBSet.CountAsync(expression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public override List<TEntity> Find(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder)
        {
            IQueryable<TEntity> queryable = DBSet.Where(expression);
            List<TEntity> result = sortOrder switch
            {
                SortOrderEnum.Ascending => queryable.OrderBy(orderExpression).ToList(),
                SortOrderEnum.Descending => queryable.OrderByDescending(orderExpression).ToList(),
                _ => queryable.ToList(),
            };
            return result;
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public override async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder)
        {
            IQueryable<TEntity> queryable = DBSet.Where(expression);
            List<TEntity> result = sortOrder switch
            {
                SortOrderEnum.Ascending => await queryable.OrderBy(orderExpression).ToListAsync(),
                SortOrderEnum.Descending => await queryable.OrderByDescending(orderExpression).ToListAsync(),
                _ => await queryable.ToListAsync(),
            };
            return result;
        }
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public override TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression) => DBSet.FirstOrDefault(expression);
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public override async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression) => await DBSet.FirstOrDefaultAsync(expression);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public override (List<TEntity> data, PageModel pageInfo) Paging(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, long pageIndex, long pageSize)
        {
            IQueryable<TEntity> queryable = DBSet.Where(filterExpression);
            PageModel pageModel = new(pageIndex, pageSize, queryable.Count())
            {
                SortPropertyName = GetSortPropertyName(orderExpression),
                IsAsc = sortOrder == SortOrderEnum.Ascending
            };
            List<TEntity> result = sortOrder switch
            {
                SortOrderEnum.Ascending => queryable.OrderBy(orderExpression).Skip(pageModel.PageSkipInt).Take(pageModel.TakeInt).ToList(),
                SortOrderEnum.Descending => queryable.OrderByDescending(orderExpression).Skip(pageModel.PageSkipInt).Take(pageModel.TakeInt).ToList(),
                _ => queryable.Skip(pageModel.PageSkipInt).Take(pageModel.TakeInt).ToList(),
            };
            return (result, pageModel);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public override async Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, long pageIndex, long pageSize)
        {
            IQueryable<TEntity> queryable = DBSet.Where(filterExpression);
            PageModel pageModel = new(pageIndex, pageSize, await queryable.CountAsync())
            {
                SortPropertyName = GetSortPropertyName(orderExpression),
                IsAsc = sortOrder == SortOrderEnum.Ascending
            };
            List<TEntity> result = sortOrder switch
            {
                SortOrderEnum.Ascending => await queryable.OrderBy(orderExpression).Skip(pageModel.PageSkipInt).Take(pageModel.TakeInt).ToListAsync(),
                SortOrderEnum.Descending => await queryable.OrderByDescending(orderExpression).Skip(pageModel.PageSkipInt).Take(pageModel.TakeInt).ToListAsync(),
                _ => await queryable.Skip(pageModel.PageSkipInt).Take(pageModel.TakeInt).ToListAsync(),
            };
            return (result, pageModel);
        }
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public override (List<TEntity> data, RangeModel rangeInfo) Range(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, long skip, long take)
        {
            IQueryable<TEntity> queryable = DBSet.Where(filterExpression);
            RangeModel rangeModel = new(skip, take, queryable.Count())
            {
                SortPropertyName = GetSortPropertyName(orderExpression),
                IsAsc = sortOrder == SortOrderEnum.Ascending
            };
            List<TEntity> result = sortOrder switch
            {
                SortOrderEnum.Ascending => queryable.OrderBy(orderExpression).Skip(rangeModel.SkipInt).Take(rangeModel.TakeInt).ToList(),
                SortOrderEnum.Descending => queryable.OrderByDescending(orderExpression).Skip(rangeModel.SkipInt).Take(rangeModel.TakeInt).ToList(),
                _ => queryable.Skip(rangeModel.SkipInt).Take(rangeModel.TakeInt).ToList(),
            };
            return (result, rangeModel);
        }
        /// <summary>
        /// 范围查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public override async Task<(List<TEntity> data, RangeModel rangeInfo)> RangeAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, long skip, long take)
        {
            IQueryable<TEntity> queryable = DBSet.Where(filterExpression);
            RangeModel rangeModel = new(skip, take, await queryable.CountAsync())
            {
                SortPropertyName = GetSortPropertyName(orderExpression),
                IsAsc = sortOrder == SortOrderEnum.Ascending
            };
            List<TEntity> result = sortOrder switch
            {
                SortOrderEnum.Ascending => await queryable.OrderBy(orderExpression).Skip(rangeModel.SkipInt).Take(rangeModel.TakeInt).ToListAsync(),
                SortOrderEnum.Descending => await queryable.OrderByDescending(orderExpression).Skip(rangeModel.SkipInt).Take(rangeModel.TakeInt).ToListAsync(),
                _ => await queryable.Skip(rangeModel.SkipInt).Take(rangeModel.TakeInt).ToListAsync(),
            };
            return (result, rangeModel);
        }
        /// <inheritdoc/>
        protected override string GetConnectionString()
        {
            string? connectionString = DBContext.Database.GetConnectionString();
            if (connectionString is null || string.IsNullOrWhiteSpace(connectionString)) throw new TTAException("获取连接字符串失败");
            return connectionString;
        }
    }
}