using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Materal.Common;
using Materal.Gateway.Common.Models;
using Materal.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.Gateway.WebAPI.Controllers
{
    public abstract class BaseController: ControllerBase
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="handler"></param>
        /// <param name="failMessage"></param>
        /// <returns></returns>
        public async Task<TResult> HandlerAsync<TResult>(Func<Task<TResult>> handler, string failMessage = null) where TResult : ResultModel, new()
        {
            try
            {
                return await handler();
            }
            catch (AspectInvocationException ex)
            {
                if (ex.InnerException != null && ex.InnerException is GatewayException integratedPlatformException)
                {
                    return GetFailResult<TResult>(failMessage ?? integratedPlatformException.Message);
                }
                throw;
            }
            catch (GatewayException ex)
            {
                return GetFailResult<TResult>(failMessage ?? ex.Message);
            }
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="handler"></param>
        /// <param name="failMessage"></param>
        /// <returns></returns>
        public TResult Handler<TResult>(Func<TResult> handler, string failMessage = null) where TResult : ResultModel, new()
        {
            try
            {
                return handler();
            }
            catch (AspectInvocationException ex)
            {
                if (ex.InnerException is GatewayException)
                {
                    return GetFailResult<TResult>(failMessage ?? ex.InnerException?.Message);
                }
                throw;
            }
            catch (GatewayException ex)
            {
                return GetFailResult<TResult>(failMessage ?? ex.Message);
            }
        }

        private TResult GetFailResult<TResult>(string message) where TResult : ResultModel, new()
        {
            TResult result = new TResult
            {
                ResultType = ResultTypeEnum.Fail,
                Message = message
            };
            return result;
        }
    }
}