using System.Security.Cryptography;

namespace Materal.Extensions
{
    /// <summary>
    /// 流扩展
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// 获得MD5加密结果(32位)
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="isLower"></param>
        /// <returns></returns>
        public static string ToMd5_32Encode(this Stream stream, bool isLower = false)
        {
            MD5 md5 = MD5.Create();
            byte[] resultValue = md5.ComputeHash(stream);
            StringBuilder stringBuilder = new();
            foreach (byte value in resultValue)
            {
                stringBuilder.Append(value.ToString("x2"));
            }
            string result = stringBuilder.ToString();
            result = isLower ? result.ToLower() : result.ToUpper();
            return result;
        }
        /// <summary>
        /// 获得MD5加密结果(16位)
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="isLower"></param>
        /// <returns></returns>
        public static string ToMd5_16Encode(this Stream stream, bool isLower = false) => stream.ToMd5_32Encode(isLower).Substring(8, 16);
        /// <summary>
        /// 转换为Base64
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ToBase64(this Stream stream)
        {
            byte[] fileArray = new byte[stream.Length];
            stream.Position = 0;
#if NETSTANDARD
            stream.Read(fileArray, 0, (int)stream.Length);
#else
            stream.ReadExactly(fileArray);
#endif
            return Convert.ToBase64String(fileArray);
        }
        /// <summary>
        /// 转换为Base64图片
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="ExtensionException"></exception>
        public static string ToBase64Image(this Stream fileStream)
        {
            if (!fileStream.IsImage(out string? imageType) || string.IsNullOrWhiteSpace(imageType)) throw new ExtensionException("不是图片");
            string fileBase64Content = fileStream.ToBase64();
            string result = imageType switch
            {
                "JPG|JPEG" => "data:image/jpeg;base64," + fileBase64Content,
                "PNG" => "data:image/png;base64," + fileBase64Content,
                "GIF" => "data:image/gif;base64," + fileBase64Content,
                "BMP" => "data:image/bmp;base64," + fileBase64Content,
                "TIFF" => "data:image/tiff;base64," + fileBase64Content,
                "ICO" => "data:image/x-icon;base64," + fileBase64Content,
                _ => throw new ExtensionException("图片类型不支持"),
            };
            return result;
        }
        /// <summary>
        /// 是否是图片文件
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static bool IsImage(this Stream stream) => IsImage(stream, out string? _);
        /// <summary>
        /// 是否是图片文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="imageType"></param>
        /// <returns></returns>
        public static bool IsImage(this Stream stream, out string? imageType)
        {
            imageType = null;
            stream.Position = 0;
            try
            {
                using BinaryReader binaryReader = new(stream);
                Dictionary<string, List<byte[]>> signatures = [];
                signatures.Add("JPG|JPEG", [[0xFF, 0xD8, 0xFF, 0xE0], [0xFF, 0xD8, 0xFF, 0xE1], [0xFF, 0xD8, 0xFF, 0xE8]]);
                signatures.Add("PNG", [[0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A]]);
                signatures.Add("GIF", [[0x47, 0x49, 0x46, 0x38, 0x37, 0x61], [0x47, 0x49, 0x46, 0x38, 0x39, 0x61]]);
                signatures.Add("BMP", [[0x42, 0x4D]]);
                signatures.Add("TIFF", [[0x49, 0x49, 0x2A, 0x00], [0x4D, 0x4D, 0x00, 0x2A]]);
                signatures.Add("ICO", [[0x00, 0x00, 0x01, 0x00], [0x00, 0x00, 0x02, 0x00]]);
                int maxLength = 0;
                foreach (KeyValuePair<string, List<byte[]>> item in signatures)
                {
                    int temp = item.Value.Max(m => m.Length);
                    if (temp > maxLength)
                    {
                        maxLength = temp;
                    }
                }
                if (maxLength > stream.Length)
                {
                    maxLength = (int)stream.Length;
                }
                byte[] headerBytes = binaryReader.ReadBytes(maxLength);
                foreach (KeyValuePair<string, List<byte[]>> signature in signatures)
                {
                    foreach (byte[] signatureItem in signature.Value)
                    {
                        if (headerBytes.Length < signatureItem.Length) continue;
                        bool result = signatureItem.SequenceEqual(headerBytes.Take(signatureItem.Length));
                        if (result)
                        {
                            imageType = signature.Key;
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
