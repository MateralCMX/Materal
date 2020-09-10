﻿using Materal.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.PresentationModel.Namespace
{
    /// <summary>
    /// 查询命名空间过滤器请求模型
    /// </summary>
    public class QueryNamespaceFilterRequestModel : FilterModel
    {
        /// <summary>
        /// 所属项目唯一标识
        /// </summary>
        [Required(ErrorMessage = "所属项目唯一标识不能为空")]
        public Guid ProjectID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
