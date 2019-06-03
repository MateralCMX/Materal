using System;
using System.Collections.Generic;
using Materal.Common;
using Materal.EnumHelper;

namespace Common
{
    /// <summary>
    /// 键值对模型
    /// </summary>
    public class KeyValueModel
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public KeyValueModel(int key, string value)
        {
            Key = key;
            Value = value;
        }
        /// <summary>
        /// 键
        /// </summary>
        public int Key { get; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; }
        /// <summary>
        /// 获得所有识别码
        /// </summary>
        /// <returns>所有识别码</returns>
        public static List<KeyValueModel> GetAllCode(Type enumType)
        {
            var allCodeList = enumType.GetAllEnum();
            var resM = new List<KeyValueModel>();
            foreach (Enum item in allCodeList)
            {
                resM.Add(new KeyValueModel(Convert.ToInt32(item), item.GetDescription()));
            }
            return resM;
        }
    }
}
