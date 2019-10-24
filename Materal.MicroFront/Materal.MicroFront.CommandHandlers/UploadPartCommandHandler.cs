using System;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Materal.MicroFront.Commands;
using Materal.MicroFront.Common.Extension;
using Materal.MicroFront.Events;
using Materal.Services;

namespace Materal.MicroFront.CommandHandlers
{
    public class UploadPartCommandHandler : AsyncJsonCommandHandler<UploadPartCommand>
    {
        private readonly IUploadPoolService _uploadPoolService;

        public UploadPartCommandHandler(IUploadPoolService uploadPoolService)
        {
            _uploadPoolService = uploadPoolService;
        }
        public override async Task ExcuteAsync(IChannelHandlerContext ctx, object commandData)
        {
            try
            {
                UploadPartCommand command = GetCommand(commandData);
                await _uploadPoolService.LoadBuffer(ctx.Channel, command);
            }
            catch (InvalidOperationException ex)
            {
                var @event = new ServerErrorEvent(ex);
                await ctx.Channel.SendJsonEventAsync(@event);
            }
        }
    }
}
