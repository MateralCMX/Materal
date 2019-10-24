using Materal.MicroFront.Common;
using Materal.Model;
using Materal.WebSocket.Http.Attributes;
using System;
using System.Reflection;

namespace Materal.MicroFront.Controllers
{
    public class SystemController
    {
        /// <summary>
        /// 获取系统名称
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public ResultModel<string> GetSystemName()
        {
            try
            {
                string name = ApplicationConfig.SystemName;
                return ResultModel<string>.Success(name, "获取成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<string>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获取系统版本
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public ResultModel<string> GetSystemVersion()
        {
            try
            {
                string version = Assembly.Load("Materal.MicroFront").GetName().Version.ToString();
                return ResultModel<string>.Success(version, "获取成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<string>.Fail(ex.Message);
            }
        }
    }
}
