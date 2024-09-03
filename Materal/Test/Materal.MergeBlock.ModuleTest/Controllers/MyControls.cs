using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.ModuleTest.Controls
{
    [Route("/api/[controller]/[action]")]
    public class MyControllers : ControllerBase
    {
        [HttpGet]
        public string Hello(string name) => $"Hello {name}!";
    }
}
