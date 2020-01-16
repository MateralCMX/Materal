using DotNetty.Transport.Channels;
using Materal.ConDep.Commands;
using Materal.ConDep.Common.Extension;
using Materal.ConDep.Events;
using Materal.ConDep.Services;
using System;
using System.Threading.Tasks;

namespace Materal.ConDep.CommandHandlers
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
