using System;
using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using Materal.ConfigCenter.ProtalServer.Common;
using Materal.ConvertHelper;
using Materal.DotNetty.Common;
using Materal.DotNetty.ControllerBus.Filters;
using Materal.Model;

namespace Materal.ConfigCenter.ProtalServer.Controllers.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public IFullHttpResponse HandlerException(IByteBufferHolder byteBufferHolder, Exception exception)
        {
            ConsoleHelper.ServerWriteError(exception);
            ResultModel resultModel = ResultModel.Fail("服务器错误，请联系后端工程师");
            return HttpResponseHelper.GetHttpResponse(HttpResponseStatus.OK, resultModel.ToJson());
        }
    }
}
