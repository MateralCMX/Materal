namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.AddTransient<![CDATA[<T>]]>()
    /// </summary>
    public interface ITransientDependency : IRegisterType
    {
    }
    /// <summary>
    /// <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.AddTransient<![CDATA[<T>]]>()
    /// </summary>
    public interface ITransientDependency<T> : ITransientDependency, IRegisterType<T>
    {
    }
    /// <summary>
    /// <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.AddTransient<![CDATA[<T>]]>()
    /// </summary>
    public interface ITransientDependency<T, T2> : ITransientDependency, IRegisterType<T, T2>
    {
    }
    /// <summary>
    /// <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.AddTransient<![CDATA[<T>]]>()
    /// </summary>
    public interface ITransientDependency<T, T2, T3> : ITransientDependency, IRegisterType<T, T2, T3>
    {
    }
    /// <summary>
    /// <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.AddTransient<![CDATA[<T>]]>()
    /// </summary>
    public interface ITransientDependency<T, T2, T3, T4> : ITransientDependency, IRegisterType<T, T2, T3, T4>
    {
    }
}
