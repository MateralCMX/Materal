using Materal.MergeBlock.Application.WebModule.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.HelloWorldTest.Controllers
{
    /// <summary>
    /// HelloWorld控制器
    /// </summary>
    [Route("HelloWorld/api/[controller]/[action]")]
    public class HelloWorldController : MergeBlockControllerBase
    {
        /// <summary>
        /// 说Hello
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<string> SayHello() => ResultModel<string>.Success("Hello World", "获取成功");
    }
}
