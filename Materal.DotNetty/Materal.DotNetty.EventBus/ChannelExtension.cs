using DotNetty.Buffers;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Transport.Channels;
using Materal.ConvertHelper;
using System.Threading.Tasks;

namespace Materal.DotNetty.EventBus
{
    public static class ChannelExtension
    {
        public static async Task SendEventByJsonAsync(this IChannel channel, IEvent @event)
        {
            if (channel != null && channel.Open)
            {
                string eventJson = @event.ToJson();
                await channel.WriteAndFlushAsync(new TextWebSocketFrame(eventJson));
            }
        }
        public static async Task SendEventByBytesAsync(this IChannel channel, IEvent @event)
        {
            byte[] bytes = @event.ToBytes();
            await SendBytesAsync(channel, bytes);
        }
        public static async Task SendBytesAsync(this IChannel channel, byte[] bytes)
        {
            if (channel != null && channel.Open)
            {
                IByteBuffer buffer = Unpooled.WrappedBuffer(bytes);
                var socketFrame = new BinaryWebSocketFrame(buffer);
                await channel.WriteAndFlushAsync(socketFrame);
            }
        }
    }
}
