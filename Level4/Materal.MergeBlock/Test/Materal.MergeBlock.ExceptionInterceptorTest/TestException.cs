using Materal.MergeBlock.Abstractions;

namespace Materal.MergeBlock.ExceptionInterceptorTest
{
    /// <summary>
    /// 测试异常
    /// </summary>
    public class TestException : MergeBlockModuleException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public TestException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public TestException(string? message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
