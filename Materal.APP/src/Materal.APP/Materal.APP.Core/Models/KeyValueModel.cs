using System;
using System.Collections.Generic;
using System.Linq;
using Materal.Common;
using Materal.EnumHelper;

namespace Materal.APP.Core.Models
{
    /// <summary>
    /// 键值对模型
    /// </summary>
    public class KeyValueModel
    {
        public KeyValueModel(Enum @enum)
        {
            ChangeByEnum(@enum);
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public KeyValueModel(string key, string value)
        {
            Key = key;
            Value = value;
        }
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 携带数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 获得枚举值
        /// </summary>
        /// <returns></returns>
        public int GetEnumValue()
        {
            return Convert.ToInt32(Key);
        }
        /// <summary>
        /// 通过枚举更改
        /// </summary>
        /// <param name="enum"></param>
        public void ChangeByEnum(Enum @enum)
        {
            Key = Convert.ToInt32(@enum).ToString();
            Value = @enum.GetDescription();
        }
        /// <summary>
        /// 获得所有识别码
        /// </summary>
        /// <returns>所有识别码</returns>
        public static List<KeyValueModel> GetAllCode(Type enumType)
        {
            List<Enum> allCodeList = enumType.GetAllEnum();
            return allCodeList.Select(item => new KeyValueModel(item)).ToList();
        }
    }
}
