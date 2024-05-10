namespace Materal.Extensions
{
    /// <summary>
    /// Object扩展
    /// </summary>
    public static partial class ObjectExtensions
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        static ObjectExtensions()
        {
            AddDefaultConvertDictionary();
        }
        /// <summary>
        /// 可转换类型字典
        /// </summary>
        private static readonly Dictionary<Type, Func<object, object?>> _convertDictionary = [];
        /// <summary>
        /// 添加默认转换字典
        /// </summary>
        private static void AddDefaultConvertDictionary()
        {
            _convertDictionary.Add(typeof(bool), WrapValueConvert(Convert.ToBoolean));
            _convertDictionary.Add(typeof(bool?), WrapValueConvert(Convert.ToBoolean));
            _convertDictionary.Add(typeof(int), WrapValueConvert(Convert.ToInt32));
            _convertDictionary.Add(typeof(int?), WrapValueConvert(Convert.ToInt32));
            _convertDictionary.Add(typeof(long), WrapValueConvert(Convert.ToInt64));
            _convertDictionary.Add(typeof(long?), WrapValueConvert(Convert.ToInt64));
            _convertDictionary.Add(typeof(short), WrapValueConvert(Convert.ToInt16));
            _convertDictionary.Add(typeof(short?), WrapValueConvert(Convert.ToInt16));
            _convertDictionary.Add(typeof(double), WrapValueConvert(Convert.ToDouble));
            _convertDictionary.Add(typeof(double?), WrapValueConvert(Convert.ToDouble));
            _convertDictionary.Add(typeof(float), WrapValueConvert(Convert.ToSingle));
            _convertDictionary.Add(typeof(float?), WrapValueConvert(Convert.ToSingle));
            _convertDictionary.Add(typeof(Guid), m =>
            {
                string? inputString = m.ToString();
                if (string.IsNullOrWhiteSpace(inputString) || !inputString.IsGuid())
                {
                    return Guid.Empty;
                }
                return Guid.Parse(inputString);
            });
            _convertDictionary.Add(typeof(Guid?), m =>
            {
                string? inputString = m.ToString();
                if (string.IsNullOrWhiteSpace(inputString) || !inputString.IsGuid())
                {
                    return null;
                }
                return Guid.Parse(inputString);
            });
            _convertDictionary.Add(typeof(string), Convert.ToString);
            _convertDictionary.Add(typeof(decimal), WrapValueConvert(Convert.ToDecimal));
            _convertDictionary.Add(typeof(decimal?), WrapValueConvert(Convert.ToDecimal));
            _convertDictionary.Add(typeof(DateTime), WrapValueConvert(Convert.ToDateTime));
            _convertDictionary.Add(typeof(DateTime?), WrapValueConvert(Convert.ToDateTime));
        }
        /// <summary>
        /// 添加转换字典
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        public static void AddConvertDictionary<T>(Func<object, T> func) => _convertDictionary.Add(typeof(T), WrapValueConvert(func));
        /// <summary>
        /// 写入值转换类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        private static Func<object, object?> WrapValueConvert<T>(Func<object, T?> input) => i => input(i);
        /// <summary>
        /// 判断是否提供到特定类型的转换
        /// </summary>
        /// <param name="_"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static bool CanConvertTo(this object _, Type targetType) => _convertDictionary.ContainsKey(targetType);
        /// <summary>
        /// 转换到特定类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T? ConvertTo<T>(this object obj) => (T?)ConvertTo(obj, typeof(T));
        /// <summary>
        /// 转换到特定类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static object? ConvertTo(this object obj, Type targetType)
        {
            if (obj is null) return !targetType.IsValueType ? null : throw new ArgumentNullException(nameof(obj), "不能将null转换为" + targetType.Name);
            if (obj.IsNullOrWhiteSpaceString()) return obj is string stringObj && targetType == typeof(string) ? stringObj : null;
            if (obj.GetType() == targetType || targetType.IsInstanceOfType(obj)) return obj;
            if (_convertDictionary.TryGetValue(targetType, out Func<object, object?>? value)) return value(obj);
            try
            {
                return Convert.ChangeType(obj, targetType);
            }
            catch
            {
                throw new ExtensionException("未实现到" + targetType.Name + "的转换");
            }
        }
    }
}
