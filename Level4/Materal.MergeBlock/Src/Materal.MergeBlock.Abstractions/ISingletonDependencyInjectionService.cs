namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 单例依赖注入服务
    /// </summary>
    public interface ISingletonDependencyInjectionService : IDependencyInjectionService
    {
    }
    /// <summary>
    /// 单例依赖注入服务
    /// </summary>
    /// <typeparam name="TServiceType"></typeparam>
    public interface ISingletonDependencyInjectionService<TServiceType> : ISingletonDependencyInjectionService
    {
    }
}
