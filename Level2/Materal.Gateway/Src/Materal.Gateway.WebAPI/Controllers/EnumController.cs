using Materal.Gateway.Service.Models.Sync;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.Gateway.WebAPI.Controllers
{
    /// <summary>
    /// 枚举控制器
    /// </summary>
    public class EnumController : GatewayControllerBase
    {
        /// <summary>
        /// 获取所有HttpVersion枚举
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<List<KeyValueModel<HttpVersionEnum>>> GetAllHttpVersion() => ResultModel<List<KeyValueModel<HttpVersionEnum>>>.Success(KeyValueModel<HttpVersionEnum>.GetAllCode(), "获取成功");
        /// <summary>
        /// 获取所有SchemeType枚举
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<List<KeyValueModel<SchemeTypeEnum>>> GetAllSchemeType() => ResultModel<List<KeyValueModel<SchemeTypeEnum>>>.Success(KeyValueModel<SchemeTypeEnum>.GetAllCode(), "获取成功");
        /// <summary>
        /// 获取所有SyncMode枚举
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<List<KeyValueModel<SyncModeEnum>>> GetAllSyncMode() => ResultModel<List<KeyValueModel<SyncModeEnum>>>.Success(KeyValueModel<SyncModeEnum>.GetAllCode(), "获取成功");
    }
}
