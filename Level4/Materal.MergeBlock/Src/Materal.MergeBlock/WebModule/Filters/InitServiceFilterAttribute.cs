using Materal.MergeBlock.Abstractions.Services;
using Materal.MergeBlock.Abstractions.WebModule.Authorization.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Materal.MergeBlock.WebModule.Filters
{
    /// <summary>
    /// 初始化服务过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class InitServiceFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action执行前
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(context.Controller is ControllerBase controller)
            {
                Type controllerType = controller.GetType();
                foreach (MemberInfo memberInfo in controllerType.GetRuntimeFields().Where(m => m.FieldType.IsAssignableTo<IBaseService>()))
                {
                    object? serviceObj = memberInfo.GetValue(controller);
                    if (serviceObj is null || serviceObj is not IBaseService service) continue;
                    service.LoginUserID = controller.GetLoginUserID();
                }
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
