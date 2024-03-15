using Materal.MergeBlock.Application.WebModule.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.HelloWorldTest.Controllers
{
    /// <summary>
    /// HelloWorldTest控制器
    /// </summary>
    [ApiExplorerSettings(GroupName = "HelloWorldTest")]
    [Route("HelloWorldTestAPI/[controller]/[action]")]
    public class HelloWorldTestController : MergeBlockControllerBase
    {
        /// <summary>
        /// 说Hello
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<string> SayHello() => ResultModel<string>.Success("Hello World", "获取成功");
    }
}
