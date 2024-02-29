namespace Materal.Utils.Http
{
    /// <summary>
    /// Http异常
    /// </summary>
    public class MateralHttpException : MateralException
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        public HttpRequestMessage? HttpRequestMessage { get; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public HttpResponseMessage? HttpResponseMessage { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="httpResponseMessage"></param>
        public MateralHttpException(HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage) : this(httpRequestMessage, httpResponseMessage, $"Http请求错误{Convert.ToInt32(httpResponseMessage.StatusCode)}")
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="httpResponseMessage"></param>
        /// <param name="message"></param>
        public MateralHttpException(HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage, string message) : base(message)
        {
            HttpRequestMessage = httpRequestMessage;
            HttpResponseMessage = httpResponseMessage;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="httpResponseMessage"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MateralHttpException(HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage, string message, Exception innerException) : base(message, innerException)
        {
            HttpRequestMessage = httpRequestMessage;
            HttpResponseMessage = httpResponseMessage;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="message"></param>
        public MateralHttpException(HttpRequestMessage httpRequestMessage, string message) : base(message)
        {
            HttpRequestMessage = httpRequestMessage;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MateralHttpException(HttpRequestMessage httpRequestMessage, string message, Exception innerException) : base(message, innerException)
        {
            HttpRequestMessage = httpRequestMessage;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MateralHttpException(string message, Exception innerException) : base(message, innerException)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public MateralHttpException(string message) : base(message)
        {
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public override string GetDetailMessage(string prefix)
        {
            StringBuilder errorMessage = new();
            errorMessage.AppendLine(Message);
            errorMessage.Append($"{prefix}--- Http详细信息开始 ---");
            if (HttpRequestMessage is not null)
            {
                errorMessage.AppendLine();
                errorMessage.Append($"{prefix}Request:");
                if (HttpRequestMessage.RequestUri is not null)
                {
                    errorMessage.AppendLine();
                    errorMessage.Append($"{prefix}\tUrl: {HttpRequestMessage.RequestUri.AbsoluteUri}");
                }
                if (HttpRequestMessage.Headers.Any())
                {
                    errorMessage.AppendLine();
                    errorMessage.Append($"{prefix}\tHeaders:");
                    foreach (KeyValuePair<string, IEnumerable<string>> header in HttpRequestMessage.Headers)
                    {
                        errorMessage.AppendLine();
                        errorMessage.Append($"{prefix}\t\t{header.Key}: {string.Join(";", header.Value)}");
                    }
                }
                if (HttpRequestMessage.Content is not null)
                {
                    errorMessage.AppendLine();
                    errorMessage.Append($"{prefix}\tContent:");
                    if (HttpRequestMessage.Content.Headers.Any())
                    {
                        errorMessage.AppendLine();
                        errorMessage.Append($"{prefix}\t\tContentHeaders:");
                        foreach (KeyValuePair<string, IEnumerable<string>> header in HttpRequestMessage.Content.Headers)
                        {
                            errorMessage.AppendLine();
                            errorMessage.Append($"{prefix}\t\t\t{header.Key}:{string.Join(";", header.Value)}");
                        }
                    }
                    string message = HttpRequestMessage.Content.ReadAsStringAsync().Result;
                    errorMessage.AppendLine();
                    errorMessage.AppendLine($"{prefix}\t\tContentBody:");
                    errorMessage.Append($"{prefix}\t\t\t{message}");
                }
            }
            if (HttpResponseMessage is not null)
            {
                errorMessage.AppendLine();
                errorMessage.AppendLine($"{prefix}Response:");
                errorMessage.Append($"{prefix}\tStatusCode: {HttpResponseMessage.StatusCode}[{(int)HttpResponseMessage.StatusCode}]");
                if (HttpResponseMessage.Headers.Any())
                {
                    errorMessage.AppendLine();
                    errorMessage.Append($"{prefix}\tHeaders:");
                    foreach (KeyValuePair<string, IEnumerable<string>> header in HttpResponseMessage.Headers)
                    {
                        errorMessage.AppendLine();
                        errorMessage.Append($"{prefix}\t\t{header.Key}: {string.Join(";", header.Value)}");
                    }
                }
                if (HttpResponseMessage.Content is not null)
                {
                    errorMessage.AppendLine();
                    errorMessage.Append($"{prefix}\tContent:");
                    if (HttpResponseMessage.Content.Headers.Any())
                    {
                        errorMessage.AppendLine();
                        errorMessage.Append($"{prefix}\t\tContentHeaders:");
                        foreach (KeyValuePair<string, IEnumerable<string>> header in HttpResponseMessage.Content.Headers)
                        {
                            errorMessage.AppendLine();
                            errorMessage.Append($"{prefix}\t\t\t{header.Key}: {string.Join(";", header.Value)}");
                        }
                    }
                    string message = HttpResponseMessage.Content.ReadAsStringAsync().Result;
                    errorMessage.AppendLine();
                    errorMessage.AppendLine($"{prefix}\t\tContentBody:");
                    errorMessage.Append($"{prefix}\t\t\t{message}");
                }
            }
            if (errorMessage[^1] != '\n')
            {
                errorMessage.AppendLine();
            }
            errorMessage.Append($"{prefix}--- Http详细信息结束 ---");
            string result = errorMessage.ToString();
            return result;
        }
    }
}
