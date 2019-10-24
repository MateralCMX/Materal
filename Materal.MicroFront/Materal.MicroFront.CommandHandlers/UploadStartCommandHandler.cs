using DotNetty.Transport.Channels;
using Materal.MicroFront.Commands;
using Materal.MicroFront.Common.Extension;
using Materal.MicroFront.Events;
using System;
using System.Threading.Tasks;
using Materal.Services;

namespace Materal.MicroFront.CommandHandlers
{
    public class UploadStartCommandHandler : AsyncJsonCommandHandler<UploadStartCommand>
    {
        private readonly IUploadPoolService _uploadPoolService;

        public UploadStartCommandHandler(IUploadPoolService uploadPoolService)
        {
            _uploadPoolService = uploadPoolService;
        }

        public override async Task ExcuteAsync(IChannelHandlerContext ctx, object commandData)
        {
            try
            {
                UploadStartCommand command = GetCommand(commandData);
                await _uploadPoolService.NewUpload(ctx.Channel, command);
            }
            catch (InvalidOperationException ex)
            {
                var @event = new ServerErrorEvent(ex);
                await ctx.Channel.SendJsonEventAsync(@event);
            }
        }
    }
}
