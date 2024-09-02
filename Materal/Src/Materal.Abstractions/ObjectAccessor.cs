namespace Materal.Abstractions
{
    /// <summary>
    /// 对象访问器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectAccessor<T>(T value) : IObjectAccessor<T>
    {
        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; private set; } = value;
    }
}
