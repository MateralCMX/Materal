using System;

namespace Materal.WebSocket.Http
{
    public interface IControllerBus
    {
        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="type"></param>
        void AddController(Type type);
        /// <summary>
        /// 获取控制器
        /// </summary>
        /// <param name="controllerName"></param>
        object GetController(string controllerName);
        /// <summary>
        /// 获取参数
        /// </summary>
        event Func<Type, object> GetParams;
    }
}
