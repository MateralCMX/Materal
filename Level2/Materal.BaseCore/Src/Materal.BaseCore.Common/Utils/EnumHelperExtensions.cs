namespace Materal.BaseCore.Common.Utils
{
    public static class EnumHelperExtensions
    {
        /// <summary>
        /// 通过描述获取枚举
        /// </summary>
        /// <param name="enumDescription"></param>
        /// <returns></returns>
        public static T GetEnumByDescription<T>(this string enumDescription) where T : Enum
        {
            var allCodeList = typeof(T).GetAllEnum();
            foreach (Enum item in allCodeList)
            {
                if (enumDescription == item.GetDescription())
                {
                    return (T)item;
                }
            }
            throw new InvalidOperationException("未找到对应枚举");
        }
    }
}
