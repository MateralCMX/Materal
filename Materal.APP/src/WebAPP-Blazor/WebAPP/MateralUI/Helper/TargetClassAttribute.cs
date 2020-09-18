using System;

namespace WebAPP.MateralUI.Helper
{
    public class TargetClassAttribute : Attribute
    {
        public TargetClassAttribute(string @class)
        {
            Class = @class;
        }
        /// <summary>
        /// 类名
        /// </summary>
        public string Class{ get; }
    }
}
