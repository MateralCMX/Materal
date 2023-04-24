using System.Net;
using System.Text;
using System.Xml;

namespace MateralVersion.Helper
{
    public static class NugetServerHelper
    {
        private const string _nugetUrl = "https://nuget.gudianbustu.com/nuget/";
        private const string _materalID = "Materal.Abstractions";
        private static readonly HttpClient _httpClient = new();
        /// <summary>
        /// 检查是否上传成功
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static async Task<string> GetLastMateralVersionAsync()
        {
            string url = $"{_nugetUrl}Packages";
            HttpRequestMessage httpRequestMessage = new()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };
            HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK) throw new Exception($"[{httpResponseMessage.StatusCode}]访问Nuget服务器失败");
            using MemoryStream memoryStream = new();
            await httpResponseMessage.Content.CopyToAsync(memoryStream);
            byte[] buffer = memoryStream.ToArray();
            string httpResponseResult = Encoding.UTF8.GetString(buffer);
            XmlDocument xmlDocument = new();
            xmlDocument.LoadXml(httpResponseResult);
            XmlNodeList? allNodes = xmlDocument.LastChild?.ChildNodes;
            if(allNodes == null) throw new Exception($"未在Nuget服务器上找到任何包");
            string? reslut = null;
            foreach (XmlNode item in allNodes)
            {
                if (item.Name != "entry") continue;
                string? value = item.FirstChild?.FirstChild?.Value;
                if (string.IsNullOrWhiteSpace(value)) continue;
                value = value[$"{_nugetUrl}Packages(Id='".Length..];
                int tempIndex = value.IndexOf("'");
                string id = value[..^(tempIndex - 2)];
                if (id != _materalID) continue;
                reslut = value[(id.Length + 11)..^2];
            }
            if(string.IsNullOrWhiteSpace(reslut)) throw new Exception($"未在Nuget服务器上找到包{_materalID}");
            return reslut;
        }
    }
}
