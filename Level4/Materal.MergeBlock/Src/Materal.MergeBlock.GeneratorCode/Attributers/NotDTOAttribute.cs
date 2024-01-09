namespace Materal.BaseCore.CodeGenerator
{
    /// <summary>
    /// 不生成DTO
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class NotDTOAttribute : Attribute { }
}
