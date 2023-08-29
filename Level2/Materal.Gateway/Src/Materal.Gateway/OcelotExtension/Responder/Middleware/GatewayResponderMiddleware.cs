using Microsoft.AspNetCore.Http;
using Ocelot.DownstreamRouteFinder.Finder;
using Ocelot.Errors;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Request.Mapper;
using Ocelot.Responder;
using System.Net.Http;

namespace Materal.Gateway.OcelotExtension.Responder.Middleware
{
    /// <summary>
    /// ������Ӧ�м��
    /// </summary>
    public class GatewayResponderMiddleware : OcelotMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpResponder _responder;
        private readonly IErrorsToHttpStatusCodeMapper _codeMapper;
        /// <summary>
        /// ���췽��
        /// </summary>
        /// <param name="next"></param>
        /// <param name="responder"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="codeMapper"></param>
        public GatewayResponderMiddleware(RequestDelegate next, IHttpResponder responder, IOcelotLoggerFactory loggerFactory, IErrorsToHttpStatusCodeMapper codeMapper) : base(loggerFactory.CreateLogger<GatewayResponderMiddleware>())
        {
            _next = next;
            _responder = responder;
            _codeMapper = codeMapper;
        }
        /// <summary>
        /// �м��ִ��
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            List<Error> errors = new();
            try
            {
                await _next(httpContext);
                errors = httpContext.Items.Errors();
            }
            catch (Exception ex)
            {
                errors.Add(new UnmappableRequestError(ex));
            }
            finally
            {
                if (errors.Count > 0)
                {
                    SetErrorResponse(httpContext, errors);
                }
                else
                {
                    DownstreamResponse downstreamResponse = httpContext.Items.DownstreamResponse();
                    await _responder.SetResponseOnHttpContext(httpContext, downstreamResponse);
                }
            }
        }
        /// <summary>
        /// ���ô�����Ӧ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="errors"></param>
        private void SetErrorResponse(HttpContext context, List<Error> errors)
        {
            int statusCode = _codeMapper.Map(errors);
            _responder.SetErrorResponseOnContext(context, statusCode);
            if (errors.Any(e => e.Code == OcelotErrorCode.QuotaExceededError))
            {
                DownstreamResponse downstreamResponse = context.Items.DownstreamResponse();
                _responder.SetErrorResponseOnContext(context, downstreamResponse);
            }
        }
    }
}
