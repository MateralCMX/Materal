using Microsoft.EntityFrameworkCore.Design;

namespace RC.ServerCenter.Repository
{
    /// <summary>
    /// ServerCenter数据库上下文工厂
    /// </summary>
    public class ServerCenterDBContextFactory : BaseDBContextFactory<ServerCenterDBContext>, IDesignTimeDbContextFactory<ServerCenterDBContext>
    {
        /// <summary>
        /// 数据库路径
        /// </summary>
        protected override string DBPath => "./ServerCenter.db";
    }
}
