﻿using Materal.StringHelper;
using System.Text.Json;

namespace RC.Core.WebAPI
{
    /// <summary>
    /// 首字母大写Policy
    /// </summary>
    public class FirstUpperNamingPolicy : JsonNamingPolicy
    {
        /// <summary>
        /// 转换名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string ConvertName(string name)
        {
            return name.FirstUpper();
        }
    }
}
