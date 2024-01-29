namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 瞬时依赖注入服务
    /// </summary>
    public interface ITransientDependencyInjectionService : IDependencyInjectionService
    {
    }
    /// <summary>
    /// 瞬时依赖注入服务
    /// </summary>
    /// <typeparam name="TServiceType"></typeparam>
    public interface ITransientDependencyInjectionService<TServiceType> : ITransientDependencyInjectionService
    {
    }
}
