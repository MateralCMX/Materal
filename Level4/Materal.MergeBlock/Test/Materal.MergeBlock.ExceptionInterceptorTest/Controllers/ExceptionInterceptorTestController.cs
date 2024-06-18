using Materal.MergeBlock.Abstractions;
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
        [HttpGet]
        public void ModuleExceptionTest() => throw new TestException("测试模块异常");
        /// <summary>
        /// 异常测试
        /// </summary>
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
        [HttpGet]
        public Task AsyncExceptionTestAsync() => throw new NotImplementedException("测试异常");
        /// <summary>
        /// 400异常测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task BadRequestExceptionTestAsync() => throw new BadRequestException("测试异常");
        /// <summary>
        /// 401异常测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task UnauthorizedExceptionTestAsync() => throw new UnauthorizedException("测试异常");
        /// <summary>
        /// 404异常测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task NotFindExceptionTestAsync() => throw new HttpCodeException(404, "测试异常");
    }
}
