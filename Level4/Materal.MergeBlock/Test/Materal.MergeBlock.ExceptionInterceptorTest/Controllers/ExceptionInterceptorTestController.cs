using Materal.MergeBlock.Application.WebModule.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.ExceptionInterceptorTest.Controllers
{
    /// <summary>
    /// ExceptionInterceptor测试控制器
    /// </summary>
    public class ExceptionInterceptorTestController : MergeBlockControllerBase
    {
        /// <summary>
        /// 模块异常测试
        /// </summary>
        /// <exception cref="TestException"></exception>
        [HttpGet]
        public void ModuleExceptionTest() => throw new TestException("测试模块异常");
        /// <summary>
        /// 异常测试
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public void ExceptionTest() => throw new NotImplementedException("测试异常");
        /// <summary>
        /// 异步模块异常测试
        /// </summary>
        /// <returns></returns>
        /// <exception cref="TestException"></exception>
        [HttpGet]
        public Task AsyncModuleExceptionTestAsync() => throw new TestException("测试模块异常");
        /// <summary>
        /// 异步异常测试
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public Task AsyncExceptionTestAsync() => throw new NotImplementedException("测试异常");
    }
}
