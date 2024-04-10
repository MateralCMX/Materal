using Microsoft.AspNetCore.Http;

namespace RC.Deploy.Application
{
    /// <summary>
    /// GZip静态文件中间件
    /// </summary>
    public class GZipStaticFilesMiddleware(RequestDelegate next) : StaticFilesMiddlewareBase(next)
    {
        /// <summary>
        /// 接受编码
        /// </summary>
        protected override string AcceptEncoding => "gzip";
        /// <summary>
        /// 文件扩展名
        /// </summary>
        protected override string FileExtension => "zp";
    }
}
