using Microsoft.AspNetCore.Mvc;

namespace Demo.WebUI.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}