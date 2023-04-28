namespace Materal.TTA.ADONETRepository
{
    /// <summary>
    /// 迁移仓储
    /// </summary>
    public interface IMaigrateRepository
    {
        /// <summary>
        /// 获得已存在的迁移数据
        /// </summary>
        /// <param name="dbOption"></param>
        /// <returns></returns>
        public List<string> GetExistingData(DBOption dbOption);
    }
}
