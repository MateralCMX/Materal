using Microsoft.AspNetCore.Mvc.Filters;

namespace Materal.MergeBlock.Web
{
    /// <summary>
    /// 分页查询过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ActionPageQueryFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action执行前
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            List<PageRequestModel?> pageRequestModels = context.ActionArguments.Where(m => m.Value is PageRequestModel).Select(m => m.Value as PageRequestModel).ToList();
            if (pageRequestModels.Count > 0)
            {
                foreach (PageRequestModel? item in pageRequestModels)
                {
                    if (item is null) continue;
                    if (item.PageIndex < PageRequestModel.PageStartNumber)
                    {
                        item.PageIndex = PageRequestModel.PageStartNumber;
                    }
                    if (item.PageSize <= 0)
                    {
                        item.PageSize = 10;
                    }
                }
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
