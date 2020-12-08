using Demo.Commands;
using Demo.Events;
using DotNetty.Transport.Channels;
using Materal.DotNetty.CommandBus;
using Materal.DotNetty.EventBus;
using System.Threading.Tasks;
using Demo.Common;

namespace Demo.CommandHandlers
{
    public class SendMessageCommandHandler : BaseCommandHandler<SendMessageCommand>
    {
        public override async Task HandlerAsync(SendMessageCommand command, IChannel channel)
        {
            ConsoleHelper.WriteLine($"接收到消息:{command.Message}");
            var @event = new ReplyMessageEvent
            {
                Message = $"已接收消息:{command.Message}"
            };
            await channel.SendEventByJsonAsync(@event);
        }
    }
}
