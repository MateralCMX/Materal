using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.SqlServerEFRepository
{
    /// <summary>
    /// Oscillator数据库上下文
    /// </summary>
    public class OscillatorDBContext : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="options"></param>
        public OscillatorDBContext(DbContextOptions<OscillatorDBContext> options) : base(options) { }
        /// <summary>
        /// 响应
        /// </summary>
        public DbSet<Answer>? Answer { get; set; }
        /// <summary>
        /// 计划
        /// </summary>
        public DbSet<Plan>? Plan { get; set; }
        /// <summary>
        /// 调度器
        /// </summary>
        public DbSet<Schedule>? Schedule { get; set; }
        /// <summary>
        /// 调度器任务
        /// </summary>
        public DbSet<ScheduleWork>? ScheduleWork { get; set; }
        /// <summary>
        /// 任务
        /// </summary>
        public DbSet<Work>? Work { get; set; }
        /// <summary>
        /// 模型创建配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Materal.Oscillator.SqlServerEFRepository"));
    }
}
