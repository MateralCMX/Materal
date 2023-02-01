using System.IO;
using System.Threading.Tasks;
using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;
using Materal.CacheHelper;
using Materal.DotNetty.Server.CoreImpl;

namespace Materal.ConDep.Services
{
    public class ConDepFileHandler : FileHandler
    {
        public ConDepFileHandler(ICacheManager cacheManager) : base(cacheManager)
        {
        }
        protected override async Task<IFullHttpResponse> GetFileResponseAsync(IFullHttpRequest request)
        {
            if (request.Method.Name == HttpMethod.Options.Name)
            {
                return  GetOptionsResponse(HttpMethod.Get.Name);
            }
            string url;
            if (request.Uri == "/")
            {
                url = "/Portal/Index.html";
            }
            else
            {
                url = string.IsNullOrEmpty(Path.GetExtension(request.Uri)) ? Path.Combine(request.Uri, "Index.html") : request.Uri;
            }
            ICharSequence ifModifiedSince = request.Headers.Get(HttpHeaderNames.IfModifiedSince, null);
            return await GetFileResponseAsync(url, ifModifiedSince);
        }
    }
}
