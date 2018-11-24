using Materal.ApplicationUpdate.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Materal.ApplicationUpdate.WebAPI.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// 主页控制器
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 关于
        /// </summary>
        /// <returns></returns>
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        /// <summary>
        /// 明细
        /// </summary>
        /// <returns></returns>
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        /// <summary>
        /// Privacy
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// Error
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
