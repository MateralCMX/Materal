using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using Materal.ConDep.Common;
using Materal.ConvertHelper;
using Materal.DotNetty.Common;
using Materal.DotNetty.ControllerBus.Filters;
using Materal.Model;
using System;
using System.Text;

namespace Materal.ConDep.Controllers.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public IFullHttpResponse HandlerException(IByteBufferHolder byteBufferHolder, Exception exception)
        {
            ConsoleHelper.ServerWriteError(exception);
            ResultModel resultModel = ResultModel.Fail("服务器错误，请联系后端工程师");
            byte[] bodyData = Encoding.UTF8.GetBytes(resultModel.ToJson());
            return HttpResponseHelper.GetHttpResponse(HttpResponseStatus.OK, bodyData);
        }
    }
}
