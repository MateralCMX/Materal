using Materal.Gateway.WebAPI.Services;
using Materal.Gateway.WebAPI.Services.Models;
using Materal.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.Gateway.WebAPI.Controllers
{
    [Route("api/[controller]/[action]"), ApiController]
    public class SchemaController : BaseController
    {
        [HttpGet]
        public async Task<ResultModel<string>> GetSchemaAsync()
        {
        }
    }
}
