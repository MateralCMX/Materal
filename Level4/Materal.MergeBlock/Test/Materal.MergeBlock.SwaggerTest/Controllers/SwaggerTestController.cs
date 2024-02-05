using Materal.MergeBlock.Application.WebModule.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Materal.MergeBlock.SwaggerTest.Controllers
{
    /// <summary>
    /// Swagger≤‚ ‘øÿ÷∆∆˜
    /// </summary>
    public class SwaggerTestController() : MergeBlockControllerBase
    {
        /// <summary>
        /// HttpGet≤‚ ‘
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultModel HttpGetTest([FromQuery, Required] string name) => ResultModel.Success($"[HttpGet]Hello {name}!");
        /// <summary>
        /// HttpPost≤‚ ‘
        /// </summary>
        /// <param name="name"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel HttpPost([FromQuery, Required] string name, [FromBody, Required] TestRequestModel requestModel) => ResultModel.Success($"[HttpPost]Hello {name}!Hello {requestModel.Name}!");
        /// <summary>
        /// HttpPut≤‚ ‘
        /// </summary>
        /// <param name="name"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public ResultModel HttpPut([FromQuery, Required] string name, [FromBody, Required] TestRequestModel requestModel) => ResultModel.Success($"[HttpPut]Hello {name}!Hello {requestModel.Name}!");
        /// <summary>
        /// HttpDelete≤‚ ‘
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpDelete]
        public ResultModel HttpDelete([FromQuery, Required] string name) => ResultModel.Success($"[HttpDelete]Hello {name}!");
        /// <summary>
        /// HttpPatch≤‚ ‘
        /// </summary>
        /// <param name="name"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPatch]
        public ResultModel HttpPatch([FromQuery, Required] string name, [FromBody, Required] TestRequestModel requestModel) => ResultModel.Success($"[HttpPatch]Hello {name}!Hello {requestModel.Name}!");
    }
}
