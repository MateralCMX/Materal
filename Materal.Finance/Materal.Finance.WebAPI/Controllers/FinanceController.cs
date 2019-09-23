using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.Finance.WebAPI.Models;
using Materal.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.Finance.WebAPI.Controllers
{
    [Route("api/[controller]/[action]"), ApiController]
    public class FinanceController : ControllerBase
    {
        public async Task<ResultModel<List<ChangTouTemperature>>> GetChangTouTemperature()
        {
            return null;
        }
    }
}