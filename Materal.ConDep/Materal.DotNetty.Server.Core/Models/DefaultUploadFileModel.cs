using System;
using System.IO;
using System.Text;
using DotNetty.Codecs.Http;

namespace Materal.DotNetty.Server.Core.Models
{
    public class DefaultUploadFileModel: IUploadFileModel
    {
        public DefaultUploadFileModel(IFullHttpRequest request)
        {
            Buffer = new byte[request.Content.ReadableBytes];
            request.Content.ReadBytes(Buffer);
            string readString = Encoding.UTF8.GetString(Buffer);
            int fileNameIndex = readString.IndexOf("filename=\"", StringComparison.Ordinal) + "filename=\"".Length;
            int contentTypeIndex = readString.IndexOf("Content-Type", StringComparison.Ordinal);
            if (fileNameIndex >= 0)
            {
                Name = readString.Substring(fileNameIndex, contentTypeIndex - fileNameIndex - 3);
            }
        }
        public byte[] Buffer { get; }
        public string Name { get; }
        public int Size => Buffer.Length;

        public void SaveAs(string path)
        {
            if (File.Exists(path)) File.Delete(path);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                stream.Write(Buffer, 0, Size);
            }
        }
    }
}
