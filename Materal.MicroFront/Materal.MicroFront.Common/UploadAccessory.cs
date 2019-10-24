using System;
using System.IO;
using System.Threading.Tasks;

namespace Materal.MicroFront.Common
{
    /// <summary>
    /// 附件上传
    /// </summary>
    public sealed class UploadAccessory
    {
        //300MB大小限制
        public const int AccessorySize = 300 * 1024 * 1024;//大小限制 //TODO:可配置
        /// <summary>
        /// 获得路径
        /// </summary>
        /// <param name="uploadPath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        (string basePath, string path) GetPath(string uploadPath, string fileName)
        {
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            var path = $"{uploadPath}\\{fileName}";
            return (uploadPath, path);
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="fileLength"></param>
        /// <param name="uploadPath"></param>
        /// <param name="canVerifySize"></param>
        /// <param name="fileName"></param>
        private void Verify(long fileLength, string uploadPath, bool canVerifySize, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new InvalidOperationException("文件名为空！");
            }
            if (string.IsNullOrEmpty(uploadPath))
            {
                throw new InvalidOperationException("存储路径不能为空！");
            }
            if (canVerifySize && fileLength > AccessorySize)
            {
                throw new InvalidOperationException("上传文件大小超过限制！");
            }
        }
        /// <summary>
        /// 根据相对路径返回保存在数据库中的相对路径(自动生成文件名)
        /// 例如：传入“/picture/kunming/2004”返回"Upload/picture/km/2004/12324343434.jpg" 
        /// </summary>
        /// <param name="fileContent">文件流</param>
        /// <param name="uploadPath">相对路径（如：/picture/km/2004）</param>
        /// <param name="canVerifySize"></param>
        /// <param name="fileName"></param>
        /// <returns>图片保存成功后返回的路径（即存储在数据库中的url）</returns>
        public async Task<(string fileName, string path, long size)> SaveAccessoryAsync(byte[] fileContent, string uploadPath, string fileName, bool canVerifySize = true)
        {
            Verify(fileContent.Length, uploadPath, canVerifySize, fileName);
            (string basePath, string path) = GetPath(uploadPath, fileName);
            using (FileStream fileStream = File.Create(Path.Combine(basePath, path)))
            {
                await fileStream.WriteAsync(fileContent, 0, fileContent.Length);
                await fileStream.FlushAsync();
            }
            return (fileName, path, fileContent.Length);
        }
    }
}
