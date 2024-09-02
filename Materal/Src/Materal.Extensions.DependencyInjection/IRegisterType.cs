namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// 注册类型
    /// </summary>
    public interface IRegisterType
    {
    }
    /// <summary>
    /// 注册类型
    /// </summary>
    public interface IRegisterType<T> : IRegisterType
    {
    }
    /// <summary>
    /// 注册类型
    /// </summary>
    public interface IRegisterType<T, T2> : IRegisterType<T>
    {
    }
    /// <summary>
    /// 注册类型
    /// </summary>
    public interface IRegisterType<T, T2, T3> : IRegisterType<T, T2>
    {
    }
    /// <summary>
    /// 注册类型
    /// </summary>
    public interface IRegisterType<T, T2, T3, T4> : IRegisterType<T, T2, T3>
    {
    }
}
