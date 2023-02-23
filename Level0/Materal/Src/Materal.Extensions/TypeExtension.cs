﻿using Materal.Extensions;
using System.Data;
using System.Reflection;

namespace System
{
    /// <summary>
    /// 类型扩展
    /// </summary>
    public static class TypeExtension
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
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object Instantiation(this Type type, params object[] args) => InstantiationOrDefault(type, args) ?? throw new ExtensionException("实例化失败");
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? InstantiationOrDefault(this Type type, params object[] args)
        {
            Type[] argTypes = args.Select(m => m.GetType()).ToArray();
            ConstructorInfo? constructorInfo = type.GetConstructor(argTypes);
            if (constructorInfo == null) return default;
            object result = constructorInfo.Invoke(args);
            return result;
        }
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T Instantiation<T>(this Type type, params object[] args)
        {
            object? obj = InstantiationOrDefault(type, args);
            if (obj == null || obj is not T result) throw new ExtensionException("实例化失败");
            return result;
        }
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T? InstantiationOrDefault<T>(this Type type, params object[] args)
        {
            object? obj = InstantiationOrDefault(type, args);
            if (obj == null || obj is not T result) return default;
            return result;
        }
        /// <summary>
        /// 是否可作为类型使用
        /// </summary>
        /// <param name="type"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static bool IsAssignableTo(this Type type, Type targetType)
        {
            if (targetType.IsInterface)
            {
                List<Type> allInterfaces = type.GetAllInterfaces();
                return allInterfaces.Any(m => m.FullName == targetType.FullName);
            }
            else if (targetType.IsClass)
            {
                if (type == targetType) return true;
                List<Type> allBaseTypes = type.GetAllBaseType();
                return allBaseTypes.Any(m => m.FullName == targetType.FullName);
            }
            return false;
        }
        /// <summary>
        /// 获得所有的基类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static List<Type> GetAllBaseType(this Type type)
        {
            List<Type> allBaseTypes = new();
            Type? temp = type;
            Type objType = typeof(object);
            while (temp != objType)
            {
                if (temp == null) break;
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
            List<Type> allInterfaces = new();
            foreach (var item in interfaces)
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
            var dt = new DataTable();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo item in props)
            {
                Type colType = item.PropertyType;
                if (colType.IsGenericType && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                var dc = new DataColumn(item.Name, colType);
                dt.Columns.Add(dc);
            }
            dt.TableName = type.Name;
            return dt;
        }
        /// <summary>
        /// 获取所有枚举
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Enum> GetAllEnum(this Type type)
        {
            if (!type.IsEnum) throw new ExtensionException("该类型不是枚举类型");
            var result = new List<Enum>();
            Array allEnums = Enum.GetValues(type);
            foreach (Enum item in allEnums)
            {
                result.Add(item);
            }
            return result;
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
    }
}
