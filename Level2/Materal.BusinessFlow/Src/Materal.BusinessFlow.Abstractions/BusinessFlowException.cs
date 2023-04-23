using Materal.Abstractions;

namespace Materal.BusinessFlow.Abstractions
{
    /// <summary>
    /// 业务流异常
    /// </summary>
    public class BusinessFlowException : MateralException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public BusinessFlowException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public BusinessFlowException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public BusinessFlowException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
