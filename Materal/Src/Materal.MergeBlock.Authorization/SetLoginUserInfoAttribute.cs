using Materal.MergeBlock.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace Materal.MergeBlock.Authorization
{
    /// <summary>
    /// 初始化服务过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SetLoginUserInfoAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action执行前
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is ControllerBase controller)
            {
                Type controllerType = controller.GetType();
                foreach (MemberInfo memberInfo in controllerType.GetRuntimeProperties().Where(m => m.PropertyType.IsAssignableTo<IBaseService>()))
                {
                    SetLoginUserInfo(memberInfo, controller);
                }
                foreach (MemberInfo memberInfo in controllerType.GetRuntimeFields().Where(m => m.FieldType.IsAssignableTo<IBaseService>()))
                {
                    SetLoginUserInfo(memberInfo, controller);
                }
            }
            await base.OnActionExecutionAsync(context, next);
        }
        /// <summary>
        /// 绑定服务
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="controller"></param>
        private static void SetLoginUserInfo(MemberInfo memberInfo, ControllerBase controller)
        {
            object? serviceObj = memberInfo.GetValue(controller);
            if (serviceObj is null || serviceObj is not IBaseService service) return;
            try
            {
                service.LoginUserID = controller.GetLoginUserID();
            }
            catch
            {
                // ignored
            }
        }
    }
}
