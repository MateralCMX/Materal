using Microsoft.AspNetCore.Mvc;

namespace MMB.Demo.Application.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class MyController : ControllerBase
    {
        [HttpGet]
        public string Hello(string name) => $"Hello {name}!";
    }
}
