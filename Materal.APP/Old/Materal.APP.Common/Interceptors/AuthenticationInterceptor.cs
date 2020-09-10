using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Authorization;

namespace Materal.APP.Common.Interceptors
{
    public class AuthenticationInterceptor : Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {

                string[] temp = context.Method.Split('/');
                if (temp.Length != 3 || temp[0] != string.Empty) throw new MateralAPPException("未识别请求");
                string serviceName = $"{temp[1]}Service";
                string methodName = temp[2];
                var nowAssembly = Assembly.GetEntryAssembly();
                if(nowAssembly==null) throw new MateralAPPException("未识别入口");
                Type serviceType = nowAssembly.GetTypes().FirstOrDefault(m => m.Name.Equals(serviceName));
                if(serviceType == null) throw new MateralAPPException("未识别服务");
                var allowAnonymousAttribute = serviceType.GetCustomAttribute<AllowAnonymousAttribute>();
                if (allowAnonymousAttribute != null)
                {
                    return await base.UnaryServerHandler(request, context, continuation);
                }
                MethodInfo methodInfo = serviceType.GetMethod(methodName);
                if (methodInfo == null) throw new MateralAPPException("未识别服务处理");
                allowAnonymousAttribute = methodInfo.GetCustomAttribute<AllowAnonymousAttribute>();
                if (allowAnonymousAttribute != null)
                {
                    return await base.UnaryServerHandler(request, context, continuation);
                }
                JwtSecurityToken jwtSecurityToken = GrpcServiceHelper.GetJwtSecurityToken(context);
                if (jwtSecurityToken == null) throw new MateralAPPException("未识别Token");
            }
            catch (MateralAPPException exception)
            {
                context.Status = new Status(StatusCode.Unauthenticated, exception.Message);
            }
            return await base.UnaryServerHandler(request, context, continuation);
        }
    }
}
