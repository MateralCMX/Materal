using Materal.Abstractions;

namespace Materal.Workflow
{
    /// <summary>
    /// 工作流异常
    /// </summary>
    public class WorkflowException : MateralException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public WorkflowException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public WorkflowException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public WorkflowException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
    /// <summary>
    /// 工作流运行时异常
    /// </summary>
    public class WorkflowRuntimeException : WorkflowException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public WorkflowRuntimeException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public WorkflowRuntimeException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public WorkflowRuntimeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
