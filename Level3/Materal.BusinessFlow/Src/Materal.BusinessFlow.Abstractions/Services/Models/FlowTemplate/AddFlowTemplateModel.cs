﻿using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.Abstractions.Services.Models.FlowTemplate
{
    public class AddFlowTemplateModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(40)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 数据模型唯一标识
        /// </summary>
        [Required]
        public Guid DataModelID { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        [Required]
        public DataTypeEnum DataType { get; set; } = DataTypeEnum.String;
        /// <summary>
        /// 数据
        /// </summary>
        public string? Data { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}
