using Materal.Abstractions;
using System.Text;

namespace System
{
    /// <summary>
    /// 异常扩展
    /// </summary>
    public static class ExceptionExtension
    {
        /// <summary>
        /// 获得错误消息
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="beforFunc"></param>
        /// <param name="afterFunc"></param>
        /// <returns></returns>
        public static string GetErrorMessage(this Exception? exception, Func<Exception, string?>? beforFunc = null, Func<Exception, string?>? afterFunc = null)
        {
            if (exception == null) return string.Empty;
            StringBuilder messageBuilder = new();
            Exception? tempException = exception;
            while (tempException != null)
            {
                if (tempException is not MateralException materalException)
                {
                    if (beforFunc != null)
                    {
                        string? temp = beforFunc(tempException);
                        if (!string.IsNullOrWhiteSpace(temp))
                        {
                            messageBuilder.AppendLine(temp);
                        }
                    }
                    messageBuilder.Append(tempException.GetType().FullName);
                    messageBuilder.Append("-->");
                    messageBuilder.AppendLine(tempException.Message);
                    if (!string.IsNullOrWhiteSpace(tempException.StackTrace))
                    {
                        messageBuilder.AppendLine(tempException.StackTrace);
                    }
                    if (afterFunc != null)
                    {
                        string? temp = afterFunc(tempException);
                        if (!string.IsNullOrWhiteSpace(temp))
                        {
                            messageBuilder.AppendLine(temp);
                        }
                    }
                }
                else
                {
                    messageBuilder.AppendLine(materalException.GetExceptionMessage(beforFunc, afterFunc));
                }
                tempException = tempException.InnerException;
            }
            string result = messageBuilder.ToString();
            return result;
        }
    }
}
