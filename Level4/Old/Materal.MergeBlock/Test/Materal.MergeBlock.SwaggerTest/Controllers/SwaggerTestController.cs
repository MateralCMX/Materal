using Materal.MergeBlock.Application.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Materal.MergeBlock.SwaggerTest.Controllers
{
    /// <summary>
    /// Swagger²âÊÔ¿ØÖÆÆ÷
    /// </summary>
    public class SwaggerTestController() : MergeBlockControllerBase
    {
        [HttpGet]
        public ResultModel HttpGetTest([FromQuery, Required] string name) => ResultModel.Success($"[HttpGet]Hello {name}!");
        [HttpPost]
        public ResultModel HttpPost([FromQuery, Required] string name, [FromBody, Required] TestRequestModel requestModel) => ResultModel.Success($"[HttpPost]Hello {name}!Hello {requestModel.Name}!");
        [HttpPut]
        public ResultModel HttpPut([FromQuery, Required] string name, [FromBody, Required] TestRequestModel requestModel) => ResultModel.Success($"[HttpPut]Hello {name}!Hello {requestModel.Name}!");
        [HttpDelete]
        public ResultModel HttpDelete([FromQuery, Required] string name) => ResultModel.Success($"[HttpDelete]Hello {name}!");
        [HttpPatch]
        public ResultModel HttpPatch([FromQuery, Required] string name, [FromBody, Required] TestRequestModel requestModel) => ResultModel.Success($"[HttpPatch]Hello {name}!Hello {requestModel.Name}!");
    }
}
