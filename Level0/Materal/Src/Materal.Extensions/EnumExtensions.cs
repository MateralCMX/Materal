namespace Materal.Extensions
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取所有枚举
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static List<Enum> GetAllEnum(this Enum @enum)
        {
            Type type = @enum.GetType();
            return type.GetAllEnum();
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
        /// <param name="enum"></param>
        /// <returns></returns>
        public static int GetEnumCount(this Enum @enum)
        {
            Type type = @enum.GetType();
            return type.GetEnumCount();
        }
    }
}
