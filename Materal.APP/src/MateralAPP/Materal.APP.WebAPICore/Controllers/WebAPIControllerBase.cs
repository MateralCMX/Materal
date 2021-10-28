using AspectCore.DynamicProxy;
using Materal.Common;
using Materal.Model;
using Materal.StringHelper;
using MateralAPP.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Materal.APP.WebAPICore.Controllers
{
    public abstract class WebAPIControllerBase : ControllerBase
    {
        /// <summary>
        /// 获得登录用户唯一标识
        /// </summary>
        /// <returns></returns>
        protected Guid GetLoginUserID()
        {
            Claim claim = User.Claims.FirstOrDefault(m => m.Type == "UserID");
            if (claim == null || !claim.Value.IsGuid()) throw new InvalidOperationException("未登录");
            Guid userID = Guid.Parse(claim.Value);
            return userID;
        }

        /// <summary>
        /// 处理
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="handler"></param>
        /// <param name="failMessage"></param>
        /// <returns></returns>
        protected async Task<TResult> HandlerAsync<TResult>(Func<Task<TResult>> handler, string failMessage = null) where TResult : ResultModel, new()
        {
            try
            {
                return await handler();
            }
            catch (AspectInvocationException ex)
            {
                if (ex.InnerException != null && ex.InnerException is MateralAPPException integratedPlatformException)
                {
                    return GetFailResult<TResult>(failMessage ?? integratedPlatformException.Message);
                }
                throw;
            }
            catch (MateralAPPException ex)
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
        protected TResult Handler<TResult>(Func<TResult> handler, string failMessage = null) where TResult : ResultModel, new()
        {
            try
            {
                return handler();
            }
            catch (AspectInvocationException ex)
            {
                if (ex.InnerException is MateralAPPException)
                {
                    return GetFailResult<TResult>(failMessage ?? ex.InnerException?.Message);
                }
                throw;
            }
            catch (MateralAPPException ex)
            {
                return GetFailResult<TResult>(failMessage ?? ex.Message);
            }
        }
        /// <summary>
        /// 获得失败返回
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
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
