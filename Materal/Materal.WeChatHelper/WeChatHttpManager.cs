using Materal.WeChatHelper.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Materal.ConvertHelper;

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
        /// <param name="url">url地址</param>
        /// <param name="xml">xml</param>
        /// <param name="isUseCert">是否使用证书</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="config">微信支付配置对象</param>
        /// <returns>请求返回数据</returns>
        public static string PostXml(string url, string xml, bool isUseCert, int timeout, WeChatConfigModel config)
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
                byte[] buffer = Encoding.UTF8.GetBytes(xml);
                request.ContentLength = buffer.Length;
                if (isUseCert)
                {
                    var cert = new X509Certificate2($"{AppDomain.CurrentDomain.BaseDirectory}{config.SSLCERT_PATH}", config.SSLCERT_PASSWORD);
                    request.ClientCertificates.Add(cert);
                }
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
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
        /// POST请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">xml</param>
        /// <param name="isUseCert">是否使用证书</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="config">微信支付配置对象</param>
        /// <returns>请求返回数据</returns>
        public static string PostJson<T>(string url, T data, bool isUseCert, int timeout, WeChatConfigModel config)
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
                request.ContentType = "application/json";
                byte[] dataBuffer = Encoding.UTF8.GetBytes(data.ToJson());
                request.ContentLength = dataBuffer.Length;
                if (isUseCert)
                {
                    var cert = new X509Certificate2($"{AppDomain.CurrentDomain.BaseDirectory}{config.SSLCERT_PATH}", config.SSLCERT_PASSWORD);
                    request.ClientCertificates.Add(cert);
                }
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(dataBuffer, 0, dataBuffer.Length);
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
            using (StreamReader sr = GetStreamReader(url))
            {
                string result = sr.ReadToEnd().Trim();
                return result;
            }
        }
        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <returns>http GET成功后返回的数据，失败抛WebException异常</returns>
        public static StreamReader GetStreamReader(string url)
        {
            GC.Collect();
            StreamReader result = null;
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
                result = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException("读取流失败"), Encoding.UTF8);
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
        /// POST请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="formItems"></param>
        /// <param name="isUseCert">是否使用证书</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="config">微信支付配置对象</param>
        /// <returns>请求返回数据</returns>
        public static string PostFormData(string url, ICollection<FormItemModel> formItems, bool isUseCert, int timeout, WeChatConfigModel config)
        {
            GC.Collect();
            var result = "";
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
                string boundary = "----" + DateTime.Now.Ticks.ToString("x");//分隔符
                request.ContentType = $"multipart/form-data; boundary={boundary}";
                //请求流
                using (var postStream = new MemoryStream())
                {
                    #region 处理Form表单请求内容
                    //是否用Form上传文件
                    bool formUploadFile = formItems != null && formItems.Count > 0;
                    if (formUploadFile)
                    {
                        //文件数据模板
                        string fileFormdataTemplate =
                            "\r\n--" + boundary +
                            "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
                            "\r\nContent-Type: application/octet-stream" +
                            "\r\n\r\n";
                        //文本数据模板
                        string dataFormdataTemplate =
                            "\r\n--" + boundary +
                            "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                            "\r\n\r\n{1}";
                        foreach (FormItemModel item in formItems)
                        {
                            string formdata;
                            if (item.IsFile)
                            {
                                //上传文件
                                formdata = string.Format(
                                    fileFormdataTemplate,
                                    item.Key, //表单键
                                    item.FileName);
                            }
                            else
                            {
                                //上传文本
                                formdata = string.Format(
                                    dataFormdataTemplate,
                                    item.Key,
                                    item.Value);
                            }
                            //统一处理
                            byte[] formdataBytes = Encoding.UTF8.GetBytes(postStream.Length == 0 ? formdata.Substring(2, formdata.Length - 2) : formdata);
                            postStream.Write(formdataBytes, 0, formdataBytes.Length);
                            //写入文件内容
                            if (item.FileContent == null || item.FileContent.Length <= 0) continue;
                            using (Stream stream = item.FileContent)
                            {
                                var buffer = new byte[1024];
                                int bytesRead;
                                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                                {
                                    postStream.Write(buffer, 0, bytesRead);
                                }
                            }
                        }
                        //结尾
                        byte[] footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                        postStream.Write(footer, 0, footer.Length);

                    }
                    else
                    {
                        request.ContentType = "application/x-www-form-urlencoded";
                    }
                    #endregion
                    request.ContentLength = postStream.Length;
                    #region 输入二进制流
                    {
                        postStream.Position = 0;
                        //直接写入流
                        Stream requestStream = request.GetRequestStream();
                        var buffer = new byte[1024];
                        int bytesRead;
                        while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            requestStream.Write(buffer, 0, bytesRead);
                        }
                    }
                    #endregion
                }
                if (isUseCert)
                {
                    var cert = new X509Certificate2($"{AppDomain.CurrentDomain.BaseDirectory}{config.SSLCERT_PATH}", config.SSLCERT_PASSWORD);
                    request.ClientCertificates.Add(cert);
                }
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
