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
        /// <returns></returns>
        void Insert(T model);
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
        List<BsonDocument> Find(FilterDefinition<BsonDocument> filterDefinition);
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
        Task<List<BsonDocument>> FindAsync(FilterDefinition<BsonDocument> filterDefinition);
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
    }
}
