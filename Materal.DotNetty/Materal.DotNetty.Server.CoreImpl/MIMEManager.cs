using Materal.DotNetty.Server.Core;
using System;
using System.Collections.Generic;
using System.IO;
using Materal.ConvertHelper;

namespace Materal.DotNetty.Server.CoreImpl
{
    public static class MIMEManager
    {
        private static readonly Dictionary<string, string> _mimeDic;
        static MIMEManager()
        {
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MIMEConfig.json");
            if (!File.Exists(configFilePath)) throw new DotNettyServerException("MIMEConfig.json文件丢失");
            string jsonConfigString = File.ReadAllText(configFilePath);
            _mimeDic = jsonConfigString.JsonToDeserializeObject<Dictionary<string, string>>();
        }
        /// <summary>
        /// 获得ContentType
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GetContentType(string extension)
        {
            return _mimeDic.ContainsKey(extension) ? _mimeDic[extension] : "application/octet-stream";
        }
    }
}
