namespace Materal.BusinessFlow.TestConsole
{
    public abstract class BaseTestHandler : ITestHandler
    {
        public virtual void Excute()
        {
        }

        public virtual Task ExcuteAsync()
        {
            Excute();
            return Task.CompletedTask;
        }
    }
}
