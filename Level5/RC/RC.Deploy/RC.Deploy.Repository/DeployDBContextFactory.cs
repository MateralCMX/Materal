using Microsoft.EntityFrameworkCore.Design;

namespace RC.Deploy.Repository
{
    /// <summary>
    /// Deploy数据库上下文工厂
    /// </summary>
    public class DeployDBContextFactory : BaseDBContextFactory<DeployDBContext>, IDesignTimeDbContextFactory<DeployDBContext>
    {
        /// <summary>
        /// 数据库路径
        /// </summary>
        protected override string DBPath => "./Deploy.db";
    }
}
