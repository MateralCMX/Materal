using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using Materal.ConDep.Common;
using Materal.ConvertHelper;
using Materal.DotNetty.ControllerBus.Filters;
using Materal.Model;
using System;
using System.Text;

namespace Materal.ConDep.Controllers.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public IFullHttpResponse HandlerException(IByteBufferHolder byteBufferHolder, IFullHttpResponse response, Exception exception)
        {
            ConsoleHelper.ServerWriteError(exception);
            ResultModel resultModel = ResultModel.Fail("服务器错误，请联系后端工程师");
            byte[] bodyData = Encoding.UTF8.GetBytes(resultModel.ToJson());
            IByteBuffer byteBuffer = Unpooled.WrappedBuffer(bodyData);
            var result = new DefaultFullHttpResponse(HttpVersion.Http11, HttpResponseStatus.OK, byteBuffer);
            result.Headers.Set(HttpHeaderNames.Date, DateTime.Now);
            result.Headers.Set(HttpHeaderNames.Server, "MateralDotNettyServer");
            result.Headers.Set(HttpHeaderNames.AcceptLanguage, "zh-CN,zh;q=0.9");
            result.Headers.Set(HttpHeaderNames.ContentType, "application/json");
            return result;
        }
    }
}
