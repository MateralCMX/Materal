using Materal.TTA.Common;
using System.Data;

namespace Materal.TTA.ADONETRepository
{
    /// <summary>
    /// TTAADONETException
    /// </summary>
    public class TTAADONETException : TTAException
    {
        /// <summary>
        /// 出错的TSQL语句
        /// </summary>
        public string? TSQL { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbCommand"></param>
        public TTAADONETException(IDbCommand dbCommand)
        {
            TSQL = dbCommand.CommandText;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="dbCommand"></param>
        public TTAADONETException(string message, IDbCommand dbCommand) : base(message)
        {
            TSQL = dbCommand.CommandText;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="dbCommand"></param>
        public TTAADONETException(string message, Exception innerException, IDbCommand dbCommand) : base(message, innerException)
        {
            TSQL = dbCommand.CommandText;
        }
    }
}
