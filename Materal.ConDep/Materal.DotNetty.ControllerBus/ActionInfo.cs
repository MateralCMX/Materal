using System;
using System.Collections.Generic;
using System.Linq;
using DotNetty.Codecs.Http;
using Materal.DotNetty.ControllerBus.Attributes;
using System.Reflection;
using System.Threading.Tasks;
using Materal.DotNetty.ControllerBus.Filters;
using Materal.DotNetty.Common;

namespace Materal.DotNetty.ControllerBus
{
    public class ActionInfo
    {
        public BaseController BaseController { get; }
        public MethodInfo Action { get; }

        public ActionInfo(BaseController baseController, MethodInfo action)
        {
            Action = action;
            BaseController = baseController;
        }
        
        public string GetMethodName()
        {
            var attribute = Action.GetCustomAttribute<HttpMethodAttribute>();
            return attribute?.Name;
        }
        /// <summary>
        /// 处理HttpMethodAttribute
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IFullHttpResponse HandlerMethod(IFullHttpRequest request)
        {
            IFullHttpResponse response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.OK);
            var attribute = Action.GetCustomAttribute<HttpMethodAttribute>();
            attribute?.Handler(request, response);
            return response;
        }

        /// <summary>
        /// 处理Action过滤器
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="globalFilters"></param>
        /// <returns></returns>
        public async Task HandlerActionAfterFilterAsync(IFullHttpRequest request, IFullHttpResponse response, params IFilter[] globalFilters)
        {
            List<Attribute> attributes = Action.GetCustomAttributes().ToList();
            List<IActionAfterFilter> filters = attributes.OfType<IActionAfterFilter>().ToList();
            if (globalFilters != null && globalFilters.Length > 0)
            {
                filters.AddRange(globalFilters.OfType<IActionAfterFilter>());
            }
            foreach (IActionAfterFilter filter in filters)
            {
                filter.HandlerFilter(BaseController, this, request, ref response);
                if (response.Status.Code != HttpResponseStatus.OK.Code) return;
            }
            List<IActionAfterAsyncFilter> asyncFilters = attributes.OfType<IActionAfterAsyncFilter>().ToList();
            if (globalFilters != null && globalFilters.Length > 0)
            {
                asyncFilters.AddRange(globalFilters.OfType<IActionAfterAsyncFilter>());
            }
            foreach (IActionAfterAsyncFilter filter in asyncFilters)
            {
                await filter.HandlerFilterAsync(BaseController, this, request, ref response);
                if (response.Status.Code != HttpResponseStatus.OK.Code) return;
            }
        }

        /// <summary>
        /// 处理Action过滤器
        /// </summary>
        /// <param name="request"></param>
        /// <param name="globalFilters"></param>
        /// <returns></returns>
        public async Task<IFullHttpResponse> HandlerActionBeforeFilterAsync(IFullHttpRequest request, params IFilter[] globalFilters)
        {
            IFullHttpResponse response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.OK);
            List<Attribute> attributes = Action.GetCustomAttributes().ToList();
            List<IActionBeforeFilter> filters = attributes.OfType<IActionBeforeFilter>().ToList();
            if (globalFilters != null && globalFilters.Length > 0)
            {
                filters.AddRange(globalFilters.OfType<IActionBeforeFilter>());
            }
            foreach (IActionBeforeFilter filter in filters)
            {
                filter.HandlerFilter(BaseController, this, request, ref response);
                if (response.Status.Code != HttpResponseStatus.OK.Code) return response;
            }
            List<IActionBeforeAsyncFilter> asyncFilters = attributes.OfType<IActionBeforeAsyncFilter>().ToList();
            if (globalFilters != null && globalFilters.Length > 0)
            {
                asyncFilters.AddRange(globalFilters.OfType<IActionBeforeAsyncFilter>());
            }
            foreach (IActionBeforeAsyncFilter filter in asyncFilters)
            {
                await filter.HandlerFilterAsync(BaseController, this, request, ref response);
                if (response.Status.Code != HttpResponseStatus.OK.Code) return response;
            }
            return response;
        }
        /// <summary>
        /// 处理权限过滤器
        /// </summary>
        /// <param name="request"></param>
        /// <param name="globalFilters"></param>
        /// <returns></returns>
        public async Task<IFullHttpResponse> HandlerAuthorityFilterAsync(IFullHttpRequest request, params IFilter[] globalFilters)
        {
            Type controllerType = BaseController.GetType();
            IFullHttpResponse response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.OK);
            var allowAuthorityAttribute = Action.GetCustomAttribute<AllowAuthorityAttribute>();
            if (allowAuthorityAttribute != null) return response;
            allowAuthorityAttribute = controllerType.GetCustomAttribute<AllowAuthorityAttribute>();
            if (allowAuthorityAttribute != null) return response;

            List<Attribute> controllerAttributes = controllerType.GetCustomAttributes().ToList();
            List<Attribute> actionAttributes = Action.GetCustomAttributes().ToList();
            List<IAuthorityFilter> filters = actionAttributes.OfType<IAuthorityFilter>().ToList();
            filters.AddRange(controllerAttributes.OfType<IAuthorityFilter>());
            if (globalFilters != null && globalFilters.Length > 0)
            {
                filters.AddRange(globalFilters.OfType<IAuthorityFilter>());
            }
            foreach (IAuthorityFilter filter in filters)
            {
                filter.HandlerFilter(BaseController, this, request, ref response);
                if (response.Status.Code != HttpResponseStatus.OK.Code) return response;
            }
            List<IAuthorityAsyncFilter> asyncFilters = actionAttributes.OfType<IAuthorityAsyncFilter>().ToList();
            asyncFilters.AddRange(controllerAttributes.OfType<IAuthorityAsyncFilter>());
            if (globalFilters != null && globalFilters.Length > 0)
            {
                asyncFilters.AddRange(globalFilters.OfType<IAuthorityAsyncFilter>());
            }
            foreach (IAuthorityAsyncFilter filter in asyncFilters)
            {
                await filter.HandlerFilterAsync(BaseController, this, request, ref response);
                if (response.Status.Code != HttpResponseStatus.OK.Code) return response;
            }
            return response;
        }
    }
}
