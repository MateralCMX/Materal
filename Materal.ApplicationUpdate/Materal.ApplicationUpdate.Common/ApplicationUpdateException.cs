using Materal.Common;
using System;
using System.Runtime.Serialization;

namespace Materal.ApplicationUpdate.Common
{
    /// <summary>
    /// 应用程序更新异常
    /// </summary>
    public class ApplicationUpdateException : MateralException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ApplicationUpdateException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public ApplicationUpdateException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ApplicationUpdateException(string message, Exception innerException) : base(message, innerException)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ApplicationUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
