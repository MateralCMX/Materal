using DotNetty.Transport.Channels;
using Materal.MicroFront.Commands;
using Materal.MicroFront.Common;
using Materal.MicroFront.Common.Extension;
using Materal.MicroFront.Events;
using Materal.Services.Models;
using Materal.StringHelper;
using Materal.WindowsHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Materal.Services
{
    public class UploadPoolServiceImpl : IUploadPoolService
    {
        private readonly string workingDirectory;
        private readonly Timer timer = new Timer(60000);
        private readonly Dictionary<IChannel, IFileModel> _uploadPool = new Dictionary<IChannel, IFileModel>();
        public UploadPoolServiceImpl()
        {
            workingDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}HtmlPages\\";
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            List<IChannel> list = _uploadPool.Where(m => !m.Key.Open || m.Value.AutoDestroy).Select(m => m.Key).Distinct().ToList();
            foreach (IChannel channel in list)
            {
                _uploadPool.Remove(channel);
            }
        }
        public async Task NewUpload(IChannel channel, UploadStartCommand command)
        {
            int uploadingFileCount = _uploadPool.Count(m => m.Value.FileAbstract.Equals(command.Abstract));
            if (uploadingFileCount > 0) throw new InvalidOperationException("该文件正在上传");
            string fileName = Path.Combine(workingDirectory, command.Name);
            if (File.Exists(fileName)) File.Delete(fileName);
            IFileModel fileModel = new MemoryFileModel
            {
                FileAbstract = command.Abstract,
                FileName = command.Name,
                Size = command.Size
            };
            _uploadPool.Add(channel, fileModel);
            var @event = new UploadReadyEvent();
            await channel.SendJsonEventAsync(@event);
        }
        public async Task LoadBuffer(IChannel channel, UploadPartCommand command)
        {
            if (!_uploadPool.ContainsKey(channel)) throw new InvalidOperationException("尚未准备完毕");
            if (string.IsNullOrEmpty(command.Base64Buffer)) throw new InvalidOperationException("数据为空");
            byte[] buffy = Convert.FromBase64String(command.Base64Buffer);
            IFileModel uploadVideoModel = _uploadPool[channel];
            if (uploadVideoModel == null) throw new InvalidOperationException("不存在该文件");
            uploadVideoModel.LoadBuffer(buffy, command.Index);
            if (uploadVideoModel.CanComplete)
            {
                (string _, string path, long _) = await new UploadAccessory().SaveAccessoryAsync(uploadVideoModel.FileContent, $"{AppDomain.CurrentDomain.BaseDirectory}//Backup", uploadVideoModel.FileName, false);
                await UpdateWebFileAsync(path, channel);
                new HttpHandler().ClearCache();
                var @event = new UploadEndEvent();
                await channel.SendJsonEventAsync(@event);
                _uploadPool.Remove(channel);
            }
            else
            {
                var @event = new UploadReadyEvent();
                await channel.SendJsonEventAsync(@event);
            }
        }

        /// <summary>
        /// 更新APP文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        private async Task UpdateWebFileAsync(string path, IChannel channel)
        {
            var fileService = ApplicationData.GetService<IWebFileService>();
            string tempPath = $"{AppDomain.CurrentDomain.BaseDirectory}Temp/{StringManager.GetRandomStrByGuid()}";
            DirectoryInfo tempDirectoryInfo = null;
            if (!Directory.Exists(tempPath)) tempDirectoryInfo = Directory.CreateDirectory(tempPath);
            if (tempDirectoryInfo == null) tempDirectoryInfo = new DirectoryInfo(tempPath);
            var cmdManager = new CmdManager();
            await cmdManager.RunCmdCommandsAsync($"unrar x -o+ -y {path} {tempPath}");
            DirectoryInfo[] directoryInfos = tempDirectoryInfo.GetDirectories();
            foreach (DirectoryInfo directoryInfo in directoryInfos)
            {
                try
                {
                    string targetPath = $"{workingDirectory}{directoryInfo.Name}";
                    CopyDirectory(directoryInfo, targetPath);
                    fileService.InitServices();
                }
                catch (Exception ex)
                {
                    var @event = new ServerErrorEvent
                    {
                        Status = 500,
                        Message = ex.Message
                    };
                    await channel.SendJsonEventAsync(@event);
                }
                finally
                {
                    directoryInfo.Delete(true);
                }
            }
            tempDirectoryInfo.Delete(true);
        }
        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="targetPath"></param>
        private void CopyDirectory(DirectoryInfo directoryInfo, string targetPath)
        {
            if (Directory.Exists(targetPath)) Directory.Delete(targetPath, true);
            if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            #region CopyFile
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            foreach (FileInfo fileInfo in fileInfos)
            {
                string filePath = Path.Combine(targetPath, fileInfo.Name);
                if (File.Exists(filePath)) File.Delete(filePath);
                File.Move(fileInfo.FullName, filePath);
            }
            #endregion
            #region CopyChildDirectory
            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
            foreach (DirectoryInfo info in directoryInfos)
            {
                string dirPath = Path.Combine(targetPath, info.Name);
                if (Directory.Exists(dirPath)) Directory.Delete(dirPath, true);
                Directory.Move(info.FullName, dirPath);
            }
            #endregion
        }
    }
}
