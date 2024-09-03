using Materal.MergeBlock.Web.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MMB.Demo.Application.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class MyController : ControllerBase, IMergeBlockController
    {
        [HttpGet]
        public string Hello(string name) => $"Hello {name}!";
    }
}
