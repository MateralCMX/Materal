using Materal.V8ScriptEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            var url = "https://api.weixin.qq.com/sns/jscode2session";
            var data = new Dictionary<string, string>
            {
                {"appid", "wx5752ec798fd5fe95"},
                {"secret", "5e04a5c6146f07bca0b4f5b572563bb2"},
                {"grant_type", "authorization_code"},
                {"js_code", HttpUtility.UrlDecode("%22%20union%20select%201%2C2--%20")},
            };
        }
    }
}
