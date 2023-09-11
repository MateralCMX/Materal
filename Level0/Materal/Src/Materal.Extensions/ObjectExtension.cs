using Materal.Extensions;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace System
{
    /// <summary>
    /// Object扩展
    /// </summary>
    public static partial class ObjectExtension
    {
        /// <summary>
        /// 可转换类型字典
        /// </summary>
        private static readonly Dictionary<Type, Func<object, object?>> _convertDictionary = new();
        /// <summary>
        /// 构造方法
        /// </summary>
        static ObjectExtension()
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
        private static Func<object, object?> WrapValueConvert<T>(Func<object, T?> input)  => i => input(i);
        /// <summary>
        /// 属性复制sourceM->targetM
        /// </summary>
        /// <typeparam name="T">复制的模型</typeparam>
        /// <param name="source">复制源头对象</param>
        /// <param name="target">复制目标对象</param>
        /// <param name="isCopy">是否复制</param>
        /// <returns>复制的对象</returns>
        public static void CopyProperties<T>(this object source, T target, Func<PropertyInfo, object?, bool> isCopy)
        {
            if (source == null) return;
            PropertyInfo[] t1Props = source.GetType().GetProperties();
            PropertyInfo[] t2Props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in t1Props)
            {
                PropertyInfo? tempProp = t2Props.FirstOrDefault(m => m.Name == prop.Name);
                if (tempProp != null && tempProp.CanWrite)
                {
                    object? value = prop.GetValue(source, null);
                    if (isCopy(tempProp, value))
                    {
                        tempProp.SetValue(target, value, null);
                    }
                }
            }
        }
        /// <summary>
        /// 属性复制
        /// </summary>
        /// <typeparam name="T">复制的模型</typeparam>
        /// <param name="source">复制源头对象</param>
        /// <param name="isCopy">是否复制</param>
        /// <returns>复制的对象</returns>
        public static T CopyProperties<T>(this object source, Func<PropertyInfo, object?, bool> isCopy)
        {
            T result = typeof(T).Instantiation<T>();
            source.CopyProperties(result, isCopy);
            return result;
        }
        /// <summary>
        /// 属性复制sourceM->targetM
        /// </summary>
        /// <typeparam name="T">复制的模型</typeparam>
        /// <param name="source">复制源头对象</param>
        /// <param name="target">复制目标对象</param>
        /// <param name="notCopyPropertyNames">不复制的属性名称</param>
        /// <returns>复制的对象</returns>
        public static void CopyProperties<T>(this object source, T target, params string[] notCopyPropertyNames)
        {
            if (source == null) return;
            PropertyInfo[] t1Props = source.GetType().GetProperties();
            PropertyInfo[] t2Props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in t1Props)
            {
                if (notCopyPropertyNames.Contains(prop.Name)) continue;
                PropertyInfo? tempProp = t2Props.FirstOrDefault(m => m.Name == prop.Name);
                if (tempProp != null && tempProp.CanWrite)
                {
                    tempProp.SetValue(target, prop.GetValue(source, null), null);
                }
            }
        }
        /// <summary>
        /// 属性复制
        /// </summary>
        /// <typeparam name="T">复制的模型</typeparam>
        /// <param name="source">复制源头对象</param>
        /// <param name="notCopyPropertyNames">不复制的属性名称</param>
        /// <returns>复制的对象</returns>
        public static T CopyProperties<T>(this object source, params string[] notCopyPropertyNames)
        {
            T result = typeof(T).Instantiation<T>();
            source.CopyProperties(result, notCopyPropertyNames);
            return result;
        }
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
            if (obj == null) return !targetType.IsValueType ? null : throw new ArgumentNullException(nameof(obj), "不能将null转换为" + targetType.Name);
            if (obj.GetType() == targetType || targetType.IsInstanceOfType(obj)) return obj;
            if (_convertDictionary.ContainsKey(targetType)) return _convertDictionary[targetType](obj);
            try
            {
                return Convert.ChangeType(obj, targetType);
            }
            catch
            {
                throw new ExtensionException("未实现到" + targetType.Name + "的转换");
            }
        }
        /// <summary>
        /// 克隆对象(Json序列化)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputObj">输入对象</param>
        /// <returns></returns>
        public static T CloneByJson<T>(this T inputObj)
            where T : notnull
        {
            string jsonStr = inputObj.ToJson();
            return jsonStr.JsonToObject<T>();
        }
        /// <summary>
        /// 克隆对象(XML序列化)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputObj">输入对象</param>
        /// <returns>克隆的对象</returns>
        public static T? CloneByXml<T>(this T inputObj)
            where T : notnull
        {
            object result;
            using (var ms = new MemoryStream())
            {
                var xml = new XmlSerializer(typeof(T));
                xml.Serialize(ms, inputObj);
                ms.Seek(0, SeekOrigin.Begin);
                result = xml.Deserialize(ms);
                ms.Close();
            }
            return (T)result;
        }
        /// <summary>
        /// 克隆对象(反射)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputObj">输入对象</param>
        /// <returns>克隆的对象</returns>
        public static T? CloneByReflex<T>(this T inputObj)
            where T : notnull
        {
            Type tType = inputObj.GetType();
            T resM = tType.Instantiation<T>();
            PropertyInfo[] propertyInfos = tType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                object? value = propertyInfo.GetValue(inputObj);
                if (value == null) continue;
                propertyInfo.SetValue(resM, value is ValueType ? value : Clone(value));
            }
            return resM;
        }
        /// <summary>
        /// 克隆对象(字节流序列化)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputObj"></param>
        /// <returns></returns>
        public static T? CloneBySerializable<T>(this T inputObj)
            where T : notnull
        {
            if (!inputObj.GetType().HasCustomAttribute<SerializableAttribute>()) throw new ExtensionException("未标识为可序列化");
            MemoryStream stream = new();
            BinaryFormatter formatter = new();
            formatter.Serialize(stream, inputObj);
            stream.Position = 0;
            object obj = formatter.Deserialize(stream);
            if (obj is T result) return result;
            return default;
        }
        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputObj">输入对象</param>
        /// <returns>克隆的对象</returns>
        public static T? Clone<T>(this T inputObj)
            where T : notnull
        {
            SerializableAttribute? serializableAttribute = inputObj.GetType().GetCustomAttribute<SerializableAttribute>();
            return serializableAttribute is not null ? CloneBySerializable(inputObj) : CloneByJson(inputObj);
        }
        #region 获得描述
        /// <summary>
        /// 描述字段名称
        /// </summary>
        private const string DescriptionMemberName = "Description";
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <returns>描述</returns>
        public static string GetDescription(this object inputObj)
        {
            if (inputObj is Enum @enum)
            {
                return inputObj.GetDescription(@enum.ToString());
            }
            else
            {
                Type objType = inputObj.GetType();
                DescriptionAttribute? attribute = objType.GetCustomAttribute<DescriptionAttribute>();
                if (attribute != null) return attribute.Description;
                const string descriptionName = DescriptionMemberName;
                return inputObj.GetDescription(descriptionName);
            }
        }
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <returns>描述</returns>
        public static string? GetDescriptionOrNull(this object inputObj)
        {
            try
            {
                return inputObj.GetDescription();
            }
            catch (ExtensionException)
            {
                return null;
            }
        }
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <param name="memberInfo">属性名称</param>
        /// <returns>描述</returns>
        public static string GetDescription(this object inputObj, MemberInfo memberInfo)
        {
            DescriptionAttribute? attribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();
            if(attribute != null) return attribute.Description;
            if (inputObj is Enum @enum) return @enum.ToString();
            if (memberInfo.Name == DescriptionMemberName)
            {
                object? value = memberInfo.GetValue(inputObj);
                if(value != null && value is string descriptionValue)
                {
                    return descriptionValue;
                }
            }
            throw new ExtensionException($"未找到特性{nameof(DescriptionAttribute)}");
        }
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <param name="memberInfo">属性名称</param>
        /// <returns>描述</returns>
        public static string? GetDescriptionOrNull(this object inputObj, MemberInfo memberInfo)
        {
            try
            {
                return inputObj.GetDescription(memberInfo);
            }
            catch (ExtensionException)
            {
                return null;
            }
        }
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <param name="memberName">属性名称</param>
        /// <returns>描述</returns>
        public static string GetDescription(this object inputObj, string memberName)
        {
            Type objType = inputObj.GetType();
            MemberInfo? memberInfo = objType.GetRuntimeField(memberName);
            memberInfo ??= objType.GetRuntimeProperty(memberName);
            if (memberInfo == null) throw new ExtensionException($"未找到字段或属性{memberName}");
            return inputObj.GetDescription(memberInfo);
        }
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <param name="memberName">属性名称</param>
        /// <returns>描述</returns>
        public static string? GetDescriptionOrNull(this object inputObj, string memberName)
        {
            try
            {
                return inputObj.GetDescription(memberName);
            }
            catch (ExtensionException)
            {
                return null;
            }
        }
        #endregion
        /// <summary>
        /// 对象是否为空或者空字符串
        /// </summary>
        /// <param name="inputObj"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyString(this object inputObj)
        {
            return inputObj switch
            {
                null => true,
                string inputStr => string.IsNullOrEmpty(inputStr),
                _ => false,
            };
        }
        /// <summary>
        /// 对象是否为空或者空或者空格字符串
        /// </summary>
        /// <param name="inputObj"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpaceString(this object inputObj)
        {
            return inputObj switch
            {
                null => true,
                string inputStr => string.IsNullOrWhiteSpace(inputStr),
                _ => false,
            };
        }
        /// <summary>
        /// 对象是否相等
        /// </summary>
        /// <param name="aModel"></param>
        /// <param name="bModel"></param>
        /// <param name="maps"></param>
        /// <returns></returns>
        public static bool Equals(this object aModel, object bModel, Dictionary<string, Func<object?, bool>> maps)
        {
            Type aType = aModel.GetType();
            Type bType = bModel.GetType();
            foreach (PropertyInfo aProperty in aType.GetProperties())
            {
                object? aValue = aProperty.GetValue(aModel);
                if (maps.ContainsKey(aProperty.Name))
                {
                    bool mapResult = maps[aProperty.Name].Invoke(aValue);
                    if (!mapResult) return false;
                }
                else
                {
                    PropertyInfo? bProperty = bType.GetProperty(aProperty.Name);
                    if (bProperty == null || aProperty.PropertyType != bProperty.PropertyType) return false;
                    object? bValue = bProperty.GetValue(bModel);
                    if (aValue != bValue) return false;
                }
            }
            return true;
        }
    }
}
