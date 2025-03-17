namespace Materal.Tools.Core.NugegtPackages
{
    /// <summary>
    /// 包Props文件服务
    /// </summary>
    public interface IPackagesPropsService
    {
        /// <summary>
        /// 排序和去重
        /// </summary>
        /// <param name="filePath"></param>
        void SortAndRemoveDuplicates(string filePath);
        /// <summary>
        /// 排序和去重
        /// </summary>
        /// <param name="fileInfo"></param>
        void SortAndRemoveDuplicates(FileInfo fileInfo);
    }
}
