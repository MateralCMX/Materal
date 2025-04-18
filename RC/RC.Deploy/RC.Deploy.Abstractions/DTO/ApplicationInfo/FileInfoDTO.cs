﻿namespace RC.Deploy.Abstractions.DTO.ApplicationInfo
{
    /// <summary>
    /// 文件信息数据传输模型
    /// </summary>
    public class FileInfoDTO
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 最后写入时间
        /// </summary>
        public DateTime LastWriteTime { get; set; }
        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownloadUrl { get; set; } = string.Empty;
    }
}
