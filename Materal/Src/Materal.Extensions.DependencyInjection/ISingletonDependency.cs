namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.AddSingleton<![CDATA[<T>]]>()
    /// </summary>
    public interface ISingletonDependency : IRegisterType
    {
    }
    /// <summary>
    /// <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.AddSingleton<![CDATA[<T>]]>()
    /// </summary>
    public interface ISingletonDependency<T> : ISingletonDependency, IRegisterType<T>
    {
    }
    /// <summary>
    /// <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.AddSingleton<![CDATA[<T>]]>()
    /// </summary>
    public interface ISingletonDependency<T, T2> : ISingletonDependency, IRegisterType<T, T2>
    {
    }
    /// <summary>
    /// <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.AddSingleton<![CDATA[<T>]]>()
    /// </summary>
    public interface ISingletonDependency<T, T2, T3> : ISingletonDependency, IRegisterType<T, T2, T3>
    {
    }
    /// <summary>
    /// <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.AddSingleton<![CDATA[<T>]]>()
    /// </summary>
    public interface ISingletonDependency<T, T2, T3, T4> : ISingletonDependency, IRegisterType<T, T2, T3, T4>
    {
    }
}
