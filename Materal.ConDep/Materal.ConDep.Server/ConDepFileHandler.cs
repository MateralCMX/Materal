using System.IO;
using System.Threading.Tasks;
using DotNetty.Codecs.Http;
using Materal.DotNetty.Server.CoreImpl;

namespace Materal.ConDep.Server
{
    public class ConDepFileHandler : FileHandler
    {
        protected override async Task<IFullHttpResponse> GetFileResponseAsync(IFullHttpRequest request)
        {
            string url;
            if (request.Uri == "/")
            {
                url = "/Portal/Index.html";
            }
            else
            {
                url = string.IsNullOrEmpty(Path.GetExtension(request.Uri)) ? Path.Combine(request.Uri, "Index.html") : request.Uri;
            }
            return await GetFileResponseAsync(url);
        }
    }
}
