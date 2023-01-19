using Materal.ConvertHelper;
using Materal.FileHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Materal.ConDep.Services.Models
{
    /// <summary>
    /// Web应用程序集合
    /// </summary>
    public class WebAppCollection
    {
        private readonly List<WebAppModel> apps;
        private readonly string _jsonFilePath;
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="jsonFilePath"></param>
        public WebAppCollection(string jsonFilePath)
        {
            _jsonFilePath = jsonFilePath;
            if (!File.Exists(_jsonFilePath))
            {
                File.WriteAllText(_jsonFilePath, "[]");
            }
            string jsonData = File.ReadAllText(_jsonFilePath);
            apps = jsonData.JsonToObject<List<WebAppModel>>();
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public async Task SaveDataAsync()
        {
            string jsonData = apps.ToJson();
            await TextFileManager.WriteTextAsync(_jsonFilePath, jsonData);
        }
        /// <summary>
        /// 总数
        /// </summary>
        public int Count => apps.Count;
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="app"></param>
        public void Add(WebAppModel app)
        {
            apps.Add(app);
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="app"></param>
        public void Remove(WebAppModel app)
        {
            apps.Remove(app);
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="id"></param>
        public void RemoveAt(Guid id)
        {
            WebAppModel app = apps.FirstOrDefault(m => m.ID == id);
            if (app != null)
            {
                Remove(app);
            }
        }
        /// <summary>
        /// ID索引
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WebAppModel this[Guid id] => apps.FirstOrDefault(m => m.ID == id);
        /// <summary>
        /// 获得应用列表
        /// </summary>
        /// <returns></returns>
        public List<WebAppModel> GetAppLists()
        {
            return apps;
        }
    }
}
