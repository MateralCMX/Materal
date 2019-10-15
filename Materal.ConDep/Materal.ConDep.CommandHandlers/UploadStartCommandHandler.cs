using Materal.ConDep.Commands;
using System;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Materal.ConDep.Common.Extension;
using Materal.ConDep.Events;
using Materal.ConDep.Services;

namespace Materal.ConDep.CommandHandlers
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
