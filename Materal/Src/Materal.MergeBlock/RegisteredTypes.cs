using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace Materal.MergeBlock
{
    /// <summary>
    /// 注册类型
    /// </summary>
    public class RegisteredTypes
    {
        /// <summary>
        /// 类型字典
        /// </summary>
        private readonly ConcurrentDictionary<string, Type> types = new();
        /// <summary>
        /// 类型列表
        /// </summary>
        public ImmutableArray<Type> Types => types.Values.ToImmutableArray();
        /// <summary>
        /// 添加类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Add<T>() => AddType(typeof(T));
        /// <summary>
        /// 添加类型
        /// </summary>
        /// <param name="type"></param>
        public void AddType(Type type)
        {
            if (string.IsNullOrWhiteSpace(type.FullName)) return;
            if (types.ContainsKey(type.FullName))
            {
                types[type.FullName] = type;
            }
            else
            {
                types.TryAdd(type.FullName, type);
            }
        }
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Type? GetType(string name) => types.TryGetValue(name, out Type? type) ? type : null;
    }
}
