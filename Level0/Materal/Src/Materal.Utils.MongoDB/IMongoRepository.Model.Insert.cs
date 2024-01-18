namespace Materal.Utils.MongoDB
{
    public partial interface IMongoRepository<T>
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task InsertAsync(T data);
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Insert(T data);
        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task InsertAsync(IEnumerable<T> data);
        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void Insert(IEnumerable<T> data);
    }
}
