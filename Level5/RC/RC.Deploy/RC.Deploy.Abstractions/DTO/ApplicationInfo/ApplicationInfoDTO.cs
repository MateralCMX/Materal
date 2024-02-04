namespace RC.Deploy.Abstractions.DTO.ApplicationInfo
{
    /// <summary>
    /// 应用程序信息数据传输模型
    /// </summary>
    public partial class ApplicationInfoDTO
    {
        /// <summary>
        /// 上传文件名称
        /// </summary>
        public string[] UploadFileNames { get; set; } = [];
    }
}
