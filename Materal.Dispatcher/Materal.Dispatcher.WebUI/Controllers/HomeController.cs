using System.Web.Mvc;

namespace Materal.Dispatcher.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}