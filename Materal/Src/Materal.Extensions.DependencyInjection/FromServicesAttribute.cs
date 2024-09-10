namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// 从服务获取特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FromServicesAttribute : Attribute
    {
    }
}