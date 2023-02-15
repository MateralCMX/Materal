using Ocelot.Configuration;

namespace Materal.Gateway.OcelotExtension.Middleware
{
    public static class DownstreamRouteExtension
    {
        public static string GetDownstreamScheme(this DownstreamRoute downstreamRoute) => downstreamRoute.DownstreamScheme switch
        {
            "grpc" => "http",
            "grpcs" => "https",
            _ => downstreamRoute.DownstreamScheme,
        };
    }
}
