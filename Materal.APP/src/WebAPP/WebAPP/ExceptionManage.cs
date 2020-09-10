using Materal.NetworkHelper;
using Microsoft.AspNetCore.Components;
using System;
using System.Net;
using WebAPP.AntDesignHelper;

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

        public void HandlerException(Exception exception)
        {
            switch (exception)
            {
                case MateralHttpException httpException:
                    HandlerHttpException(httpException);
                    return;
                default:
                    _messageManage.Error(exception);
                    break;
            }
        }

        public void HandlerHttpException(MateralHttpException exception)
        {
            switch (exception.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _messageManage.Error("认证失败，请重新登录");
                    _navigationManager.NavigateTo("/Login");
                    break;
                case HttpStatusCode.NotFound:
                    _messageManage.Error($"服务 [{exception.Url}] 不存在或已失效");
                    break;
                default:
                    _messageManage.Error(exception);
                    break;
            }
        }
    }
}
