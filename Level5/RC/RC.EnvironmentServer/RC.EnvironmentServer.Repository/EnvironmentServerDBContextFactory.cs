using Microsoft.EntityFrameworkCore.Design;

namespace RC.EnvironmentServer.Repository
{
    /// <summary>
    /// EnvironmentServer数据库上下文工厂
    /// </summary>
    public class EnvironmentServerDBContextFactory : BaseDBContextFactory<EnvironmentServerDBContext>, IDesignTimeDbContextFactory<EnvironmentServerDBContext>
    {
        /// <summary>
        /// 数据库路径
        /// </summary>
        protected override string DBPath => "./EnvironmentServer.db";
    }
}
