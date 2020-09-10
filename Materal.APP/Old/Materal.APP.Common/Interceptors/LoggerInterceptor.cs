using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Materal.APP.Common.Interceptors
{
    public class LoggerInterceptor : Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            return await base.UnaryServerHandler(request, context, continuation);
        }
    }
}
