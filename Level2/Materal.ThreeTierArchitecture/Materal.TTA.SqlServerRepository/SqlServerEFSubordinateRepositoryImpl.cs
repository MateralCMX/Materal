﻿using Materal.Abstractions;
using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Materal.TTA.SqlServerRepository.Model;
using Materal.Utils.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Materal.TTA.SqlServerRepository
{
    internal class EFSubordinateRepositoryHelper
    {
        public static int ConnectionSeed { get; set; }
    }

    public abstract class SqlServerEFSubordinateRepositoryImpl<T, TPrimaryKeyType, TDBContext> : SqlServerEFRepositoryImpl<T, TPrimaryKeyType, TDBContext>, IEFSubordinateRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>
        where TDBContext : DbContext
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 从属数据库
        /// </summary>
        protected readonly TDBContext SubordinateDB;
        protected SqlServerEFSubordinateRepositoryImpl(TDBContext dbContext, IEnumerable<SqlServerSubordinateConfigModel> subordinateConfigs, Action<DbContextOptionsBuilder, string> optionAction) : base(dbContext)
        {
            Type type = typeof(TDBContext);
            SqlServerSubordinateConfigModel config = SqlServerEFSubordinateRepositoryImpl<T, TPrimaryKeyType, TDBContext>.GetConfig(subordinateConfigs.ToList());
            var contextOptions = new DbContextOptions<TDBContext>();
            var optionsBuilder = new DbContextOptionsBuilder(contextOptions);
            optionAction(optionsBuilder, config.ConnectionString);
            var arg = (DbContextOptions<TDBContext>)optionsBuilder.Options;
            SubordinateDB = (TDBContext)type.Instantiation(new object[] { arg });
        }
        public virtual bool ExistedFromSubordinate(TPrimaryKeyType id) => GetSubordinateResult(queryable => queryable.Any(m => m.ID.Equals(id)));
        public virtual async Task<bool> ExistedFromSubordinateAsync(TPrimaryKeyType id) => await GetSubordinateResultAsync(async queryable => await queryable.AnyAsync(m => m.ID.Equals(id)));
        public bool ExistedFromSubordinate(Expression<Func<T, bool>> expression) => GetSubordinateResult(queryable => queryable.Any(expression));
        public async Task<bool> ExistedFromSubordinateAsync(Expression<Func<T, bool>> expression) => await GetSubordinateResultAsync(async queryable => await queryable.AnyAsync(expression));
        public bool ExistedFromSubordinate(FilterModel filterModel) => ExistedFromSubordinate(filterModel.GetSearchExpression<T>());
        public async Task<bool> ExistedFromSubordinateAsync(FilterModel filterModel) => await ExistedFromSubordinateAsync(filterModel.GetSearchExpression<T>());
        public virtual int CountFromSubordinate(Expression<Func<T, bool>> expression) => GetSubordinateResult(queryable => queryable.Count(expression));
        public virtual async Task<int> CountFromSubordinateAsync(Expression<Func<T, bool>> expression) => await GetSubordinateResultAsync(async queryable => await queryable.CountAsync(expression));
        public int CountFromSubordinate(FilterModel filterModel) => CountFromSubordinate(filterModel.GetSearchExpression<T>());
        public async Task<int> CountFromSubordinateAsync(FilterModel filterModel) => await CountFromSubordinateAsync(filterModel.GetSearchExpression<T>());
        public virtual List<T> FindFromSubordinate(Expression<Func<T, bool>> expression) => FindFromSubordinate(expression, null, SortOrder.Unspecified).ToList();
        public List<T> FindFromSubordinate(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression) => FindFromSubordinate(expression, orderExpression, SortOrder.Ascending);
        public List<T> FindFromSubordinate(Expression<Func<T, bool>> expression, Expression<Func<T, object>>? orderExpression, SortOrder sortOrder)
        {
            orderExpression ??= m => m.ID;
            return GetSubordinateResult(queryable =>
            {
                List<T> result = sortOrder switch
                {
                    SortOrder.Ascending => queryable.OrderBy(orderExpression).Where(expression).ToList(),
                    SortOrder.Descending => queryable.OrderByDescending(orderExpression).Where(expression).ToList(),
                    _ => queryable.Where(expression).ToList(),
                };
                return result;
            });
        }
        public virtual async Task<List<T>> FindFromSubordinateAsync(Expression<Func<T, bool>> expression) => await FindFromSubordinateAsync(expression, null, SortOrder.Unspecified);
        public async Task<List<T>> FindFromSubordinateAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression) => await FindFromSubordinateAsync(expression, orderExpression, SortOrder.Ascending);
        public async Task<List<T>> FindFromSubordinateAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>>? orderExpression, SortOrder sortOrder)
        {
            orderExpression ??= m => m.ID;
            return await GetSubordinateResultAsync(async queryable =>
            {
                List<T> result = sortOrder switch
                {
                    SortOrder.Ascending => await queryable.OrderBy(orderExpression).Where(expression).ToListAsync(),
                    SortOrder.Descending => await queryable.OrderByDescending(orderExpression).Where(expression).ToListAsync(),
                    _ => await queryable.Where(expression).ToListAsync(),
                };
                return result;
            });
        }
        public List<T> FindFromSubordinate(FilterModel filterModel) => FindFromSubordinate(filterModel.GetSearchExpression<T>());
        public List<T> FindFromSubordinate(FilterModel filterModel, Expression<Func<T, object>> orderExpression) => FindFromSubordinate(filterModel.GetSearchExpression<T>(), orderExpression);
        public List<T> FindFromSubordinate(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder) => FindFromSubordinate(filterModel.GetSearchExpression<T>(), orderExpression, sortOrder);
        public async Task<List<T>> FindFromSubordinateAsync(FilterModel filterModel) => await FindFromSubordinateAsync(filterModel.GetSearchExpression<T>());
        public async Task<List<T>> FindFromSubordinateAsync(FilterModel filterModel, Expression<Func<T, object>> orderExpression) => await FindFromSubordinateAsync(filterModel.GetSearchExpression<T>(), orderExpression);
        public async Task<List<T>> FindFromSubordinateAsync(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder) => await FindFromSubordinateAsync(filterModel.GetSearchExpression<T>(), orderExpression, sortOrder);
        public virtual T? FirstOrDefaultFromSubordinate(TPrimaryKeyType id) => GetSubordinateResult(queryable => queryable.FirstOrDefault(m => m.ID.Equals(id)));
        public virtual async Task<T?> FirstOrDefaultFromSubordinateAsync(TPrimaryKeyType id) => await GetSubordinateResultAsync(async queryable => await queryable.FirstOrDefaultAsync(m => m.ID.Equals(id)));
        public T? FirstOrDefaultFromSubordinate(FilterModel filterModel) => FirstOrDefaultFromSubordinate(filterModel.GetSearchExpression<T>());
        public async Task<T?> FirstOrDefaultFromSubordinateAsync(FilterModel filterModel) => await FirstOrDefaultFromSubordinateAsync(filterModel.GetSearchExpression<T>());
        public T? FirstOrDefaultFromSubordinate(Expression<Func<T, bool>> expression) => GetSubordinateResult(queryable => queryable.FirstOrDefault(expression));
        public async Task<T?> FirstOrDefaultFromSubordinateAsync(Expression<Func<T, bool>> expression) => await GetSubordinateResultAsync(async queryable => await queryable.FirstOrDefaultAsync(expression));
        public (List<T> result, PageModel pageModel) PagingFromSubordinate(PageRequestModel pageRequestModel) => PagingFromSubordinate(pageRequestModel.GetSearchExpression<T>(), pageRequestModel);
        public (List<T> result, PageModel pageModel) PagingFromSubordinate(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression) => PagingFromSubordinate(pageRequestModel.GetSearchExpression<T>(), orderExpression, pageRequestModel);
        public (List<T> result, PageModel pageModel) PagingFromSubordinate(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder) => PagingFromSubordinate(pageRequestModel.GetSearchExpression<T>(), orderExpression, sortOrder, pageRequestModel);
        public virtual (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel) => PagingFromSubordinate(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        public virtual (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize) => PagingFromSubordinate(filterExpression, m => m.ID, pagingIndex, pagingSize);
        public virtual (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel) => PagingFromSubordinate(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        public virtual (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel) => PagingFromSubordinate(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        public virtual (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize) => PagingFromSubordinate(filterExpression, orderExpression, SortOrder.Ascending, pagingIndex, pagingSize);
        public virtual (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
        {
            return GetSubordinateResult(queryable =>
            {
                queryable = queryable.Where(filterExpression);
                var pageModel = new PageModel(pagingIndex, pagingSize, queryable.Count());
                List<T> result = sortOrder switch
                {
                    SortOrder.Ascending => queryable.OrderBy(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take).ToList(),
                    SortOrder.Descending => queryable.OrderByDescending(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take).ToList(),
                    _ => queryable.Skip(pageModel.Skip).Take(pageModel.Take).ToList(),
                };
                return (result, pageModel);
            });
        }
        public async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(PageRequestModel pageRequestModel) => await PagingFromSubordinateAsync(pageRequestModel.GetSearchExpression<T>(), pageRequestModel);
        public async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression) => await PagingFromSubordinateAsync(pageRequestModel.GetSearchExpression<T>(), orderExpression, pageRequestModel);
        public async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder) => await PagingFromSubordinateAsync(pageRequestModel.GetSearchExpression<T>(), orderExpression, sortOrder, pageRequestModel);
        public virtual async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel) => await PagingFromSubordinateAsync(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        public virtual async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize) => await PagingFromSubordinateAsync(filterExpression, m => m.ID, pagingIndex, pagingSize);
        public virtual async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel) => await PagingFromSubordinateAsync(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        public virtual async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel) => await PagingFromSubordinateAsync(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        public virtual async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize) => await PagingFromSubordinateAsync(filterExpression, orderExpression, SortOrder.Ascending, pagingIndex, pagingSize);
        public virtual async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
        {
            return await GetSubordinateResultAsync(async queryable =>
            {
                queryable = queryable.Where(filterExpression);
                var pageModel = new PageModel(pagingIndex, pagingSize, await queryable.CountAsync());
                List<T> result = sortOrder switch
                {
                    SortOrder.Ascending => await queryable.OrderBy(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take)
                                                .ToListAsync(),
                    SortOrder.Descending => await queryable.OrderByDescending(orderExpression).Skip(pageModel.Skip)
                                                .Take(pageModel.Take).ToListAsync(),
                    _ => await queryable.Skip(pageModel.Skip).Take(pageModel.Take).ToListAsync(),
                };
                return (result, pageModel);
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
            var result = await func(queryable);
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
            var result = func(queryable);
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