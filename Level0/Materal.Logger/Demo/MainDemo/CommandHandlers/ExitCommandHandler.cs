namespace MainDemo.CommandHandlers
{
    public class ExitCommandHandler : BaseCommandHandler<ExitCommandHandler>
    {
        public ExitCommandHandler(IServiceProvider services) : base(services)
        {
        }
        public override bool Handler() => false;
    }
}
