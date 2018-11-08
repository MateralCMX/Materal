using Materal.ConfigurationHelper;
using System;
using Materal.StringHelper;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        static void Main()
        {
            var a = new UrlAndPortModel("http://www.baidu.com");
            a = new UrlAndPortModel("http://www.baidu.com:3096");
            a = new UrlAndPortModel("http://127.0.0.1");
            a = new UrlAndPortModel("http://127.0.0.1:3096");
        }
    }
}
