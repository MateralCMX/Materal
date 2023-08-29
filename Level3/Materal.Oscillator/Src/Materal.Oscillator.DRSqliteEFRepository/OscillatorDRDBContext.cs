using Materal.Oscillator.Abstractions.DR.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Materal.Oscillator.DRSqliteEFRepository
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public sealed class OscillatorDRDBContext : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="options"></param>
        public OscillatorDRDBContext(DbContextOptions<OscillatorDRDBContext> options) : base(options) { }
        /// <summary>
        /// 流程
        /// </summary>
        public DbSet<Flow>? Flow { get; set; }
        /// <summary>
        /// 模型配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Materal.Oscillator.DRSqliteEFRepository"));
    }
}
