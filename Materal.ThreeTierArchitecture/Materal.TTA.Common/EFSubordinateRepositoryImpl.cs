using Materal.Common;
using Materal.TTA.Common.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Materal.Model;

// ReSharper disable All

namespace Materal.TTA.Common
{
    public abstract class EFSubordinateRepositoryImpl<T, TPrimaryKeyType, TContext> : EFRepositoryImpl<T, TPrimaryKeyType>, IEFSubordinateRepository<T, TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>
    where TContext : DbContext
    {
        /// <summary>
        /// 从属数据库
        /// </summary>
        protected readonly List<TContext> SubordinateDB = new List<TContext>();
        protected EFSubordinateRepositoryImpl(TContext dbContext, IEnumerable<SqlServerSubordinateConfigModel> subordinateConfigs, Action<DbContextOptionsBuilder, string> optionAction) : base(dbContext)
        {
            Type type = typeof(TContext);
            ConstructorInfo[] constructorInfos = type.GetConstructors();
            ConstructorInfo constructorInfo = constructorInfos.FirstOrDefault(m => m.GetParameters().Length == 1);
            if (constructorInfo == null) throw new MateralException("不可用构造函数");
            foreach (SqlServerSubordinateConfigModel config in subordinateConfigs)
            {
                var contextOptions = new DbContextOptions<TContext>();
                var optionsBuilder = new DbContextOptionsBuilder(contextOptions);
                optionAction(optionsBuilder, config.ConnectionString);
                var arg = (DbContextOptions<TContext>)optionsBuilder.Options;
                var newDbContext = (TContext)constructorInfo.Invoke(new object[] { arg });
                SubordinateDB.Add(newDbContext);
            }
        }
        public virtual bool ExistedFromSubordinate(TPrimaryKeyType id)
        {
            return GetSubordinateResult(async queryable =>
            {
                return await queryable.AnyAsync(m => m.ID.Equals(id));
            });
        }

        public virtual async Task<bool> ExistedFromSubordinateAsync(TPrimaryKeyType id)
        {
            return await Task.Run(() =>
            {
                return GetSubordinateResult(async queryable =>
                {
                    return await queryable.AnyAsync(m => m.ID.Equals(id));
                });
            });
        }

        public bool ExistedFromSubordinate(Expression<Func<T, bool>> expression)
        {
            return GetSubordinateResult(async queryable =>
            {
                return await queryable.AnyAsync(expression);
            });
        }

        public async Task<bool> ExistedFromSubordinateAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() =>
            {
                return GetSubordinateResult(async queryable =>
                {
                    return await queryable.AnyAsync(expression);
                });
            });
        }

        public bool ExistedFromSubordinate(FilterModel filterModel)
        {
            return ExistedFromSubordinate(filterModel.GetSearchExpression<T>());
        }

        public async Task<bool> ExistedFromSubordinateAsync(FilterModel filterModel)
        {
            return await ExistedFromSubordinateAsync(filterModel.GetSearchExpression<T>());
        }

        public virtual int CountFromSubordinate(Expression<Func<T, bool>> expression)
        {
            return GetSubordinateResult(async queryable =>
            {
                return await queryable.CountAsync(expression);
            });
        }

        public virtual async Task<int> CountFromSubordinateAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() =>
            {
                return GetSubordinateResult(async queryable =>
                {
                    return await queryable.CountAsync(expression);
                });
            });
        }

        public int CountFromSubordinate(FilterModel filterModel)
        {
            return CountFromSubordinate(filterModel.GetSearchExpression<T>());
        }

        public async Task<int> CountFromSubordinateAsync(FilterModel filterModel)
        {
            return await CountFromSubordinateAsync(filterModel.GetSearchExpression<T>());
        }

        public virtual List<T> FindFromSubordinate(Expression<Func<T, bool>> expression)
        {
            return FindFromSubordinate(expression, null, SortOrder.Unspecified).ToList();
        }

        public List<T> FindFromSubordinate(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression)
        {
            return FindFromSubordinate(expression, orderExpression, SortOrder.Ascending);
        }

        public List<T> FindFromSubordinate(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder)
        {
            return GetSubordinateResult(async queryable =>
            {
                List<T> result;
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        result = await queryable.OrderBy(orderExpression).Where(expression).ToListAsync();
                        break;
                    case SortOrder.Descending:
                        result = await queryable.OrderByDescending(orderExpression).Where(expression).ToListAsync();
                        break;
                    default:
                        result = await queryable.ToListAsync();
                        break;
                }
                return result;
            });
        }
        public virtual async Task<List<T>> FindFromSubordinateAsync(Expression<Func<T, bool>> expression)
        {
            return await FindFromSubordinateAsync(expression, null, SortOrder.Unspecified);
        }
        public async Task<List<T>> FindFromSubordinateAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression)
        {
            return await FindFromSubordinateAsync(expression, orderExpression, SortOrder.Ascending);
        }
        public async Task<List<T>> FindFromSubordinateAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder)
        {
            return await Task.Run(() =>
            {
                return GetSubordinateResult(async queryable =>
                {
                    List<T> result;
                    switch (sortOrder)
                    {
                        case SortOrder.Ascending:
                            result = await queryable.OrderBy(orderExpression).Where(expression).ToListAsync();
                            break;
                        case SortOrder.Descending:
                            result = await queryable.OrderByDescending(orderExpression).Where(expression).ToListAsync();
                            break;
                        default:
                            result = await queryable.ToListAsync();
                            break;
                    }
                    return result;
                });
            });
        }

        public List<T> FindFromSubordinate(FilterModel filterModel)
        {
            return FindFromSubordinate(filterModel.GetSearchExpression<T>());
        }

        public List<T> FindFromSubordinate(FilterModel filterModel, Expression<Func<T, object>> orderExpression)
        {
            return FindFromSubordinate(filterModel.GetSearchExpression<T>(), orderExpression);
        }

        public List<T> FindFromSubordinate(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder)
        {
            return FindFromSubordinate(filterModel.GetSearchExpression<T>(), orderExpression, sortOrder);
        }

        public async Task<List<T>> FindFromSubordinateAsync(FilterModel filterModel)
        {
            return await FindFromSubordinateAsync(filterModel.GetSearchExpression<T>());
        }

        public async Task<List<T>> FindFromSubordinateAsync(FilterModel filterModel, Expression<Func<T, object>> orderExpression)
        {
            return await FindFromSubordinateAsync(filterModel.GetSearchExpression<T>(), orderExpression);
        }

        public async Task<List<T>> FindFromSubordinateAsync(FilterModel filterModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder)
        {
            return await FindFromSubordinateAsync(filterModel.GetSearchExpression<T>(), orderExpression, sortOrder);
        }

        public virtual T FirstOrDefaultFromSubordinate(TPrimaryKeyType id)
        {
            return GetSubordinateResult(async queryable =>
            {
                return await queryable.FirstOrDefaultAsync(m => m.ID.Equals(id));
            });
        }

        public virtual async Task<T> FirstOrDefaultFromSubordinateAsync(TPrimaryKeyType id)
        {
            return await Task.Run(() =>
            {
                return GetSubordinateResult(async queryable =>
                {
                    return await queryable.FirstOrDefaultAsync(m => m.ID.Equals(id));
                });
            });
        }

        public T FirstOrDefaultFromSubordinate(FilterModel filterModel)
        {
            return FirstOrDefaultFromSubordinate(filterModel.GetSearchExpression<T>());
        }

        public async Task<T> FirstOrDefaultFromSubordinateAsync(FilterModel filterModel)
        {
            return await FirstOrDefaultFromSubordinateAsync(filterModel.GetSearchExpression<T>());
        }

        public T FirstOrDefaultFromSubordinate(Expression<Func<T, bool>> expression)
        {
            return GetSubordinateResult(async queryable =>
            {
                return await queryable.FirstOrDefaultAsync(expression);
            });
        }

        public async Task<T> FirstOrDefaultFromSubordinateAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() =>
            {
                return GetSubordinateResult(async queryable =>
                {
                    return await queryable.FirstOrDefaultAsync(expression);
                });
            });
        }

        public (List<T> result, PageModel pageModel) PagingFromSubordinate(PageRequestModel pageRequestModel)
        {
            return PagingFromSubordinate(pageRequestModel.GetSearchExpression<T>(), pageRequestModel);
        }

        public (List<T> result, PageModel pageModel) PagingFromSubordinate(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression)
        {
            return PagingFromSubordinate(pageRequestModel.GetSearchExpression<T>(), orderExpression, pageRequestModel);
        }

        public (List<T> result, PageModel pageModel) PagingFromSubordinate(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder)
        {
            return PagingFromSubordinate(pageRequestModel.GetSearchExpression<T>(), orderExpression, sortOrder, pageRequestModel);
        }

        public virtual (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            return PagingFromSubordinate(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize)
        {
            return PagingFromSubordinate(filterExpression, m => m.ID, pagingIndex, pagingSize);
        }

        public virtual (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel)
        {
            return PagingFromSubordinate(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel)
        {
            return PagingFromSubordinate(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize)
        {
            return PagingFromSubordinate(filterExpression, orderExpression, SortOrder.Ascending, pagingIndex, pagingSize);
        }

        public virtual (List<T> result, PageModel pageModel) PagingFromSubordinate(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
        {
            return GetSubordinateResult(async queryable =>
            {
                List<T> result;
                queryable = queryable.Where(filterExpression);
                var pageModel = new PageModel(pagingIndex, pagingSize, await queryable.CountAsync());
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        result = await queryable.OrderBy(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take).ToListAsync();
                        break;
                    case SortOrder.Descending:
                        result = await queryable.OrderByDescending(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take).ToListAsync();
                        break;
                    default:
                        result = await queryable.Skip(pageModel.Skip).Take(pageModel.Take).ToListAsync();
                        break;
                }
                return (result, pageModel);
            });
        }

        public async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(PageRequestModel pageRequestModel)
        {
            return await PagingFromSubordinateAsync(pageRequestModel.GetSearchExpression<T>(), pageRequestModel);
        }

        public async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression)
        {
            return await PagingFromSubordinateAsync(pageRequestModel.GetSearchExpression<T>(), orderExpression, pageRequestModel);
        }

        public async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(PageRequestModel pageRequestModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder)
        {
            return await PagingFromSubordinateAsync(pageRequestModel.GetSearchExpression<T>(), orderExpression, sortOrder, pageRequestModel);
        }

        public virtual async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, PageRequestModel pageRequestModel)
        {
            return await PagingFromSubordinateAsync(filterExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, int pagingIndex, int pagingSize)
        {
            return await PagingFromSubordinateAsync(filterExpression, m => m.ID, pagingIndex, pagingSize);
        }

        public virtual async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, PageRequestModel pageRequestModel)
        {
            return await PagingFromSubordinateAsync(filterExpression, orderExpression, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, PageRequestModel pageRequestModel)
        {
            return await PagingFromSubordinateAsync(filterExpression, orderExpression, sortOrder, pageRequestModel.PageIndex, pageRequestModel.PageSize);
        }

        public virtual async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, int pagingIndex, int pagingSize)
        {
            return await PagingFromSubordinateAsync(filterExpression, orderExpression, SortOrder.Ascending, pagingIndex, pagingSize);
        }

        public virtual async Task<(List<T> result, PageModel pageModel)> PagingFromSubordinateAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pagingIndex, int pagingSize)
        {
            return await Task.Run(() =>
            {
                return GetSubordinateResult(async queryable =>
                {
                    List<T> result;
                    queryable = queryable.Where(filterExpression);
                    var pageModel = new PageModel(pagingIndex, pagingSize, await queryable.CountAsync());
                    switch (sortOrder)
                    {
                        case SortOrder.Ascending:
                            result = await queryable.OrderBy(orderExpression).Skip(pageModel.Skip).Take(pageModel.Take)
                                .ToListAsync();
                            break;
                        case SortOrder.Descending:
                            result = await queryable.OrderByDescending(orderExpression).Skip(pageModel.Skip)
                                .Take(pageModel.Take).ToListAsync();
                            break;
                        default:
                            result = await queryable.Skip(pageModel.Skip).Take(pageModel.Take).ToListAsync();
                            break;
                    }
                    return (result, pageModel);
                });
            });
        }
        #region 私有方法
        /// <summary>
        /// 获得从属库返回值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        private TResult GetSubordinateResult<TResult>(Func<IQueryable<T>, Task<TResult>> func)
        {
            if (SubordinateDB == null || SubordinateDB.Count == 0) throw new MateralException("没有从属数据库");
            ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();
            TResult result = default;
            var task = Task.Run(() =>
            {
                Parallel.ForEach(SubordinateDB, context =>
                {
                    try
                    {
                        IQueryable<T> queryable = GetSubordinateQueryable(context);
                        result = func(queryable).Result;
                        return;
                    }
                    catch (Exception ex)
                    {
                        exceptions.Enqueue(ex);
                    }
                });
            });
            Task.WaitAll(task);
            if (exceptions.Count == SubordinateDB.Count)
            {
                result = func(DBQueryable).Result;
            }
            return result;
        }
        /// <summary>
        /// 获得从库查询对象
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private IQueryable<T> GetSubordinateQueryable(TContext context)
        {
            return IsView ? (IQueryable<T>)context.Query<T>() : context.Set<T>();
        }
        #endregion
    }
}
