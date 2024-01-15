using NetCore.AutoRegisterDi;

namespace Materal.BaseCore.HttpClient.Extensions
{
    public static class HttpClientDIExtension
    {
        public static IServiceCollection AddHttpClientService(this IServiceCollection services, string appName, params Assembly[] assemblies)
        {
            HttpClientConfig.AppName = appName;
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes().Where(m => !m.IsAbstract && m.Name.EndsWith("HttpClient")).ToArray();
                foreach (Type type in types)
                {
                    services.AddSingleton(type);
                }
            }
            return services;
        }
    }
}
