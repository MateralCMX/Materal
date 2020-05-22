using Common;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(ApplicationConfig.OcelotConfigModel);
        }
    }
}