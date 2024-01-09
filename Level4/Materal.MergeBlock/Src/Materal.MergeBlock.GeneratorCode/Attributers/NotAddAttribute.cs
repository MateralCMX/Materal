namespace Materal.BaseCore.CodeGenerator
{
    /// <summary>
    /// 不添加
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class NotAddAttribute : Attribute { }
}
