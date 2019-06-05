﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Authority.DataTransmitModel.User
{
    /// <summary>
    /// 用户所属角色
    /// </summary>
    public class UserRoleTreeDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 子级
        /// </summary>
        public List<UserRoleTreeDTO> Child { get; set; }
        /// <summary>
        /// 拥有标识
        /// </summary>
        public bool Owned { get; set; }
    }
}
