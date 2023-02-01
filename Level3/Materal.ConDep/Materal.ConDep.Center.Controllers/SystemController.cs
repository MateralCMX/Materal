using Materal.ConDep.Center.Controllers.Models;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;
using System;
using System.Reflection;

namespace Materal.ConDep.Center.Controllers
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
                Version version = Assembly.Load("Materal.ConDep.Center.Server").GetName().Version;
                var result = new SystemInfo
                {
                    Name = "Materal-ConDep-控制中心",
                    Version = version?.ToString()
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
