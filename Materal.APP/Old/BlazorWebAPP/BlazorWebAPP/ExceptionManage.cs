using BlazorWebAPP.AntDesignHelper;
using Grpc.Core;
using Microsoft.AspNetCore.Components;
using System;

namespace BlazorWebAPP
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
                case RpcException rpcException:
                    HandlerRpcException(rpcException);
                    return;
                default:
                    _messageManage.Error(exception);
                    break;
            }
        }

        public void HandlerRpcException(RpcException exception)
        {
            switch (exception.StatusCode)
            {
                case StatusCode.Unauthenticated:
                    _messageManage.Error("认证失败，请重新登录");
                    _navigationManager.NavigateTo("/Login");
                    break;
                case StatusCode.NotFound:
                    _messageManage.Error("服务不存在或已失效");
                    break;
                default:
                    _messageManage.Error(exception);
                    break;
            }
        }
    }
}
