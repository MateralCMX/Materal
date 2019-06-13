using Microsoft.AspNetCore.Mvc;

namespace WeChatService.WebAPI.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// 微信公众号控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class WeChatPublicNumberController : ControllerBase
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public WeChatPublicNumberController()
        {
        }
    }
}
