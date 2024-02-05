using Microsoft.EntityFrameworkCore.Design;
using RC.EnvironmentServer.Repository;

namespace RC.ServerCenter.Repository
{
    /// <summary>
    /// ServerCenter数据库上下文工厂
    /// </summary>
    public class EnvironmentServerDBContextFactory : BaseDBContextFactory<EnvironmentServerDBContext>, IDesignTimeDbContextFactory<EnvironmentServerDBContext>
    {
        /// <summary>
        /// 数据库路径
        /// </summary>
        protected override string DBPath => "./ServerCenter.db";
    }
}
