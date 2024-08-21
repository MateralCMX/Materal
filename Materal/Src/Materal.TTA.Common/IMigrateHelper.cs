namespace Materal.TTA.Common
{
    /// <summary>
    /// 迁移帮助类
    /// </summary>
    public interface IMigrateHelper
    {
        /// <summary>
        /// 迁移
        /// </summary>
        /// <returns></returns>
        public Task MigrateAsync();
        /// <summary>
        /// 迁移
        /// </summary>
        /// <returns></returns>
        public void Migrate();
    }
}
