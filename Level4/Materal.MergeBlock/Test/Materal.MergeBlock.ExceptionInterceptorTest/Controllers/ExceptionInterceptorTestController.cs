using Materal.MergeBlock.Application.WebModule.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.ExceptionInterceptorTest.Controllers
{
    /// <summary>
    /// ExceptionInterceptor���Կ�����
    /// </summary>
    public class ExceptionInterceptorTestController : MergeBlockControllerBase
    {
        /// <summary>
        /// ģ���쳣����
        /// </summary>
        /// <exception cref="TestException"></exception>
        [HttpGet]
        public void ModuleExceptionTest() => throw new TestException("����ģ���쳣");
        /// <summary>
        /// �쳣����
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public void ExceptionTest() => throw new NotImplementedException("�����쳣");
        /// <summary>
        /// �첽ģ���쳣����
        /// </summary>
        /// <returns></returns>
        /// <exception cref="TestException"></exception>
        [HttpGet]
        public Task AsyncModuleExceptionTestAsync() => throw new TestException("����ģ���쳣");
        /// <summary>
        /// �첽�쳣����
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public Task AsyncExceptionTestAsync() => throw new NotImplementedException("�����쳣");
    }
}
