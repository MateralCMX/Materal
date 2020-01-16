using System.Threading.Tasks;
using DotNetty.Buffers;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Transport.Channels;
using Materal.ConvertHelper;

namespace Materal.ConDep.Events
{
    public static class ChannelExtension
    {
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task SendDataAsync(this IChannel channel, byte[] data)
        {
            if (channel != null && channel.Open)
            {
                IByteBuffer buffer = Unpooled.WrappedBuffer(data);
                var socketFrame = new BinaryWebSocketFrame(buffer);
                await channel.WriteAndFlushAsync(socketFrame);
            }
        }
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="event"></param>
        /// <returns></returns>
        public static async Task SendJsonEventAsync(this IChannel channel, Event @event)
        {
            if (channel != null && channel.Open)
            {
                string eventJson = @event.ToJson();
                await channel.WriteAndFlushAsync(new TextWebSocketFrame(eventJson));
            }
        }
    }
}
