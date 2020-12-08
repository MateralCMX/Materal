using Materal.DotNetty.CommandBus;

namespace Demo.Common
{
    public abstract class DemoCommand : BaseCommand
    {
        protected DemoCommand()
        {
            Command = GetType().Name;
        }
    }
}
