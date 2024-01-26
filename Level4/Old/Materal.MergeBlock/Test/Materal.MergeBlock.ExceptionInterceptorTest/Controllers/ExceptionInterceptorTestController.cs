using Materal.MergeBlock.Application.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.ExceptionInterceptorTest.Controllers
{
    /// <summary>
    /// ExceptionInterceptor≤‚ ‘øÿ÷∆∆˜
    /// </summary>
    public class ExceptionInterceptorTestController : MergeBlockControllerBase
    {
        [HttpGet]
        public void ModuleExceptionTest() => throw new TestException("≤‚ ‘ƒ£øÈ“Ï≥£");
        [HttpGet]
        public void ExceptionTest() => throw new NotImplementedException("≤‚ ‘“Ï≥£");
        [HttpGet]
        public Task AsyncModuleExceptionTestAsync() => throw new TestException("≤‚ ‘ƒ£øÈ“Ï≥£");
        [HttpGet]
        public Task AsyncExceptionTestAsync() => throw new NotImplementedException("≤‚ ‘“Ï≥£");
    }
}
