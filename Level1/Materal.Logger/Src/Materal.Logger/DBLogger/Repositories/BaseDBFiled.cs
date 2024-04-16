using System.ComponentModel.DataAnnotations;

namespace Materal.Logger.DBLogger.Repositories
{
    /// <summary>
    /// 数据库字段
    /// </summary>
    public abstract class BaseDBFiled : IDBFiled
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 数据库类型
        /// </summary>
        [Required(ErrorMessage = "数据库类型不能为空")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// C#类型
        /// </summary>
        public abstract Type CSharpType { get; }
        /// <summary>
        /// 值
        /// </summary>
        public string? Value { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        public bool PK { get; set; } = false;
        /// <summary>
        /// 索引
        /// </summary>
        public string? Index { get; set; }
        /// <summary>
        /// 可以为空
        /// </summary>
        public bool IsNull { get; set; } = true;
        /// <summary>
        /// 获得创建表字段SQL
        /// </summary>
        /// <returns></returns>
        public abstract string GetCreateTableFiledSQL();
        /// <summary>
        /// 获得新的数据库字段
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ruleOptions"></param>
        /// <param name="targetOptions"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="LoggerException"></exception>
        public virtual IDBFiled GetNewDBFiled(Log log, LoggerRuleOptions ruleOptions, LoggerTargetOptions targetOptions, LoggerOptions options)
        {
            object resultObj = GetType().Instantiation();
            if (resultObj is not IDBFiled result) throw new LoggerException("实例化数据库字段失败");
            this.CopyProperties(result);
            if (Value is not null && !string.IsNullOrWhiteSpace(Value))
            {
                result.Value = log.ApplyText(Value, options);
            }
            return result;
        }
    }
}
