using Materal.MergeBlock.Abstractions;

namespace Materal.MergeBlock.ExceptionInterceptorTest
{
    public class TestException : MergeBlockModuleException
    {
        public TestException()
        {
        }

        public TestException(string? message) : base(message)
        {
        }

        public TestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
