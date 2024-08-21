using Materal.Utils.MongoDB.Extensions;
using MongoDB.Driver;

namespace Materal.Utils.MongoDB
{
    public partial class MongoRepository<T>
    {
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(FilterDefinition<T> filter)
        {
            IAsyncCursor<T> collectionResult = await RangeCursorAsync(filter, 0, 1);
            bool result = await collectionResult.AnyAsync();
            return result;
        }
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual bool Any(FilterDefinition<T> filter)
        {
            IAsyncCursor<T> collectionResult = RangeCursor(filter, 0, 1);
            bool result = collectionResult.Any();
            return result;
        }
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            IAsyncCursor<T> collectionResult = await RangeCursorAsync(filter, 0, 1);
            bool result = await collectionResult.AnyAsync();
            return result;
        }
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<T, bool>> filter)
        {
            IAsyncCursor<T> collectionResult = RangeCursor(filter, 0, 1);
            bool result = collectionResult.Any();
            return result;
        }
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(FilterModel filterModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return await AnyAsync(filter);
        }
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public virtual bool Any(FilterModel filterModel)
        {
            FilterDefinition<T> filter = filterModel.GetSearchFilterDefinition<T>();
            return Any(filter);
        }
    }
}
