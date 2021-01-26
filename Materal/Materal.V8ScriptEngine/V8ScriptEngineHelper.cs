using Materal.ConvertHelper;
using System;
using System.IO;
using System.Linq;
using Materal.CacheHelper;
using Materal.Common;

namespace Materal.V8ScriptEngine
{
    public class V8ScriptEngineHelper
    {
        private static ICacheManager _cacheManager;
        private readonly string[] _libsPaths;
        public V8ScriptEngineHelper(params string[] libsPaths)
        {
            _cacheManager ??= new MemoryCacheManager();
            _libsPaths = libsPaths;
        }
        public V8ScriptEngineHelper(ICacheManager cacheManager, params string[] libsPaths)
        {
            _cacheManager = cacheManager;
            _libsPaths = libsPaths;
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
            string cmd = _libsPaths.Aggregate(string.Empty, (current, libPath) => current + $"{GetFileContent(libPath)}\r\n");
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
            string cmd = filePaths.Aggregate(string.Empty, (current, filePath) => current + $"{GetFileContent(filePath)}\r\n");
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
        /// <summary>
        /// 获得文件内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string GetFileContent(string filePath)
        {
            string result = _cacheManager.Get<string>(filePath);
            if (!string.IsNullOrWhiteSpace(result)) return result;
            if (!File.Exists(filePath)) throw new MateralException($"文件{filePath}不存在");
            result = File.ReadAllText(filePath);
            _cacheManager.SetBySliding(filePath, result, 1);
            return result;
        }
    }
}
