using Materal.ConDep.Common;
using Materal.ConDep.ControllerCore;
using Materal.ConDep.Controllers.Models;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Materal.FileHelper;

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
                Version version = Assembly.Load("Materal.ConDep.Server").GetName().Version;
                var result = new SystemInfo
                {
                    Name = ApplicationConfig.SystemName,
                    Version = version?.ToString()
                };
                return ResultModel<SystemInfo>.Success(result, "获取成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<SystemInfo>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 获取默认数据
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAuthority]
        public ResultModel<string> GetDefaultData()
        {
            string result = null;
            string filePath = GetDefaultDataFilePath();
            if (File.Exists(filePath))
            {
                result = File.ReadAllText(filePath);
            }
            return ResultModel<string>.Success(result?.Trim(), "获取成功");
        }
        /// <summary>
        /// 设置默认数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> SetDefaultData(SetDefaultDataModel model)
        {
            string filePath = GetDefaultDataFilePath();
            await TextFileManager.WriteTextAsync(filePath, model.Data);
            return ResultModel.Success("设置成功");
        }

        #region 私有方法

        private string GetDefaultDataFilePath()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? string.Empty, "DefaultData.data");
            return filePath;
        }
        #endregion
    }
}
