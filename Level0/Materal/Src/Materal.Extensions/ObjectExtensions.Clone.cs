using System.Xml.Serialization;

namespace Materal.Extensions
{
    /// <summary>
    /// Object扩展
    /// </summary>
    public static partial class ObjectExtensions
    {
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
            object? result;
            using (MemoryStream ms = new())
            {
                XmlSerializer xml = new(typeof(T));
                xml.Serialize(ms, inputObj);
                ms.Seek(0, SeekOrigin.Begin);
                result = xml.Deserialize(ms);
                ms.Close();
            }
            return result is null ? default : (T)result;
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
                if (value is null) continue;
                propertyInfo.SetValue(resM, value is ValueType ? value : Clone(value));
            }
            return resM;
        }
#if NETSTANDARD
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
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new();
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
#else
        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputObj">输入对象</param>
        /// <returns>克隆的对象</returns>
        public static T? Clone<T>(this T inputObj)
            where T : notnull => CloneByJson(inputObj);
#endif
    }
}
