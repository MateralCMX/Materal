using System;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Materal.APP.Common;
using Materal.APP.GrpcModel;
using Materal.ConvertHelper;
using Microsoft.AspNetCore.Authorization;

namespace Materal.APP.Server.Services
{
    public class StringHelperService : GrpcModel.StringHelper.StringHelperBase
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public override Task<StringHandlerGrpcResultModel> DesDecryption(StringHandlerGrpcRequestModel request, ServerCallContext context)
        {
            try
            {
                string result = request.Value.DesDecode(ApplicationConfig.DesKey, ApplicationConfig.DesIV, Encoding.UTF8);
                return Task.FromResult(new StringHandlerGrpcResultModel
                {
                    Status = 0,
                    Message = "���ܳɹ�",
                    Value = result
                });
            }
            catch (Exception exception)
            {
                return Task.FromResult(new StringHandlerGrpcResultModel
                {
                    Status = 1,
                    Message = exception.Message,
                    Value = string.Empty
                });
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<StringHandlerGrpcResultModel> DesEncryption(StringHandlerGrpcRequestModel request, ServerCallContext context)
        {
            try
            {
                string result = request.Value.ToDesEncode(ApplicationConfig.DesKey, ApplicationConfig.DesIV, Encoding.UTF8);
                return Task.FromResult(new StringHandlerGrpcResultModel
                {
                    Status = 0,
                    Message = "���ܳɹ�",
                    Value = result
                });
            }
            catch (Exception exception)
            {
                return Task.FromResult(new StringHandlerGrpcResultModel
                {
                    Status = 1,
                    Message = exception.Message,
                    Value = string.Empty
                });
            }
        }
    }
}
