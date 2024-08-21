namespace Materal.Extensions
{
    /// <summary>
    /// 忽略转换为ExpandoObject
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class IgnoreToExpandoObjectAttribute : Attribute { }
}
