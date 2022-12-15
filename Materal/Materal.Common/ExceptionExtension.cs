using System.Text;

namespace Materal.Common
{
    public static class ExceptionExtension
    {
        /// <summary>
        /// 获得错误消息
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static string GetErrorMessage(this Exception? exception, Func<Exception, string>? beforFunc = null, Func<Exception, string>? afterFunc = null)
        {
            if(exception == null) return string.Empty;
            StringBuilder messageBuilder = new();
            Exception? tempException = exception;
            while (tempException != null)
            {
                if (beforFunc != null)
                {
                    messageBuilder.Append(beforFunc(tempException));
                }
                messageBuilder.Append(tempException.GetType().FullName);
                messageBuilder.Append("->");
                messageBuilder.AppendLine(tempException.Message);
                if (!string.IsNullOrWhiteSpace(tempException.StackTrace))
                {
                    messageBuilder.AppendLine(tempException.StackTrace);
                }
                if (afterFunc != null)
                {
                    messageBuilder.Append(afterFunc(tempException));
                }
                tempException = tempException.InnerException;
            }
            return messageBuilder.ToString();
        }
    }
}
