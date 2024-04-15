namespace Materal.Logger.Repositories
{
    /// <summary>
    /// 数据库字段
    /// </summary>
    public interface IDBFiled
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        string Type { get; set; }
        /// <summary>
        /// C#类型
        /// </summary>
        Type CSharpType { get; }
        /// <summary>
        /// 值
        /// </summary>
        string? Value { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        bool PK { get; set; }
        /// <summary>
        /// 索引
        /// </summary>
        string? Index { get; set; }
        /// <summary>
        /// 可以为空
        /// </summary>
        bool IsNull { get; set; }
        /// <summary>
        /// 获得创建表字段SQL
        /// </summary>
        /// <returns></returns>
        string GetCreateTableFiledSQL();
        /// <summary>
        /// 获得新的数据库字段
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IDBFiled GetNewDBFiled(LoggerWriterModel model);
    }
}
