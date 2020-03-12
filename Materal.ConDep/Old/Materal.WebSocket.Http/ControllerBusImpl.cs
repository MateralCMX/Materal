using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Materal.WebSocket.Http
{
    public class ControllerBusImpl : IControllerBus
    {
        private static readonly List<Type> _controllers = new List<Type>();
        public event Func<Type, object> GetParams;
        public void AddController(Type type)
        {
            if (!_controllers.Contains(type))
            {
                _controllers.Add(type);
            }
        }
        public object GetController(string controllerName)
        {
            Type type = _controllers.FirstOrDefault(m => m.Name.Equals(controllerName));
            if (type == null) throw new InvalidOperationException("未找到对应控制器");
            ConstructorInfo[] constructors = type.GetConstructors();
            if (constructors.Length == 0) throw new InvalidOperationException("控制器无法构建");
            ConstructorInfo constructor = null;
            var constructorParametersLength = 0;
            foreach (ConstructorInfo constructorInfo in constructors)
            {
                int length = constructorInfo.GetParameters().Length;
                if (length < constructorParametersLength) continue;
                constructor = constructorInfo;
                constructorParametersLength = length;
            }
            if(constructor == null) throw new InvalidOperationException("控制器无法构建");
            ParameterInfo[] parameterInfos = constructor.GetParameters();
            var parameters = new object[parameterInfos.Length];
            for (var i = 0; i < parameterInfos.Length; i++)
            {
                parameters[i] = GetParams?.Invoke(parameterInfos[i].ParameterType);
            }
            object controller = Activator.CreateInstance(type, parameters);
            return controller;
        }
    }
}
