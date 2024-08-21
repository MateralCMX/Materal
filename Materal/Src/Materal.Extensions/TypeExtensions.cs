using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Materal.Extensions
{
    /// <summary>
    /// 类型扩展
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// 根据描述获取枚举
        /// </summary>
        /// <param name="type"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static object GetEnumByDescription(this Type type, string description)
        {
            if (!type.IsEnum) throw new ExtensionException("该类型不是枚举类型");
            List<Enum> allEnums = type.GetAllEnum();
            foreach (Enum @enum in allEnums)
            {
                if (@enum.GetDescription().Equals(description))
                {
                    return Enum.Parse(type, @enum.ToString());
                }
            }
            throw new ExtensionException("未找到该描述的枚举");
        }
        #region 类型实例化
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? InstantiationOrDefault(this Type type, object[]? args = null)
        {
            args ??= [];
            try
            {
                return Activator.CreateInstance(type, [.. args]);
            }
            catch
            {
                Type[] argTypes = args.Select(m => m.GetType()).ToArray();
                ConstructorInfo? constructorInfo = type.GetConstructor(argTypes);
                if (constructorInfo is null) return null;
                object result = constructorInfo.Invoke(args);
                return result;
            }
        }
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? InstantiationOrDefault(this Type type, IServiceProvider serviceProvider, object[]? args = null)
        {
            args ??= [];
            try
            {
                return ActivatorUtilities.CreateInstance(serviceProvider, type, [.. args]);
            }
            catch
            {
                ConstructorInfo[] constructorInfos = [.. type.GetConstructors().OrderByDescending(m => m.GetParameters().Length)];
                foreach (ConstructorInfo constructorInfo in constructorInfos)
                {
                    List<object> argList = [.. args];
                    List<object?> trueArguments = [];
                    ParameterInfo[] argumentInfos = constructorInfo.GetParameters();
                    foreach (ParameterInfo argumentInfo in argumentInfos)
                    {
                        bool isOK = false;
                        for (int i = 0; i < argList.Count; i++)
                        {
                            if (argList[i].GetType() != argumentInfo.ParameterType) continue;
                            trueArguments.Add(argList[i]);
                            argList.RemoveAt(i);
                            isOK = true;
                            break;
                        }
                        if (isOK) continue;
                        object? argument = serviceProvider.GetService(argumentInfo.ParameterType);
                        trueArguments.Add(argument);
                    }
                    if (trueArguments.Count != argumentInfos.Length) continue;
                    object result = constructorInfo.Invoke([.. trueArguments]);
                    return result;
                }
                return null;
            }
        }
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object Instantiation(this Type type, object[]? args = null)
            => type.InstantiationOrDefault(args) ?? throw new ExtensionException("实例化失败");
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object Instantiation(this Type type, IServiceProvider serviceProvider, object[]? args = null)
            => type.InstantiationOrDefault(serviceProvider, args) ?? throw new ExtensionException("实例化失败");
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T? InstantiationOrDefault<T>(this Type type, object[]? args = null)
        {
            object? obj = type.InstantiationOrDefault(args);
            if (obj is null || obj is not T result) return default;
            return result;
        }
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T? InstantiationOrDefault<T>(this Type type, IServiceProvider serviceProvider, object[]? args = null)
        {
            object? obj = type.InstantiationOrDefault(serviceProvider, args);
            if (obj is null || obj is not T result) return default;
            return result;
        }
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T Instantiation<T>(this Type type, object[]? args = null)
            => type.InstantiationOrDefault<T>(args) ?? throw new ExtensionException("实例化失败");
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T Instantiation<T>(this Type type, IServiceProvider serviceProvider, object[]? args = null)
            => type.InstantiationOrDefault<T>(serviceProvider, args) ?? throw new ExtensionException("实例化失败");
        #endregion
        /// <summary>
        /// 是否可作为类型使用
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAssignableFrom<T>(this Type type) => typeof(T).IsAssignableFrom(type);
        /// <summary>
        /// 是否可作为类型使用
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAssignableTo<T>(this Type type) => type.IsAssignableTo(typeof(T));
#if NETSTANDARD
        /// <summary>
        /// 是否可作为类型使用
        /// </summary>
        /// <param name="type"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static bool IsAssignableTo(this Type type, Type targetType) => targetType.IsAssignableFrom(type);
#endif
        /// <summary>
        /// 获得所有的基类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Type> GetAllBaseType(this Type type)
        {
            List<Type> allBaseTypes = [];
            Type? temp = type;
            Type objType = typeof(object);
            while (temp != objType)
            {
                if (temp is null) break;
                allBaseTypes.Add(temp);
                temp = temp.BaseType;
            }
            allBaseTypes.Add(objType);
            return allBaseTypes;
        }
        /// <summary>
        /// 获得所有接口
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Type> GetAllInterfaces(this Type type) => new(GetAllInterfaces(type.GetInterfaces()));
        /// <summary>
        /// 获得所有接口
        /// </summary>
        /// <param name="interfaces"></param>
        /// <returns></returns>
        private static List<Type> GetAllInterfaces(Type[] interfaces)
        {
            List<Type> allInterfaces = [];
            foreach (Type item in interfaces)
            {
                allInterfaces.Add(item);
                Type[] itemInterfaces = item.GetInterfaces();
                if (itemInterfaces.Length <= 0) continue;
                allInterfaces.AddRange(GetAllInterfaces(itemInterfaces));
            }
            return allInterfaces.Distinct().ToList();
        }
        /// <summary>
        /// 将类型转换为数据表
        /// 该数据表的列即为类型的属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>数据表</returns>
        public static DataTable ToDataTable(this Type type)
        {
            DataTable dt = new();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo item in props)
            {
                Type colType = item.PropertyType;
                if (colType.IsGenericType && colType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                DataColumn dc = new(item.Name, colType);
                dt.Columns.Add(dc);
            }
            dt.TableName = type.Name;
            return dt;
        }
        /// <summary>
        /// 获取枚举总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetEnumCount(this Type type)
        {
            if (!type.IsEnum) throw new ExtensionException("该类型不是枚举类型");
            return Enum.GetValues(type).Length;
        }
        /// <summary>
        /// 是否有特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool HasCustomAttribute<T>(this Type type)
            where T : Attribute
        {
            Attribute? attr = type.GetCustomAttribute<T>();
            return attr is not null;
        }
        /// <summary>
        /// 是否可空类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullableType(this Type type) => !type.IsClass && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        /// <summary>
        /// 是否可空类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="genericType"></param>
        /// <returns></returns>
        public static bool IsNullableType(this Type type, Type genericType) => !IsNullableType(genericType) && IsNullableType(type) && genericType.IsStruct() && type == typeof(Nullable<>).MakeGenericType(genericType);
        /// <summary>
        /// 是否可空类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullableType<T>(this Type type) => IsNullableType(type, typeof(T));
        /// <summary>
        /// 类型是否为结构体
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsStruct(this Type type) => type.IsValueType && !type.IsEnum;
    }
}
