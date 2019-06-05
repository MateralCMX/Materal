using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using Authority.Service;

namespace Authority.PresentationModel
{
    /// <summary>
    /// 权限拦截器
    /// </summary>
    public class AuthorityFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 依赖注入服务
        /// </summary>
        public static IServiceProvider ServiceProvider { get; set; }
        /// <summary>
        /// 权限代码
        /// </summary>
        private readonly string[] _codes;
        private readonly IUserService _userService;
        private readonly IAPIAuthorityService _apiAuthorityService;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="codes"></param>
        public AuthorityFilter(params string[] codes)
        {
            _codes = codes;
            if (ServiceProvider == null) return;
            _userService = (IUserService)ServiceProvider.GetService(typeof(IUserService));
            _apiAuthorityService = (IAPIAuthorityService)ServiceProvider.GetService(typeof(IAPIAuthorityService));
        }
        /// <summary>
        /// 方法执行之前
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await base.OnActionExecutionAsync(context, next);
            //IHeaderDictionary header = context.HttpContext.Request.Headers;
            //if (header.ContainsKey("Authorization"))
            //{
            //    string[] values = header.GetCommaSeparatedValues("Authorization");
            //    string token = string.Empty;
            //    foreach (string value in values)
            //    {
            //        string[] tempValue = value.Split(' ');
            //        if (tempValue.Length == 2 && tempValue[0] == "Bearer")
            //        {
            //            token = tempValue[1];
            //            break;
            //        }
            //    }
            //    if (string.IsNullOrEmpty(token))
            //    {
            //        context.HttpContext.Response.StatusCode = 403;
            //        return;
            //    }
            //    Guid userID = _userService.GetUserID(token);
            //    if (await _apiAuthorityService.HasAPIAuthorityAsync(userID, _codes))
            //    {
            //        await base.OnActionExecutionAsync(context, next);
            //    }
            //    else
            //    {
            //        context.HttpContext.Response.StatusCode = 403;
            //    }
            //}
            //else
            //{
            //    context.HttpContext.Response.StatusCode = 403;
            //}
        }
    }
}
