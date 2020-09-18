using Materal.NetworkHelper;
using Microsoft.AspNetCore.Components;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WebAPP
{
    public class ExceptionManage
    {
        private readonly NavigationManager _navigationManager;
        private readonly MessageManage _messageManage;

        public ExceptionManage(NavigationManager navigationManager, MessageManage messageManage)
        {
            _navigationManager = navigationManager;
            _messageManage = messageManage;
        }

        public async Task HandlerExceptionAsync(Exception exception)
        {
            switch (exception)
            {
                case MateralHttpException httpException:
                    await HandlerHttpExceptionAsync(httpException);
                    return;
                default:
                    await _messageManage.ErrorAsync(exception);
                    break;
            }
        }

        public async Task HandlerHttpExceptionAsync(MateralHttpException exception)
        {
            switch (exception.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    await _messageManage.ErrorAsync("认证失败，请重新登录");
                    _navigationManager.NavigateTo("/Login");
                    break;
                case HttpStatusCode.NotFound:
                    await _messageManage.ErrorAsync($"服务 [{exception.Url}] 不存在或已失效");
                    break;
                default:
                    await _messageManage.ErrorAsync(exception);
                    break;
            }
        }
    }
}
