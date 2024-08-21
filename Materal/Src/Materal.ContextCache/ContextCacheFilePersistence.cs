using Newtonsoft.Json;
using System.Text;

namespace Materal.ContextCache
{
    /// <summary>
    /// 上下文缓存文件持久化
    /// </summary>
    public class ContextCacheFilePersistence : IContextCachePersistence
    {
        private const string _savePath = "Temp/ContextCache";
        private const string _fileExtension = "ContextCache";
        private readonly Encoding _encoding = Encoding.UTF8;
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="groupModel"></param>
        /// <param name="model"></param>
        public virtual void Save(ContextCacheGroupModel groupModel, ContextCacheModel model)
        {
            string filePath = GetContextCacheFilePath(groupModel.ID);
            FileInfo fileInfo = new(filePath);
            StringBuilder contentBuilder = new();
            if (!fileInfo.Exists)
            {
                string content = JsonConvert.SerializeObject(groupModel);
                contentBuilder.AppendLine(content);
            }
            (string _, StringBuilder modelContentBuilder) = GetSaveInfo(model);
            contentBuilder.Append(modelContentBuilder);
            File.AppendAllText(filePath, contentBuilder.ToString(), _encoding);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="groupModel"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task SaveAsync(ContextCacheGroupModel groupModel, ContextCacheModel model)
        {
            string filePath = GetContextCacheFilePath(groupModel.ID);
            FileInfo fileInfo = new(filePath);
            StringBuilder contentBuilder = new();
            if (!fileInfo.Exists)
            {
                string content = JsonConvert.SerializeObject(groupModel);
                contentBuilder.AppendLine(content);
            }
            (string _, StringBuilder modelContentBuilder) = GetSaveInfo(model);
            contentBuilder.Append(modelContentBuilder);
#if NETSTANDARD2_0
            File.AppendAllText(filePath, contentBuilder.ToString(), _encoding);
            await Task.CompletedTask;
#else
            await File.AppendAllTextAsync(filePath, contentBuilder.ToString(), _encoding);
#endif
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        public virtual void Save(ContextCacheModel model)
        {
            (string savePath, StringBuilder contentBuilder) = GetSaveInfo(model);
            File.AppendAllText(savePath, contentBuilder.ToString(), _encoding);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task SaveAsync(ContextCacheModel model)
        {
            (string savePath, StringBuilder contentBuilder) = GetSaveInfo(model);
#if NETSTANDARD2_0
            File.AppendAllText(savePath, contentBuilder.ToString(), _encoding);
            await Task.CompletedTask;
#else
            await File.AppendAllTextAsync(savePath, contentBuilder.ToString(), _encoding);
#endif
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="groupID"></param>
        public virtual void Remove(Guid groupID)
        {
            FileInfo fileInfo = GetContextCacheFileInfo(groupID);
            if (!fileInfo.Exists) return;
            fileInfo.Delete();
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(Guid groupID)
        {
            Remove(groupID);
            await Task.CompletedTask;
        }
        /// <summary>
        /// 获得所有分组信息
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<ContextCacheGroupModel> GetAllGroupInfo()
        {
            DirectoryInfo directoryInfo = GetContextCacheDirectoryInfo();
            FileInfo[] fileInfos = directoryInfo.GetFiles($"*.{_fileExtension}");
            foreach (FileInfo fileInfo in fileInfos)
            {
                string[] fileDatas = File.ReadAllLines(fileInfo.FullName, _encoding);
                ContextCacheGroupModel result = GetGroupInfo(fileDatas);
                yield return result;
            }
        }
        /// <summary>
        /// 获得保存信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private (string, StringBuilder) GetSaveInfo(ContextCacheModel model)
        {
            FileInfo fileInfo = GetContextCacheFileInfo(model.GroupID);
            StringBuilder contentBuilder = new();
            string content = JsonConvert.SerializeObject(model);
            contentBuilder.AppendLine(content);
            return (fileInfo.FullName, contentBuilder);
        }
        /// <summary>
        /// 获得分组信息
        /// </summary>
        /// <param name="fileDatas"></param>
        /// <returns></returns>
        private static ContextCacheGroupModel GetGroupInfo(string[] fileDatas)
        {
            ContextCacheGroupModel result = fileDatas.First().JsonToObject<ContextCacheGroupModel>();
            result.ContextCacheData = [];
#if NETSTANDARD2_0
            fileDatas = fileDatas.Skip(1).ToArray();
#else
            fileDatas = fileDatas[1..];
#endif
            List<ContextCacheModel> contextCaches = fileDatas.Select(m => m.JsonToObject<ContextCacheModel>()).ToList();
            if (contextCaches.Count <= 0) return result;
            ContextCacheModel? nowContextCache = contextCaches.FirstOrDefault(m => m.UpID is null);
            while (nowContextCache is not null)
            {
                result.ContextCacheData.Add(nowContextCache);
                nowContextCache = contextCaches.FirstOrDefault(m => m.UpID == nowContextCache.ID);
            }
            return result;
        }
        /// <summary>
        /// 获得上下文缓存目录
        /// </summary>
        /// <returns></returns>
        private DirectoryInfo GetContextCacheDirectoryInfo()
        {
            string path = Path.Combine(GetType().Assembly.GetDirectoryPath(), _savePath);
            DirectoryInfo directoryInfo = new(path);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
                directoryInfo.Refresh();
            }
            return directoryInfo;
        }
        /// <summary>
        /// 获得上下文缓存文件
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        private FileInfo GetContextCacheFileInfo(Guid groupID)
        {
            FileInfo fileInfo = new(GetContextCacheFilePath(groupID));
            if (!fileInfo.Exists)
            {
                FileStream fileStream = fileInfo.Create();
                fileStream.Close();
                fileStream.Dispose();
                fileInfo.Refresh();
            }
            return fileInfo;
        }
        /// <summary>
        /// 获得上下文缓存文件路径
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        private string GetContextCacheFilePath(Guid groupID)
        {
            DirectoryInfo directoryInfo = GetContextCacheDirectoryInfo();
            string result = Path.Combine(directoryInfo.FullName, $"{groupID}.{_fileExtension}");
            return result;
        }
    }
}
