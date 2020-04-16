using Materal.Model;
using Materal.TTA.Common;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Materal.TTA.MongoDBRepository
{
    /// <summary>
    /// MongoDB仓储
    /// </summary>
    public interface IMongoDBRepository<T,in TPrimaryKeyType> : IRepository<T, TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>, new()
    {
        /// <summary>
        /// 选择集合
        /// </summary>
        /// <param name="collectionNameString"></param>
        void SelectCollection(string collectionNameString);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        long CountLong(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        Task<long> CountLongAsync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task InsertAsync(T model);
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model"></param>
        void Insert(T model);
        /// <summary>
        /// 插入一组数据
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Task InsertManyAsync(List<T> models);
        /// <summary>
        /// 插入一组数据
        /// </summary>
        /// <param name="models"></param>
        void InsertMany(List<T> models);
        /// <summary>
        /// 删除指定的数据
        /// </summary>
        /// <param name="model"></param>
        void Delete(T model);
        /// <summary>
        /// 删除指定的数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task DeleteAsync(T model);
        /// <summary>
        /// 删除指定的数据
        /// </summary>
        /// <param name="id"></param>
        void Delete(TPrimaryKeyType id);
        /// <summary>
        /// 删除指定的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(TPrimaryKeyType id);
        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<long> DeleteAsync(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        long Delete(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<long> DeleteManyAsync(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        long DeleteMany(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 删除一组数据
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<long> DeleteManyAsync(List<T> models);
        /// <summary>
        /// 删除一组数据
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        long DeleteMany(List<T> models);
        /// <summary>
        /// 异步保存一组数据，没有则创建，有则更新
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Task SaveManyAsync(List<T> models);
        /// <summary>
        /// 保存一组数据，没有则创建，有则更新
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        void SaveMany(List<T> models);
        /// <summary>
        /// 异步保存，没有则创建，有则更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task SaveAsync(T model);
        /// <summary>
        /// 保存，没有则创建，有则更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Save(T model);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filterDefinition"></param>
        /// <returns></returns>
        List<T> Find(FilterDefinition<T> filterDefinition);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filterDefinition"></param>
        /// <returns></returns>
        IFindFluent<BsonDocument, BsonDocument> Find(FilterDefinition<BsonDocument> filterDefinition);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filterDefinition"></param>
        /// <returns></returns>
        Task<List<T>> FindAsync(FilterDefinition<T> filterDefinition);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filterDefinition"></param>
        /// <returns></returns>
        Task<IAsyncCursor<BsonDocument>> FindAsync(FilterDefinition<BsonDocument> filterDefinition);
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task InsertManyAsync(IEnumerable<T> model);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="model"></param>
        void InsertMany(IEnumerable<T> model);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filterDefinition"></param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> FindDocumentAsync(FilterDefinition<T> filterDefinition);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterDefinition"></param>
        /// <returns></returns>
        IFindFluent<T, T> FindDocument(FilterDefinition<T> filterDefinition);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        IFindFluent<T, T> FindDocument(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> FindDocumentAsync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        IFindFluent<T, T> FindDocument(FilterModel filterModel);
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filterModel">过滤器模型</param>
        /// <returns></returns>
        Task<IAsyncCursor<T>> FindDocumentAsync(FilterModel filterModel);


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterDefinition">分页请求模型</param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        (List<T> result, PageModel pageModel) Paging(FilterDefinition<T> filterDefinition, PageRequestModel pageRequestModel);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterDefinition">分页请求模型</param>
        /// <param name="sortDefinition">排序表达式</param>
        /// <param name="pageRequestModel"></param>
        /// <returns></returns>
        (List<T> result, PageModel pageModel) Paging(FilterDefinition<T> filterDefinition, SortDefinition<T> sortDefinition, PageRequestModel pageRequestModel);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterDefinition">分页请求模型</param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        (List<T> result, PageModel pageModel) Paging(FilterDefinition<T> filterDefinition, int pageIndex, int pageSize, int skip, int take);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="filterDefinition">分页请求模型</param>
        /// <param name="sortDefinition">排序表达式</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        (List<T> result, PageModel pageModel) Paging(FilterDefinition<T> filterDefinition, SortDefinition<T> sortDefinition, int pageIndex, int pageSize, int skip, int take);
    }
}
