using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Authority.Domain;
using Authority.Domain.Views;
namespace Authority.EFRepository
{
    /// <summary>
    /// Authority数据库上下文
    /// </summary>
    public sealed class AuthorityDbContext : DbContext
    {
        public AuthorityDbContext(DbContextOptions<AuthorityDbContext> options) : base(options)
        {
        }
        /// <summary>
        /// 功能权限
        /// </summary>
        public DbSet<ActionAuthority> ActionAuthority { get; set; }
        /// <summary>
        /// API权限
        /// </summary>
        public DbSet<APIAuthority> APIAuthority { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<Role> Role { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> User { get; set; }
        /// <summary>
        /// 网页菜单权限
        /// </summary>
        public DbSet<WebMenuAuthority> WebMenuAuthority { get; set; }
        /// <summary>
        /// 角色功能权限
        /// </summary>
        public DbSet<RoleActionAuthority> RoleActionAuthority { get; set; }
        /// <summary>
        /// 角色API权限
        /// </summary>
        public DbSet<RoleAPIAuthority> RoleAPIAuthority { get; set; }
        /// <summary>
        /// 角色网页菜单权限
        /// </summary>
        public DbSet<RoleWebMenuAuthority> RoleWebMenuAuthority { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public DbSet<UserRole> UserRole { get; set; }
        /// <summary>
        /// 用户拥有的API权限
        /// </summary>
        public DbQuery<UserOwnedAPIAuthority> UserOwnedAPIAuthority { get; set; }
        /// <summary>
        /// 用户拥有的功能权限
        /// </summary>
        public DbQuery<UserOwnedActionAuthority> UserOwnedActionAuthority { get; set; }
        /// <summary>
        /// 用户拥有的网页菜单权限
        /// </summary>
        public DbQuery<UserOwnedWebMenuAuthority> UserOwnedWebMenuAuthority { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
