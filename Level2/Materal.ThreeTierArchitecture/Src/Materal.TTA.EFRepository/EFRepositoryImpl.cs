using Materal.TTA.Common;
using Materal.Utils.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// EF仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class EFRepositoryImpl<TEntity, TPrimaryKeyType, TDBContext> : CommonRepositoryImpl<TEntity, TPrimaryKeyType>, IEFRepository<TEntity, TPrimaryKeyType>
        where TEntity : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected TDBContext DBContext { get; private set; }
        /// <summary>
        /// 实体对象
        /// </summary>
        protected virtual DbSet<TEntity> DBSet => DBContext.Set<TEntity>();
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        protected EFRepositoryImpl(TDBContext dbContext)
        {
            DBContext = dbContext;
        }
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
        public override (List<TEntity> data, PageModel pageInfo) Paging(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, int pageIndex, int pageSize)
        {
            IQueryable<TEntity> queryable = DBSet.Where(filterExpression);
            var pageModel = new PageModel(pageIndex, pageSize, queryable.Count());
            List<TEntity> result = sortOrder switch
            {
                SortOrderEnum.Ascending => queryable.OrderBy(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take).ToList(),
                SortOrderEnum.Descending => queryable.OrderByDescending(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take).ToList(),
                _ => queryable.Skip(pageModel.Skip).Take(pageModel.Take).ToList(),
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
        public override async Task<(List<TEntity> data, PageModel pageInfo)> PagingAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, int pageIndex, int pageSize)
        {
            IQueryable<TEntity> queryable = DBSet.Where(filterExpression);
            var pageModel = new PageModel(pageIndex, pageSize, queryable.Count());
            List<TEntity> result = sortOrder switch
            {
                SortOrderEnum.Ascending => await queryable.OrderBy(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take).ToListAsync(),
                SortOrderEnum.Descending => await queryable.OrderByDescending(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take).ToListAsync(),
                _ => await queryable.Skip(pageModel.Skip).Take(pageModel.Take).ToListAsync(),
            };
            return (result, pageModel);
        }
    }
}