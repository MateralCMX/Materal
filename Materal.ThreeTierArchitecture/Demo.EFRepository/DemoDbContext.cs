using Demo.Domain;
using Demo.Domain.Views;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Demo.EFRepository
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
        }
        /// <summary>
        /// 学生
        /// </summary>
        public DbSet<Student> Student { get; set; }
        /// <summary>
        /// 班级
        /// </summary>
        public DbSet<Class> Class { get; set; }
        /// <summary>
        /// 学生信息视图
        /// </summary>
        public DbQuery<StudentInfoView> StudentInfoView { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
