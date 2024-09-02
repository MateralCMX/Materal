namespace Materal.Abstractions
{
    /// <summary>
    /// 对象访问器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObjectAccessor<out T>
    {
        /// <summary>
        /// 值
        /// </summary>
        T Value { get; }
    }
}
