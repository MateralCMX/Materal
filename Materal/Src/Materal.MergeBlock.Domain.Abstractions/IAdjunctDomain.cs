namespace Materal.MergeBlock.Domain.Abstractions
{
    /// <summary>
    /// 附件Domain
    /// </summary>
    public interface IAdjunctDomain : IDomain
    {
        /// <summary>
        /// 上传文件唯一标识
        /// </summary>
        Guid UploadFileID { get; set; }
    }
}
