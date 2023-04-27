using System.Data;

namespace Materal.TTA.ADONETRepository.Extensions
{
    /// <summary>
    /// 数据库命令扩展
    /// </summary>
    public static class IDbCommandExtension
    {
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void AddParameter(this IDbCommand command, string name, object? value)
        {
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;
            command.Parameters.Add(parameter);
        }
    }
}
