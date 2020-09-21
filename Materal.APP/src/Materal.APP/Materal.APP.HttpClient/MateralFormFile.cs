using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Materal.APP.HttpClient
{
    public class MateralFormFile : IFormFile
    {
        public MultipartFormDataContent HttpContent { get; }
        public MateralFormFile(MemoryStream stream, string name, string fileName)
        {
            HttpContent = new MultipartFormDataContent();
            HttpContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            byte[] buffer = stream.GetBuffer();
            HttpContent.Add(new ByteArrayContent(buffer), name, fileName);
            //HttpContent.Add(new StreamContent(stream, Convert.ToInt32(stream.Length)), name, fileName);
        }
        public Stream OpenReadStream()
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Stream target)
        {
            throw new NotImplementedException();
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public string ContentType { get; set; }
        public string ContentDisposition { get; set; }
        public IHeaderDictionary Headers { get; set; }
        public long Length { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
    }
}
