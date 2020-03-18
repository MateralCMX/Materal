using System;
using System.Reflection;
using Materal.ConfigCenter.ControllerCore;
using Materal.ConfigCenter.ProtalServer.Common;
using Materal.ConfigCenter.ProtalServer.Controllers.Models;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;

namespace Materal.ConfigCenter.ProtalServer.Controllers
{
    public class SystemController : ConfigCenterBaseController
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
                    Version = Assembly.Load("Materal.ConfigCenter.ProtalServer").GetName().Version.ToString()
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
