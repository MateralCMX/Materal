using Materal.ConDep.Common;
using Materal.ConDep.Controllers.Models;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;
using System;
using System.Reflection;
using Materal.ConDep.ControllerCore;

namespace Materal.ConDep.Controllers
{
    public class SystemController : ConDepBaseController
    {
        /// <summary>
        /// 获取系统信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAuthority]
        public ResultModel<SystemInfo> GetSystemInfo()
        {
            try
            {
                var result = new SystemInfo
                {
                    Name = ApplicationConfig.SystemName,
                    Version = Assembly.Load("Materal.ConDep.Server").GetName().Version.ToString()
                };
                return ResultModel<SystemInfo>.Success(result, "获取成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<SystemInfo>.Fail(ex.Message);
            }
        }
    }
}
