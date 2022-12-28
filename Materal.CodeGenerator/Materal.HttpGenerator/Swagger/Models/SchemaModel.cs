﻿using Materal.ConvertHelper;
using Newtonsoft.Json.Linq;
using System;

namespace Materal.HttpGenerator.Swagger.Models
{
    public class SchemaModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 必填列表
        /// </summary>
        public List<string>? Requireds { get; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; } = string.Empty;
        /// <summary>
        /// 格式
        /// </summary>
        public string Format { get; } = string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        public string CSharpType => Type.GetCSharpType(Format);
        /// <summary>
        /// 说明
        /// </summary>
        public string? Description { get; }
        /// <summary>
        /// 是枚举
        /// </summary>
        public bool IsEnum => EnumValues != null;
        /// <summary>
        /// 枚举值
        /// </summary>
        public List<int>? EnumValues { get; }
        /// <summary>
        /// 属性
        /// </summary>
        public List<PropertyModel> Properties { get; } = new();
        public SchemaModel(JProperty source)
        {
            Name = source.Name;
            foreach (JToken item in source)
            {
                if (item is not JObject subSource) continue;
                #region 处理基础信息
                Type = subSource["type"]?.ToString() ?? string.Empty;
                Format = subSource["format"]?.ToString() ?? string.Empty;
                Description = subSource["description"]?.ToString() ?? string.Empty;
                #endregion
                #region 处理枚举
                {
                    if(subSource["enum"] is JArray enums)
                    {
                        EnumValues = new();
                        foreach (JToken enumValue in enums)
                        {
                            EnumValues.Add(enumValue.ToString().ConvertTo<int>());
                        }
                    }
                }
                #endregion
                #region 处理必填
                {
                    if (subSource["required"] is JArray requireds)
                    {
                        Requireds = new();
                        foreach (JToken required in requireds)
                        {
                            if (required is not JValue jValue) continue;
                            Requireds.Add(jValue.ToString());
                        }
                    }
                }
                #endregion
                #region 处理字段
                {
                    if (subSource["properties"] is JObject properties)
                    {
                        foreach (KeyValuePair<string, JToken?> property in properties)
                        {
                            if (property.Value == null || property.Value is not JObject propertySource) continue;
                            Properties.Add(new(property.Key, propertySource, Requireds));
                        }
                    }
                }
                #endregion
            }
        }
    }
}
