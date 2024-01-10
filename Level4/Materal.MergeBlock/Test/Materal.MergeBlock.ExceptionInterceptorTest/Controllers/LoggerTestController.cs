using Materal.MergeBlock.Application.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.ExceptionInterceptorTest.Controllers
{
    /// <summary>
    /// ExceptionInterceptor���Կ�����
    /// </summary>
    public class ExceptionInterceptorTestController : MergeBlockControllerBase
    {
        [HttpGet]
        public void ModuleExceptionTest() => throw new TestException("����ģ���쳣");
        [HttpGet]
        public void ExceptionTest() => throw new NotImplementedException("�����쳣");
        [HttpGet]
        public Task AsyncModuleExceptionTestAsync() => throw new TestException("����ģ���쳣");
        [HttpGet]
        public Task AsyncExceptionTestAsync() => throw new NotImplementedException("�����쳣");
    }
}
