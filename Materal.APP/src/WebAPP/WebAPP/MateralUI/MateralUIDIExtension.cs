using Microsoft.Extensions.DependencyInjection;

namespace WebAPP.MateralUI
{
    public static class MateralUIDIExtension
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddMateralUI(this IServiceCollection services)
        {
            services.AddSingleton<MessageService>();
            services.AddSingleton<NotificationService>();
        }
    }
}
