using Materal.StringHelper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace Materal.APP.WebAPICore
{
    /// <summary>
    /// WebAPIControllerBase
    /// </summary>
    public class WebAPIControllerBase : ControllerBase
    {
        /// <summary>
        /// 获得登录用户唯一标识
        /// </summary>
        /// <returns></returns>
        protected Guid GetLoginUserID()
        {
            Claim claim = User.Claims.FirstOrDefault(m => m.Type == "UserID");
            if (claim == null || !claim.Value.IsGuid()) throw new InvalidOperationException("未登录");
            Guid userID = Guid.Parse(claim.Value);
            return userID;
        }
    }
}
