using Microsoft.AspNetCore.Http;
using Ocelot.Configuration;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.WebSockets.Middleware;
using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace Materal.Gateway.OcelotExtension.WebSockets.Middleware
{
    /// <summary>
    /// 网关WebSockets代理中间件
    /// </summary>
    public class GatewayWebSocketsProxyMiddleware : OcelotMiddleware
    {
        private static readonly string[] NotForwardedWebSocketHeaders = new[] { "Connection", "Host", "Upgrade", "Sec-WebSocket-Accept", "Sec-WebSocket-Protocol", "Sec-WebSocket-Key", "Sec-WebSocket-Version", "Sec-WebSocket-Extensions" };
        private const int DefaultWebSocketBufferSize = 4096;
        private readonly RequestDelegate _next;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        public GatewayWebSocketsProxyMiddleware(RequestDelegate next, IOcelotLoggerFactory loggerFactory): base(loggerFactory.CreateLogger<WebSocketsProxyMiddleware>())
        {
            _next = next;
        }
        /// <summary>
        /// 从一个WebSocket复制到另一个WebSocket
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="bufferSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static async Task PumpWebSocket(WebSocket source, WebSocket destination, int bufferSize, CancellationToken cancellationToken)
        {
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize));
            }

            var buffer = new byte[bufferSize];
            while (true)
            {
                WebSocketReceiveResult result;
                try
                {
                    result = await source.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    await destination.CloseOutputAsync(WebSocketCloseStatus.EndpointUnavailable, null, cancellationToken);
                    return;
                }
                catch (WebSocketException e)
                {
                    if (e.WebSocketErrorCode == WebSocketError.ConnectionClosedPrematurely)
                    {
                        await destination.CloseOutputAsync(WebSocketCloseStatus.EndpointUnavailable, null, cancellationToken);
                        return;
                    }
                    throw;
                }

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    WebSocketCloseStatus status = WebSocketCloseStatus.NormalClosure;
                    if (source.CloseStatus != null)
                    {
                        status = source.CloseStatus.Value;
                    }
                    await destination.CloseOutputAsync(status, source.CloseStatusDescription, cancellationToken);
                    return;
                }

                await destination.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, cancellationToken);
            }
        }
        /// <summary>
        /// 执行中间件
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            var uri = httpContext.Items.DownstreamRequest().ToUri();
            await Proxy(httpContext, uri);
        }
        /// <summary>
        /// 代理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="serverEndpoint"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        private async Task Proxy(HttpContext context, string serverEndpoint)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (serverEndpoint == null)
            {
                throw new ArgumentNullException(nameof(serverEndpoint));
            }

            if (!context.WebSockets.IsWebSocketRequest)
            {
                throw new InvalidOperationException();
            }

            var client = new ClientWebSocket();
            DownstreamRoute downstreamRoute = context.Items.DownstreamRoute();
            if (downstreamRoute.DangerousAcceptAnyServerCertificateValidator)
            {
                client.Options.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            }
            foreach (var protocol in context.WebSockets.WebSocketRequestedProtocols)
            {
                client.Options.AddSubProtocol(protocol);
            }

            foreach (var headerEntry in context.Request.Headers)
            {
                if (!NotForwardedWebSocketHeaders.Contains(headerEntry.Key, StringComparer.OrdinalIgnoreCase))
                {
                    try
                    {
                        client.Options.SetRequestHeader(headerEntry.Key, headerEntry.Value);
                    }
                    catch (ArgumentException)
                    {
                        // Expected in .NET Framework for headers that are mistakenly considered restricted.
                        // See: https://github.com/dotnet/corefx/issues/26627
                        // .NET Core does not exhibit this issue, ironically due to a separate bug (https://github.com/dotnet/corefx/issues/18784)
                    }
                }
            }
            var destinationUri = new Uri(serverEndpoint);
            try
            {
                await client.ConnectAsync(destinationUri, context.RequestAborted);
                using var server = await context.WebSockets.AcceptWebSocketAsync(client.SubProtocol);
                var bufferSize = DefaultWebSocketBufferSize;
                await Task.WhenAll(PumpWebSocket(client, server, bufferSize, context.RequestAborted), PumpWebSocket(server, client, bufferSize, context.RequestAborted));
            }
            catch (Exception ex)
            {
                List<Header> headers = new()
                {
                    new Header("Server", new string[] { "Materal.Gateway" })
                };
                HttpContent content = new StringContent($"websocket连接失败:{ex.Message}", Encoding.UTF8, "text/plain");
                DownstreamResponse downstreamResponse = new(content, HttpStatusCode.BadGateway, headers, "GatewayWebSocketsProxyMiddleware");
                context.Items.UpsertDownstreamResponse(downstreamResponse);
                _next?.Invoke(context);
            }
        }
    }
}
