using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Materal.Gateway.Pages
{
    /// <summary>
    /// 错误模型
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        /// <summary>
        /// 请求ID
        /// </summary>
        public string? RequestId { get; set; }
        /// <summary>
        /// 是否显示请求ID
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        /// <summary>
        /// GET请求
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}