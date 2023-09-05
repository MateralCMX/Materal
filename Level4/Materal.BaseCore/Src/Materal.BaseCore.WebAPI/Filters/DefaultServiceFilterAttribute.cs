using Materal.BaseCore.Common.ConfigModels;
using Materal.BaseCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Security.Claims;

namespace Materal.BaseCore.WebAPI.Filters
{
    public class DefaultServiceFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is not ControllerBase controller) return;
            FieldInfo? fieldInfo = context.Controller.GetType().GetRuntimeFields().FirstOrDefault(m => m.Name == "DefaultService");
            if (fieldInfo is null) return;
            object? serviceObj = fieldInfo.GetValue(context.Controller);
            if(serviceObj is null || serviceObj is not IBaseService service) return;
            service.ClientIP = FilterHelper.GetIPAddress(context.HttpContext.Connection);
            if (controller.User.Claims != null)
            {
                Claim? claim = controller.User.Claims.FirstOrDefault(m => m.Type == JWTConfigModel.UserIDKey);
                if (claim != null && claim.Value.IsGuid())
                {
                    service.LoginUserID = Guid.Parse(claim.Value);
                }
                claim = controller.User.Claims.FirstOrDefault(m => m.Type == JWTConfigModel.ServerNameKey);
                if (claim != null && !string.IsNullOrWhiteSpace(claim.Value))
                {
                    service.LoginServiceName = claim.Value;
                }
            }
        }
    }
}
