using Materal.Oscillator.Abstractions.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Materal.Oscillator.Abstractions
{
    public abstract class OscillatorDBContext<T> : DbContext
        where T : OscillatorDBContext<T>
    {
        protected OscillatorDBContext(DbContextOptions<T> options) : base(options) { }
        /// <summary>
        /// 响应
        /// </summary>
        public DbSet<Answer> Answer { get; set; }
        /// <summary>
        /// 计划
        /// </summary>
        public DbSet<Plan> Plan { get; set; }
        /// <summary>
        /// 调度器
        /// </summary>
        public DbSet<Schedule> Schedule { get; set; }
        /// <summary>
        /// 调度器任务
        /// </summary>
        public DbSet<ScheduleWork> ScheduleWork { get; set; }
        /// <summary>
        /// 任务
        /// </summary>
        public DbSet<Work> Work { get; set; }
        /// <summary>
        /// 任务事件
        /// </summary>
        public DbSet<WorkEvent> WorkEvent { get; set; }
        #region 视图
        /// <summary>
        /// 响应视图
        /// </summary>
        public DbSet<AnswerView> AnswerView { get; set; }
        /// <summary>
        /// 计划视图
        /// </summary>
        public DbSet<PlanView> PlanView { get; set; }
        /// <summary>
        /// 调度器任务视图
        /// </summary>
        public DbSet<ScheduleWorkView> ScheduleWorkView { get; set; }
        /// <summary>
        /// 任务事件视图
        /// </summary>
        public DbSet<WorkEventView> WorkEventView { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Materal.Oscillator.SqliteRepositoryImpl"));
    }
}
