namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// 属性注入特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class PropertyInjectionAttribute : Attribute
    {
    }
}
