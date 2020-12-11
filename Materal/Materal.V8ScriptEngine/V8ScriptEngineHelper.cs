using Materal.ConvertHelper;
using System;
using System.IO;
using System.Linq;

namespace Materal.V8ScriptEngine
{
    public class V8ScriptEngineHelper
    {
        private readonly string[] _libsPath;
        public V8ScriptEngineHelper(params string[] libsPath)
        {
            _libsPath = libsPath;
        }
        /// <summary>
        /// 执行代码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="resultName"></param>
        /// <returns></returns>
        public T HandlerByCode<T>(string code, string resultName = "result")
        {
            using Microsoft.ClearScript.V8.V8ScriptEngine engine = GetEngine();
            string cmd = _libsPath.Aggregate(string.Empty, (current, libPath) => current + $"{File.ReadAllText(libPath)}\r\n");
            cmd += code;
            engine.Execute(cmd);
            var result = ((object)engine.Script[resultName]).ToJson().JsonToDeserializeObject<T>();
            return result;
        }
        /// <summary>
        /// 通过文件执行代码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="runCode"></param>
        /// <param name="resultName"></param>
        /// <returns></returns>
        public T HandlerByFile<T>(string filePath, string runCode, string resultName = "result")
        {
            string[] filePaths = { filePath };
            return HandlerByFiles<T>(filePaths, runCode, resultName);
        }
        /// <summary>
        /// 通过文件执行代码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePaths"></param>
        /// <param name="runCode"></param>
        /// <param name="resultName"></param>
        /// <returns></returns>
        public T HandlerByFiles<T>(string[] filePaths, string runCode, string resultName = "result")
        {
            string cmd = filePaths.Aggregate(string.Empty, (current, filePath) => current + $"{File.ReadAllText(filePath)}\r\n");
            cmd = $"{cmd}{runCode}";
            return HandlerByCode<T>(cmd, resultName);
        }
        /// <summary>
        /// 获得引擎
        /// </summary>
        /// <returns></returns>
        private Microsoft.ClearScript.V8.V8ScriptEngine GetEngine()
        {
            var result = new Microsoft.ClearScript.V8.V8ScriptEngine();
            result.AddHostType("Console", typeof(Console));
            return result;
        }
    }
}
