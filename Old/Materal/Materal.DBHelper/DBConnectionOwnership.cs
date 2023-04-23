namespace Materal.DBHelper
{
    /// <summary>   
    /// 标识数据库连接是由DBHelper提供还是由调用者提供   
    /// </summary>   
    public enum DBConnectionOwnership
    {
        /// <summary>
        /// DBHelper提供
        /// </summary>
        Internal,
        /// <summary>
        /// 调用者提供
        /// </summary>
        External
    }
}
