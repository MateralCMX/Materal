using System;

namespace Materal.DotNetty.ControllerBus.Attributes
{
    [AttributeUsage(AttributeTargets.All,AllowMultiple = false)]
    public class RouteAttribute : Attribute
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; }
        public RouteAttribute(string key)
        {
            Key = key;
        }
    }
}
