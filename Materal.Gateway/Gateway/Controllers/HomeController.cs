using Common;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["OcelotConfig"] = ApplicationConfig.OcelotConfigModel;
            return View();
        }
    }
}