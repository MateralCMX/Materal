using Materal.MergeBlock.Application.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace MMB.Demo.Application.Controllers
{
    /// <summary>
    /// Swagger���Կ�����
    /// </summary>
    public class SwaggerTestController() : MergeBlockControllerBase
    {
        /// <summary>
        /// ˵Hello
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel SayHello() => ResultModel.Success("Hello World!");
    }
}
