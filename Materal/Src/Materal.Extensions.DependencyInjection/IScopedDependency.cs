namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.AddScoped<![CDATA[<T>]]>()
    /// </summary>
    public interface IScopedDependency : IRegisterType
    {
    }
    /// <summary>
    /// <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.AddScoped<![CDATA[<T>]]>()
    /// </summary>
    public interface IScopedDependency<T> : IScopedDependency, IRegisterType<T>
    {
    }
    /// <summary>
    /// <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.AddScoped<![CDATA[<T>]]>()
    /// </summary>
    public interface IScopedDependency<T, T2> : IScopedDependency, IRegisterType<T, T2>
    {
    }
    /// <summary>
    /// <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.AddScoped<![CDATA[<T>]]>()
    /// </summary>
    public interface IScopedDependency<T, T2, T3> : IScopedDependency, IRegisterType<T, T2, T3>
    {
    }
    /// <summary>
    /// <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.AddScoped<![CDATA[<T>]]>()
    /// </summary>
    public interface IScopedDependency<T, T2, T3, T4> : IScopedDependency, IRegisterType<T, T2, T3, T4>
    {
    }
}
