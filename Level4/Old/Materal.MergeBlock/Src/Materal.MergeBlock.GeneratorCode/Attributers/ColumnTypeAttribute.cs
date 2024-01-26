namespace Materal.MergeBlock.GeneratorCode.Attributers
{
    /// <summary>
    /// 列类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ColumnTypeAttribute(string sqlType) : Attribute
    {
        /// <summary>
        /// SQL类型
        /// </summary>
        public string SqlType { get; private set; } = sqlType;
    }
}
