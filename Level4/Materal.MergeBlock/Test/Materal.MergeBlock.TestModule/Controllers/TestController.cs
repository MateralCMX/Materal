using Materal.MergeBlock.Application.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace MMB.Demo.Application.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    public class TestController() : MergeBlockControllerBase
    {
        /// <summary>
        /// 说Hello
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel SayHello() => ResultModel.Success("Hello World!");
        /// <summary>
        /// 抛出系统异常
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel ThrowSystemException() => throw new NotImplementedException("测试异常");
        /// <summary>
        /// 抛出模块业务异常
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel ThrowModuleException() => throw new MergeBlockModuleException("测试异常");
    }
}
