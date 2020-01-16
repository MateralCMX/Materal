using DotNetty.Transport.Channels;
using Materal.ConDep.Commands;
using Materal.ConDep.Services;
using Materal.DotNetty.CommandBus;
using System;
using System.Threading.Tasks;
using Materal.ConDep.Events;

namespace Materal.ConDep.CommandHandlers
{
    public class UploadStartCommandHandler : BaseCommandHandler<UploadStartCommand>
    {
        private readonly IUploadPoolService _uploadPoolService;

        public UploadStartCommandHandler(IUploadPoolService uploadPoolService)
        {
            _uploadPoolService = uploadPoolService;
        }

        public override async Task HandlerAsync(ICommand command, IChannel channel)
        {
            try
            {
                if (!(command is UploadStartCommand uploadStartCommand)) return;
                await _uploadPoolService.NewUpload(channel, uploadStartCommand);
            }
            catch (InvalidOperationException ex)
            {
                var @event = new ServerErrorEvent(ex);
                await channel.SendJsonEventAsync(@event);
            }
        }
    }
}
