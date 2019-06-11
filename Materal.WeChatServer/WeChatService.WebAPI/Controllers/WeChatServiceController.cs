using Authority.PresentationModel;
using Common.Model.APIAuthorityConfig;
using Materal.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using WeChatService.PresentationModel.WeChatDomain.Request;
using WeChatService.Service.Model.WeChatDomain;

namespace WeChatService.WebAPI.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// 微信域名控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class WeChatServiceController : ControllerBase
    {
        /// <summary>
        /// 日志对象
        /// </summary>
        private readonly ILogger<WeChatServiceController> _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        public WeChatServiceController(ILogger<WeChatServiceController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 验证域名
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool GetAccessToken()
        {
        }
    }
}
