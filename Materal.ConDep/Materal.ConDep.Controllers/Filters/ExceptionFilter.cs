using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using Materal.ConDep.Common;
using Materal.ConvertHelper;
using Materal.DotNetty.ControllerBus.Filters;
using Materal.DotNetty.Server.CoreImpl;
using Materal.Model;
using System;
using System.Text;
using Materal.DotNetty.Common;

namespace Materal.ConDep.Controllers.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void HandlerException(IByteBufferHolder byteBufferHolder, Exception exception, ref IFullHttpResponse response)
        {
            ConsoleHelper.ServerWriteError(exception);
            ResultModel resultModel = ResultModel.Fail("服务器错误，请联系后端工程师");
            byte[] bodyData = Encoding.UTF8.GetBytes(resultModel.ToJson());
            response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.OK, bodyData);
        }
    }
}
