namespace Materal.BaseCore.CodeGenerator
{
    /// <summary>
    /// 不编辑
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class NotEditAttribute : Attribute { }
}
