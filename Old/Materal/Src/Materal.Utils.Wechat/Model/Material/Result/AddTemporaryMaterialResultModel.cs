﻿using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Materal.Utils.Wechat.Model.Material.Result
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AddTemporaryMaterialResultModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        [Description("类型")]
        public MaterialTypeEnum MaterialType => (MaterialTypeEnum)Enum.Parse(typeof(MaterialTypeEnum), type);
        /// <summary>
        /// 类型文本
        /// </summary>
        [Description("类型文本")]
        public string type { get; set; }
        /// <summary>
        /// 媒体标识
        /// </summary>
        [Description("媒体标识")]
        public string media_id { get; set; }
        /// <summary>
        /// 创建时间时间戳
        /// </summary>
        [Description("创建时间时间戳")]
        public string created_at { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        public DateTime CreateTime => DateTimeHelper.TimeStampToDateTime(long.Parse(created_at + "0000000"));
    }
}
