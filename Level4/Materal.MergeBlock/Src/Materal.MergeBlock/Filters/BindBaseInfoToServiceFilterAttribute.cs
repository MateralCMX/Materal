using Materal.MergeBlock.Abstractions.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Materal.MergeBlock.Filters
{
    /// <summary>
    /// 绑定基础信息到服务过滤器
    /// </summary>
    public class BindBaseInfoToServiceFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        /// <summary>
        /// 执行时
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
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
            object? serviceObj = memberInfo.GetValue(context.Controller);
            if (serviceObj is null || serviceObj is not IBaseService service) return;
            service.ClientIP = FilterHelper.GetIPAddress(context.HttpContext.Connection);
        }
    }
}
