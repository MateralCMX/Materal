using System;

namespace Materal.DotNetty.CommandBus
{
    [Serializable]
    public class BaseCommand : ICommand
    {
        public string Command { get; set; }

        public string CommandHandler => $"{Command}Handler";
    }
}
