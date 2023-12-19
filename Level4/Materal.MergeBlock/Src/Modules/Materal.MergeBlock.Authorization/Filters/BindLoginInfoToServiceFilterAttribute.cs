using Materal.MergeBlock.Authorization.Abstractions.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Materal.MergeBlock.Authorization.Filters
{
    /// <summary>
    /// 绑定登录信息到服务过滤器
    /// </summary>
    public class BindLoginInfoToServiceFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        /// <summary>
        /// 执行时
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is not ControllerBase) return;
            IEnumerable<FieldInfo> fieldInfos = context.Controller.GetType().GetRuntimeFields().Where(m => m.FieldType.IsAssignableTo<IBaseService>());
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                BindLoginInfo(fieldInfo, context);
            }
            IEnumerable<PropertyInfo> propertyInfos = context.Controller.GetType().GetRuntimeProperties().Where(m => m.PropertyType.IsAssignableTo<IBaseService>());
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                BindLoginInfo(propertyInfo, context);
            }
        }
        /// <summary>
        /// 绑定登录信息
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="context"></param>
        private static void BindLoginInfo(MemberInfo memberInfo, ActionExecutingContext context)
        {
            if (context.Controller is not ControllerBase controller) return;
            object? serviceObj = memberInfo.GetValue(context.Controller);
            if (serviceObj is null || serviceObj is not IBaseService service) return;
            if (controller.IsUserLogin())
            {
                service.LoginUserID = controller.GetLoginUserID();
            }
            if (controller.IsServerLogin())
            {
                service.LoginServiceName = controller.GetLoginServerName();
            }
        }
    }
}
