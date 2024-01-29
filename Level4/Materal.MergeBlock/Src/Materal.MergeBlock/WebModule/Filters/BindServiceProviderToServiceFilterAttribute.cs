//using Materal.MergeBlock.Abstractions.WebModule.Controllers;
//using Microsoft.AspNetCore.Mvc.Filters;

//namespace Materal.MergeBlock.WebModule.Filters
//{
//    /// <summary>
//    /// 绑定服务提供者到服务过滤器
//    /// </summary>
//    public class BindServiceProviderToServiceFilterAttribute(IServiceProvider serviceProvider) : ActionFilterAttribute, IActionFilter
//    {
//        /// <summary>
//        /// 执行时
//        /// </summary>
//        /// <param name="context"></param>
//        public override void OnActionExecuting(ActionExecutingContext context)
//        {
//            if (context.Controller is IMergeBlockControllerBase mergeBlockController)
//            {
//                mergeBlockController.ServiceProvider = serviceProvider;
//            }
//            IEnumerable<FieldInfo> fieldInfos = context.Controller.GetType().GetRuntimeFields().Where(m => m.FieldType.IsAssignableTo<IBaseService>());
//            foreach (FieldInfo fieldInfo in fieldInfos)
//            {
//                BindServiceProvider(fieldInfo, context);
//            }
//            IEnumerable<PropertyInfo> propertyInfos = context.Controller.GetType().GetRuntimeProperties().Where(m => m.PropertyType.IsAssignableTo<IBaseService>());
//            foreach (PropertyInfo propertyInfo in propertyInfos)
//            {
//                BindServiceProvider(propertyInfo, context);
//            }
//        }
//        /// <summary>
//        /// 绑定服务提供者
//        /// </summary>
//        /// <param name="memberInfo"></param>
//        /// <param name="context"></param>
//        private void BindServiceProvider(MemberInfo memberInfo, ActionExecutingContext context)
//        {
//            object? serviceObj = memberInfo.GetValue(context.Controller);
//            if (serviceObj is null || serviceObj is not IBaseService service) return;
//            service.ServiceProvider = serviceProvider;
//        }
//    }
//}