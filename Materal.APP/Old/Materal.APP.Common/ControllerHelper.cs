using AspectCore.DynamicProxy;
using Materal.Common;
using Materal.Model;
using System;
using System.Threading.Tasks;

namespace Materal.APP.Common
{
    public static class ControllerHelper
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static async Task<TResult> HandlerAsync<TResult>(Func<Task<TResult>> func, string errorMessage = null) where TResult : ResultModel, new()
        {
            try
            {
                return await func();
            }
            catch (MateralAPPException exception)
            {
                return GetResultModel<TResult>(errorMessage ?? exception.Message, ResultTypeEnum.Fail);
            }
            catch (AspectInvocationException exception)
            {
                if (exception.InnerException is MateralAPPException)
                {
                    return GetResultModel<TResult>(errorMessage ?? exception.InnerException.Message, ResultTypeEnum.Fail);
                }
                throw;
            }
        }
        /// <summary>
        /// 获得返回模型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="message"></param>
        /// <param name="resultType"></param>
        /// <returns></returns>
        private static TResult GetResultModel<TResult>(string message, ResultTypeEnum resultType) where TResult : ResultModel, new()
        {
            var result = new TResult
            {
                Message = message, 
                ResultType = resultType
            };
            return result;
        }
    }
}
