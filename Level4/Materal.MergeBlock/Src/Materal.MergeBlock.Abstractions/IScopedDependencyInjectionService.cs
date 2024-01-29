namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 作用域依赖注入服务
    /// </summary>
    public interface IScopedDependencyInjectionService : IDependencyInjectionService
    {
    }
    /// <summary>
    /// 作用域依赖注入服务
    /// </summary>
    /// <typeparam name="TServiceType"></typeparam>
    public interface IScopedDependencyInjectionService<TServiceType> : IScopedDependencyInjectionService
    {
    }
}
