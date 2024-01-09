namespace Materal.MergeBlock.GeneratorCode.Attributers
{
    /// <summary>
    /// 不生成ListDTO
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class NotListDTOAttribute : Attribute { }
}
