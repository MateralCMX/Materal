using System;
using System.Text;

namespace Materal.Abstractions
{
    /// <summary>
    /// Materal基础异常类
    /// </summary>
    public class MateralException : Exception
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralException(string message, Exception innerException) : base(message, innerException)
        {
        }
        /// <summary>
        /// 获得异常消息
        /// </summary>
        /// <returns></returns>
        public virtual string GetExceptionMessage(Func<Exception, string?>? beforFunc = null, Func<Exception, string?>? afterFunc = null)
        {
            StringBuilder messageBuilder = new();
            Exception? tempException = this;
            while (tempException != null)
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
                tempException = tempException.InnerException;
            }
            string result = messageBuilder.ToString();
            return result;
        }
    }
}
