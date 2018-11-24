using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Materal.ApplicationUpdate.Domain;
using Microsoft.EntityFrameworkCore;

namespace Materal.ApplicationUpdate.EFRepository
{
    public class AppUpdateContext : DbContext
    {
        public AppUpdateContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<ApplicationLog> ApplicationLog { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// 模型创建
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<Type> typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(q => q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null);

            foreach (Type type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
