namespace Materal.Extensions
{
    /// <summary>
    /// 类型帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 根据描述获取枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T ConvertToEnumByDescription<T>(string description)
            where T : Enum
        {
            Type type = typeof(T);
            object obj = type.GetEnumByDescription(description);
            if (obj is not T result) throw new ExtensionException("转换失败");
            return result;
        }
    }
}
