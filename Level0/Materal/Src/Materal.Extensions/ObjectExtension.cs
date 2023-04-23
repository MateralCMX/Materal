using Materal.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
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
            _convertDictionary.Add(typeof(DateTime), WrapValueConvert(Convert.ToDateTime));
            _convertDictionary.Add(typeof(DateTime?), WrapValueConvert(Convert.ToDateTime));
        }
        /// <summary>
        /// 添加转换字典
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        public static void AddConvertDictionary<T>(Func<object, T> func) where T : notnull => _convertDictionary.Add(typeof(T), WrapValueConvert(func));
        /// <summary>
        /// 写入值转换类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        private static Func<object, object> WrapValueConvert<T>(Func<object, T> input) where T : notnull => i => input(i);
        /// <summary>
        /// 属性复制sourceM->targetM
        /// </summary>
        /// <typeparam name="T">复制的模型</typeparam>
        /// <param name="sourceM">复制源头对象</param>
        /// <param name="targetM">复制目标对象</param>
        /// <param name="isCopy">是否复制</param>
        /// <returns>复制的对象</returns>
        public static void CopyProperties<T>(this object sourceM, T targetM, Func<PropertyInfo, object?, bool> isCopy)
        {
            if (sourceM == null) return;
            PropertyInfo[] t1Props = sourceM.GetType().GetProperties();
            PropertyInfo[] t2Props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in t1Props)
            {
                PropertyInfo? tempProp = t2Props.FirstOrDefault(m => m.Name == prop.Name);
                if (tempProp != null && tempProp.CanWrite)
                {
                    object? value = prop.GetValue(sourceM, null);
                    if (isCopy(tempProp, value))
                    {
                        tempProp.SetValue(targetM, value, null);
                    }
                }
            }
        }
        /// <summary>
        /// 属性复制
        /// </summary>
        /// <typeparam name="T">复制的模型</typeparam>
        /// <param name="sourceM">复制源头对象</param>
        /// <param name="isCopy">是否复制</param>
        /// <returns>复制的对象</returns>
        public static T CopyProperties<T>(this object sourceM, Func<PropertyInfo, object?, bool> isCopy)
        {
            T targetM = TypeHelper.Instantiation<T>();
            sourceM.CopyProperties(targetM, isCopy);
            return targetM;
        }
        /// <summary>
        /// 属性复制sourceM->targetM
        /// </summary>
        /// <typeparam name="T">复制的模型</typeparam>
        /// <param name="sourceM">复制源头对象</param>
        /// <param name="targetM">复制目标对象</param>
        /// <param name="notCopyPropertyNames">不复制的属性名称</param>
        /// <returns>复制的对象</returns>
        public static void CopyProperties<T>(this object sourceM, T targetM, params string[] notCopyPropertyNames)
        {
            if (sourceM == null) return;
            PropertyInfo[] t1Props = sourceM.GetType().GetProperties();
            PropertyInfo[] t2Props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in t1Props)
            {
                if (notCopyPropertyNames.Contains(prop.Name)) continue;
                PropertyInfo? tempProp = t2Props.FirstOrDefault(m => m.Name == prop.Name);
                if (tempProp != null && tempProp.CanWrite)
                {
                    tempProp.SetValue(targetM, prop.GetValue(sourceM, null), null);
                }
            }
        }
        /// <summary>
        /// 属性复制
        /// </summary>
        /// <typeparam name="T">复制的模型</typeparam>
        /// <param name="sourceM">复制源头对象</param>
        /// <param name="notCopyPropertyNames">不复制的属性名称</param>
        /// <returns>复制的对象</returns>
        public static T CopyProperties<T>(this object sourceM, params string[] notCopyPropertyNames)
        {
            T targetM = TypeHelper.Instantiation<T>();
            sourceM.CopyProperties(targetM, notCopyPropertyNames);
            return targetM;
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
        public static T? CloneByJson<T>(this T inputObj)
        {
            if (inputObj == null) return default;
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
            Type tType = inputObj.GetType();
            Attribute? attr = tType.GetCustomAttribute(typeof(SerializableAttribute));
            if (attr == null) throw new ExtensionException("未标识为可序列化");
            object? resM;
            using (var ms = new MemoryStream())
            {
                var xml = new XmlSerializer(typeof(T));
                xml.Serialize(ms, inputObj);
                ms.Seek(0, SeekOrigin.Begin);
                resM = xml.Deserialize(ms);
                ms.Close();
            }
            return (T?)resM;
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
            T? resM = (T?)Activator.CreateInstance(tType);
            PropertyInfo[] pis = tType.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                object? piValue = pi.GetValue(inputObj);
                if (piValue == null) continue;
                pi.SetValue(resM, piValue is ValueType ? piValue : Clone(piValue));
            }
            return resM;
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
            return CloneByJson(inputObj);
        }
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <returns>描述</returns>
        public static string GetDescription(this object inputObj)
        {
            Type objType = inputObj.GetType();
            string? inputString = inputObj.ToString();
            if (string.IsNullOrEmpty(inputString)) return string.Empty;
            FieldInfo? fieldInfo = objType.GetField(inputString);
            Attribute? attribute = fieldInfo != null ?
                fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute)) :
                objType.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute != null && attribute is DescriptionAttribute descriptionAttribute ?
                descriptionAttribute.Description :
                throw new ExtensionException("需要特性DescriptionAttribute");
        }
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>描述</returns>
        public static string GetDescription(this object inputObj, string propertyName)
        {
            Type objType = inputObj.GetType();
            PropertyInfo? propertyInfo = objType.GetProperty(propertyName);
            if (propertyInfo == null) throw new ExtensionException($"未找到名称是{propertyName}的属性");
            var attribute = propertyInfo.GetCustomAttribute<DescriptionAttribute>();
            return attribute != null ?
                attribute.Description :
                throw new ExtensionException("需要特性DescriptionAttribute");
        }
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
        /// 属性是否包含
        /// </summary>
        /// <param name="leftModel"></param>
        /// <param name="rightModel"></param>
        /// <param name="maps"></param>
        /// <returns></returns>
        public static bool PropertyContains(this object leftModel, object rightModel, Dictionary<string, Func<bool>>? maps = null)
        {
            Type aType = leftModel.GetType();
            Type bType = rightModel.GetType();
            foreach (PropertyInfo aProperty in aType.GetProperties())
            {
                if (maps != null && maps.ContainsKey(aProperty.Name))
                {
                    bool mapResult = maps[aProperty.Name].Invoke();
                    if (!mapResult) return false;
                }
                else
                {
                    PropertyInfo? bProperty = bType.GetProperty(aProperty.Name);
                    if (bProperty == null || aProperty.PropertyType != bProperty.PropertyType) return false;
                    object? aValue = aProperty.GetValue(leftModel);
                    object? bValue = bProperty.GetValue(rightModel);
                    if (aValue != bValue) return false;
                }
            }
            return true;
        }
    }
}
