using DotNetty.Transport.Channels;
using Materal.ConDep.Commands;
using Materal.ConDep.Common;
using Materal.ConDep.Common.Extension;
using Materal.ConDep.Events;
using Materal.ConDep.Manager;
using Materal.ConDep.Services.Models;
using Materal.WindowsHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Materal.ConDep.Services
{
    public class UploadPoolServiceImpl : IUploadPoolService
    {
        private readonly string workingDirectory;
        private readonly Timer timer = new Timer(60000);
        private readonly Dictionary<IChannel, IFileModel> _uploadPool = new Dictionary<IChannel, IFileModel>();
        public UploadPoolServiceImpl()
        {
            workingDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}Application\\";
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
                (string _, string path, long _) = await new UploadAccessory().SaveAccessoryAsync(uploadVideoModel.FileContent, $"{workingDirectory}\\Backup", uploadVideoModel.FileName, false);
                await UpdateAppFileAsync(path);
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
        /// <returns></returns>
        private async Task UpdateAppFileAsync(string path)
        {
            var appManager = ApplicationData.GetService<IAppManager>();
            string tempPath = $"{workingDirectory}Temp";
            DirectoryInfo tempDirectoryInfo = null;
            if (!Directory.Exists(tempPath)) tempDirectoryInfo = Directory.CreateDirectory(tempPath);
            if (tempDirectoryInfo == null) tempDirectoryInfo = new DirectoryInfo(tempPath);
            var cmdManager = new CmdManager();
            await cmdManager.RunCmdCommandsAsync($"unrar x -o+ -y {path} {tempPath}");
            DirectoryInfo[] directoryInfos = tempDirectoryInfo.GetDirectories();
            string[] paths = directoryInfos.Select(m => m.Name).ToArray();
            appManager.StopAppByPaths(paths);
            foreach (DirectoryInfo directoryInfo in directoryInfos)
            {
                string dirPath = $"{workingDirectory}{directoryInfo.Name}";
                CopyDirectory(directoryInfo, dirPath);
                directoryInfo.Delete();
            }
        }

        private void CopyDirectory(DirectoryInfo directoryInfo, string targetPath)
        {
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
