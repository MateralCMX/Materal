using Microsoft.AspNetCore.Http;
using RC.Deploy.Abstractions.Services.Models;

namespace RC.Deploy.Application
{
    /// <summary>
    /// Deploy中间件
    /// </summary>
    public class DeployMiddleware(RequestDelegate next)
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (!httpContext.Request.Path.HasValue || string.IsNullOrWhiteSpace(httpContext.Request.Path.Value)) return;
            IApplicationInfoService applicationInfoService = httpContext.RequestServices.GetRequiredService<IApplicationInfoService>();
            string[] paths = httpContext.Request.Path.Value.Split("/");
            IApplicationRuntimeModel? applicationRuntimeModel = applicationInfoService.GetApplicationRuntimeModel(paths[1]);
            if (applicationRuntimeModel is null ||
                applicationRuntimeModel.ApplicationInfo.ApplicationType != ApplicationTypeEnum.StaticDocument ||
                applicationRuntimeModel.ApplicationStatus == ApplicationStatusEnum.Runing)
            {
                if (applicationRuntimeModel is not null && applicationRuntimeModel.ApplicationInfo.ApplicationType == ApplicationTypeEnum.StaticDocument)
                {
                    if (paths.Length == 3 && string.IsNullOrWhiteSpace(paths[2]))
                    {
                        paths[2] = "Index.html";
                        httpContext.Request.Path = string.Join('/', paths);
                    }
                    else if (paths.Length == 2)
                    {
                        httpContext.Request.Path = string.Join('/', paths) + "/Index.html";
                    }
                }
                await next(httpContext);
            }
            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }
    }
}
