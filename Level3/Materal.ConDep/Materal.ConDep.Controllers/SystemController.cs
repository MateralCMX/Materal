using Materal.ConDep.Controllers.Models;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.FileHelper;
using Materal.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Materal.ConDep.Controllers
{
    public class SystemController : ConDepBaseController
    {
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
