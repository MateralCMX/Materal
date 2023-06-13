using Materal.Utils.Wechat.ServerEventHandler;
using Microsoft.Extensions.Primitives;
using System.Text;
using WechatServerTest.EventHandlers;

namespace WechatServerTest
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<ISubscribeEventHandler, SubscribeEventHandler>();
            builder.Services.AddScoped<IUnsubscribeEventHandler, UnsubscribeEventHandler>();
            builder.Services.AddScoped<IImageMessageEventHandler, ImageMessageEventHandler>();
            builder.Services.AddScoped<ILinkMessageEventHandler, LinkMessageEventHandler>();
            builder.Services.AddScoped<ILocationMessageEventHandler, LocationMessageEventHandler>();
            builder.Services.AddScoped<IShortVideoMessageEventHandler, ShortVideoMessageEventHandler>();
            builder.Services.AddScoped<ITextMessageEventHandler, TextMessageEventHandler>();
            builder.Services.AddScoped<IVideoMessageEventHandler, VideoMessageEventHandler>();
            builder.Services.AddScoped<IVoiceMessageEventHandler, VoiceMessageEventHandler>();
            WebApplication app = builder.Build();
            app.Use(async (httpContext, next) =>
            {
                httpContext.Request.EnableBuffering();
                await SaveHttpContextInfoAsync(httpContext);
                await next();
            });
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            await app.RunAsync();
        }
        private static async Task SaveHttpContextInfoAsync(HttpContext context)
        {
            StringBuilder contentBuilder = new();
            string url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
            string method = context.Request.Method;
            contentBuilder.AppendLine($"[{DateTime.Now}]{method} {url}");
            foreach (KeyValuePair<string, StringValues> header in context.Request.Headers)
            {
                contentBuilder.AppendLine($"[Headers] {header.Key} {header.Value}");
            }
            if(method != "GET")
            {
                context.Request.Body.Seek(0, SeekOrigin.Begin);
                using MemoryStream stream = new();
                await context.Request.Body.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);
                using StreamReader streamReader = new(stream, Encoding.UTF8);
                string bodyString = await streamReader.ReadToEndAsync();
                contentBuilder.AppendLine($"[Body] {bodyString}");
                context.Request.Body.Seek(0, SeekOrigin.Begin);
            }
            string content = contentBuilder.ToString();
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HttpContextLog.log");
            await File.AppendAllTextAsync(filePath, content);
        }
    }
}