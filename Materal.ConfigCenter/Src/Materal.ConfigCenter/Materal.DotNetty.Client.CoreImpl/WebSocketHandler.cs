using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Materal.DotNetty.Client.Core;
using System;
using System.Threading.Tasks;

namespace Materal.DotNetty.Client.CoreImpl
{
    public class WebSocketHandler : ClientHandlerContext
    {
        public override async Task HandlerAsync(IChannelHandlerContext ctx, object data)
        {
            throw new NotImplementedException();
        }
    }
}
