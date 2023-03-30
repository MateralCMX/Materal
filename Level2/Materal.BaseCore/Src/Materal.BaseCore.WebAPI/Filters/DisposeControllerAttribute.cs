using Microsoft.AspNetCore.Mvc.Filters;

namespace Materal.BaseCore.WebAPI.Filters
{
    public class DisposeControllerAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
            if(context.Controller is IDisposable controller) controller.Dispose();
        }
    }
}
