using DotNetty.Codecs.Http;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.DotNetty.ControllerBus.Filters;
using Materal.DotNetty.Server.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Materal.DotNetty.Common;

namespace Materal.DotNetty.ControllerBus
{
    public abstract class BaseController
    {
        public IFullHttpRequest Request { get; set; }
        public ActionInfo GetAction(string key)
        {
            Type controllerType = GetType();
            MethodInfo[] methodInfos = controllerType.GetMethods();
            foreach (MethodInfo methodInfo in methodInfos)
            {
                var routeAttribute = methodInfo.GetCustomAttribute<RouteAttribute>();
                if(routeAttribute != null && key == routeAttribute.Key ||
                    routeAttribute == null && key == methodInfo.Name)
                {
                    return new ActionInfo(this, methodInfo);
                }
            }
            throw new DotNettyServerException("未找到对应的Action");
        }

        /// <summary>
        /// 处理控制器过滤器
        /// </summary>
        /// <param name="response"></param>
        /// <param name="globalFilters"></param>
        /// <returns></returns>
        public async Task HandlerControllerAfterFilterAsync(IFullHttpResponse response, params IFilter[] globalFilters)
        {
            List<Attribute> attributes = GetType().GetCustomAttributes().ToList();
            List<IControllerAfterFilter> filters = attributes.OfType<IControllerAfterFilter>().ToList();
            if (globalFilters != null && globalFilters.Length > 0)
            {
                filters.AddRange(globalFilters.OfType<IControllerAfterFilter>());
            }
            foreach (IControllerAfterFilter filter in filters)
            {
                filter.HandlerFilter(this, Request, ref response);
                if (response.Status.Code != HttpResponseStatus.OK.Code) return;
            }
            List<IControllerAfterAsyncFilter> asyncFilters = attributes.OfType<IControllerAfterAsyncFilter>().ToList();
            if (globalFilters != null && globalFilters.Length > 0)
            {
                asyncFilters.AddRange(globalFilters.OfType<IControllerAfterAsyncFilter>());
            }
            foreach (IControllerAfterAsyncFilter filter in asyncFilters)
            {
                await filter.HandlerFilterAsync(this, Request, ref response);
                if (response.Status.Code != HttpResponseStatus.OK.Code) return;
            }
        }

        /// <summary>
        /// 处理控制器过滤器
        /// </summary>
        /// <param name="globalFilters"></param>
        /// <returns></returns>
        public async Task<IFullHttpResponse> HandlerControllerBeforeFilterAsync(params IFilter[] globalFilters)
        {
            IFullHttpResponse response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.OK);
            List<Attribute> attributes = GetType().GetCustomAttributes().ToList();
            List<IControllerBeforeFilter> filters = attributes.OfType<IControllerBeforeFilter>().ToList();
            if (globalFilters != null && globalFilters.Length > 0)
            {
                filters.AddRange(globalFilters.OfType<IControllerBeforeFilter>());
            }
            foreach (IControllerBeforeFilter filter in filters)
            {
                filter.HandlerFilter(this, Request, ref response);
                if (response.Status.Code != HttpResponseStatus.OK.Code) return response;
            }
            List<IControllerBeforeAsyncFilter> asyncFilters = attributes.OfType<IControllerBeforeAsyncFilter>().ToList();
            if (globalFilters != null && globalFilters.Length > 0)
            {
                asyncFilters.AddRange(globalFilters.OfType<IControllerBeforeAsyncFilter>());
            }
            foreach (IControllerBeforeAsyncFilter filter in asyncFilters)
            {
                await filter.HandlerFilterAsync(this, Request, ref response);
                if (response.Status.Code != HttpResponseStatus.OK.Code) return response;
            }
            return response;
        }
    }
}
