
using Materal.ConvertHelper;
using Materal.FileHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Materal.BlockchainHelper.Example
{
    /// <summary>
    /// 信息溯源服务
    /// </summary>
    public class InformationSourceService
    {
        public InformationSourceService()
        {
            Task<MBlock> task = ReadNewestBlockByFileAsync();
            Task.WaitAll(task);
            _nowBlock = task.Result;
        }
        /// <summary>
        /// 当前区块
        /// </summary>
        public MBlock _nowBlock;
        /// <summary>
        /// 记录新的信息
        /// </summary>
        /// <param name="message"></param>
        public async Task RecordNewMessage(string message)
        {
            if (_nowBlock == null)
            {
                _nowBlock = new MBlock(message);
            }
            else
            {
                var newBlock = new MBlock(_nowBlock, message);
                if (_nowBlock.IsNextBlock(newBlock))
                {
                    _nowBlock = newBlock;
                }
                else
                {
                    throw new MateralBlockchainException("记录数据失败");
                }
            }
            await SaveToJsonFileAsync(_nowBlock);
        }
        /// <summary>
        /// 数据存储文件路径
        /// </summary>
        private string _dataDirectoryPath
        {
            get
            {
                string result = Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? string.Empty, "BlockData");
                if (!Directory.Exists(result))
                {
                    Directory.CreateDirectory(result);
                }
                return result;
            }
        }
        /// <summary>
        /// 获得所有信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetAllInformationAsync()
        {
            var result = new List<string>();
            MBlock targetBlock = _nowBlock;
            while (targetBlock != null)
            {
                int index = targetBlock.Index;
                result.Insert(0, targetBlock.GetData<string>());
                targetBlock = await ReadBlockByFileAsync(targetBlock.Index - 1, targetBlock.PrevHash);
                if (targetBlock == null && index != 0)
                {
                    result.Clear();
                }
            }
            return result;
        }
        /// <summary>
        /// 读取区块文件
        /// </summary>
        /// <param name="index"></param>
        /// <param name="hash"></param>
        /// <returns></returns>m
        private async Task<MBlock> ReadBlockByFileAsync(int index, string hash)
        {
            string filePath = Path.Combine(_dataDirectoryPath, $"{index}_{hash}.json");
            if (!File.Exists(filePath))
            {
                return null;
            }
            string jsonData = await File.ReadAllTextAsync(filePath);
            MBlock block = MBlock.FormJson(jsonData);
            return block;
        }
        /// <summary>
        /// 读取最新的区块文件
        /// </summary>
        /// <returns></returns>
        private async Task<MBlock> ReadNewestBlockByFileAsync()
        {
            var index = 0;
            MBlock upBlock = null;
            while (true)
            {
                MBlock block = await ReadBlockByFileAsync(index++);
                if (block == null)
                {
                    return upBlock;
                }
                upBlock = block;
            }
        }
        /// <summary>
        /// 读取区块文件
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private async Task<MBlock> ReadBlockByFileAsync(int index)
        {
            var directoryInfo = new DirectoryInfo(_dataDirectoryPath);
            FileInfo[] fileInfos = directoryInfo.GetFiles($"{index}_*.json");
            if (fileInfos.Length != 1) return null;
            FileInfo fileInfo = fileInfos[0];
            string jsonData = await File.ReadAllTextAsync(fileInfo.FullName);
            MBlock block = MBlock.FormJson(jsonData);
            return block;
        }
        /// <summary>
        /// 保存到Json文件
        /// </summary>
        /// <param name="block"></param>
        private async Task SaveToJsonFileAsync(MBlock block)
        {
            string filePath = Path.Combine(_dataDirectoryPath, $"{block.Index}_{block.Hash}.json");
            string jsonString = block.ToJson();
            await TextFileManager.WriteTextAsync(filePath, jsonString);
        }
    }
}
