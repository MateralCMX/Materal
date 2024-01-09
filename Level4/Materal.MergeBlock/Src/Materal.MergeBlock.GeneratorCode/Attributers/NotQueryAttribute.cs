namespace Materal.BaseCore.CodeGenerator
{
    /// <summary>
    /// 不查询
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class NotQueryAttribute : Attribute { }
}
