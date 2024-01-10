using Materal.MergeBlock.Application.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace MMB.Demo.Application.Controllers
{
    /// <summary>
    /// Swagger²âÊÔ¿ØÖÆÆ÷
    /// </summary>
    public class SwaggerTestController() : MergeBlockControllerBase
    {
        /// <summary>
        /// ËµHello
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel SayHello() => ResultModel.Success("Hello World!");
    }
}
