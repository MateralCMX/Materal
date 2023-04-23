namespace Materal.BaseCore.CodeGenerator
{
    /// <summary>
    /// 列类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ColumnTypeAttribute : Attribute
    {
        /// <summary>
        /// SQL类型
        /// </summary>
        public string SqlType { get; private set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public ColumnTypeAttribute(string sqlType)
        {
            SqlType = sqlType;
        }
    }
}
