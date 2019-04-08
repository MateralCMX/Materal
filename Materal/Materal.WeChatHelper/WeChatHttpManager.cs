using Materal.WeChatHelper.Model;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace Materal.WeChatHelper
{
    /// <summary>
    /// 微信Http管理器
    /// </summary>
    public class WeChatHttpManager
    {
        /// <summary>
        /// 确认返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;//不验证直接通过验证
        }
        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="xml">xml</param>
        /// <param name="url">url地址</param>
        /// <param name="isUseCert">是否使用证书</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="config">微信支付配置对象</param>
        /// <returns>请求返回数据</returns>
        public static string Post(string xml, string url, bool isUseCert, int timeout, WeChatConfigModel config)
        {
            GC.Collect();
            string result = "";
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                ServicePointManager.DefaultConnectionLimit = 200;
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                }
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = timeout * 1000;
                request.ContentType = "text/xml";
                byte[] data = Encoding.UTF8.GetBytes(xml);
                request.ContentLength = data.Length;
                if (isUseCert)
                {
                    var cert = new X509Certificate2($"{AppDomain.CurrentDomain.BaseDirectory}{config.SSLCERT_PATH}", config.SSLCERT_PASSWORD);
                    request.ClientCertificates.Add(cert);
                }
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
                response = (HttpWebResponse)request.GetResponse();
                var sr = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException("读取流失败"), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
            finally
            {
                response?.Close();
                request?.Abort();
            }
            return result;
        }
        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <returns>http GET成功后返回的数据，失败抛WebException异常</returns>
        public static string Get(string url)
        {
            GC.Collect();
            string result = "";
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                ServicePointManager.DefaultConnectionLimit = 200;
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                }
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                response = (HttpWebResponse)request.GetResponse();
                var sr = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException("读取流失败"), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
            finally
            {
                response?.Close();
                request?.Abort();
            }
            return result;
        }
    }
}
