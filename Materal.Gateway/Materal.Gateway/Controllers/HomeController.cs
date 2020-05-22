using Materal.Gateway.Models;
using Materal.Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Materal.Gateway.Controllers
{
    /// <summary>
    /// 主页
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ConfigService _configService;
        /// <summary>
        /// 主页
        /// </summary>
        public HomeController(ConfigService configService)
        {
            _configService = configService;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(string searchKey)
        {
            List<ConfigItemModel> items = _configService.GetConfigItems(searchKey);
            return View(items);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit(Guid id)
        {
            ConfigItemModel configItem = id == Guid.Empty ? new ConfigItemModel() : _configService.GetConfigItem(id);
            return View(configItem);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="configItem"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save(ConfigItemModel configItem)
        {
            configItem.EnableCache = HttpContext.Request.Form.ContainsKey("enableCache") &&
                                     HttpContext.Request.Form["enableCache"] == "on";
            try
            {
                if (configItem.ID == Guid.Empty)
                {
                    await _configService.AddItemAsync(configItem);
                }
                else
                {
                    await _configService.SaveItemAsync(configItem);
                }
                return Redirect("/Home/Index");
            }
            catch (Exception exception)
            {
                var url = "/Home/Edit";
                if (configItem.ID != Guid.Empty)
                {
                    url += $"/{configItem.ID}";
                }
                return Message(exception.Message, url);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(Guid id)
        {
            await _configService.DeleteItemAsync(id);
            return Redirect("/Home/Index");
        }
        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public IActionResult Message(string message, string url)
        {
            ViewData["message"] = message;
            ViewData["url"] = url;
            return View("Message");
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
