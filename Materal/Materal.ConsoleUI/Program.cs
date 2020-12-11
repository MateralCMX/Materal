using Materal.V8ScriptEngine;
using System;
using System.IO;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            string[] libsPath = {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? "", "turf.min.js")
            };

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? string.Empty, "TestJs.js");
            var jsCode = $@"
var a = Handler([[[125, -15], [113, -22], [154, -27], [144, -15], [125, -15]]]);
";

            var engineHelper = new V8ScriptEngineHelper(libsPath);
            var value = engineHelper.HandlerByFile<double>(filePath, jsCode, "a");
            Console.WriteLine(value);
        }
    }
}
