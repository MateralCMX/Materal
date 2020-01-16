using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Materal.ConDep.Commands;
using Materal.ConDep.Events;
using Materal.ConDep.Services;
using Materal.DotNetty.CommandBus;

namespace Materal.ConDep.CommandHandlers
{
    public class UploadPartCommandHandler : BaseCommandHandler<UploadPartCommand>
    {
        private readonly IUploadPoolService _uploadPoolService;

        public UploadPartCommandHandler(IUploadPoolService uploadPoolService)
        {
            _uploadPoolService = uploadPoolService;
        }

        public override async Task HandlerAsync(ICommand command, IChannel channel)
        {
            try
            {
                if (!(command is UploadPartCommand uploadPartCommand)) return;
                await _uploadPoolService.LoadBuffer(channel, uploadPartCommand);
            }
            catch (InvalidOperationException ex)
            {
                var @event = new ServerErrorEvent(ex);
                await channel.SendJsonEventAsync(@event);
            }
        }
    }
}
