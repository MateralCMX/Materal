﻿using Materal.Utils.Model;

namespace Materal.BusinessFlow.Abstractions.Services.Models.User
{
    public class QueryUserModel : BaseQueryModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string? Name { get; set; }
    }
}
