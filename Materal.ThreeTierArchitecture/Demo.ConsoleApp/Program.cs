using Demo.ConsoleApp.EFRepositories;
using Demo.ConsoleApp.EFRepositories.Repositories;
using Demo.ConsoleApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.ConsoleApp.Domain;

namespace Demo.ConsoleApp
{
    public class Program
    {
        public static async Task Main()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(ApplicationConfig.UserDB.ConnectionString, m => { m.EnableRetryOnFailure(); });
            }, ServiceLifetime.Transient);
            services.AddTransient<IUserUnitOfWork, UserUnitOfWorkImpl>();
            services.AddTransient<IUserRepository, UserSqlServerEFRepositoryImpl>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            Guid userID;
            #region 添加
            {
                Console.WriteLine("开始添加.....");
                var unitOfWork = serviceProvider.GetService<IUserUnitOfWork>();
                var user = new User
                {
                    Account = "Admin",
                    Name = "管理员",
                    Password = "123456"
                };
                unitOfWork.RegisterAdd(user);
                await unitOfWork.CommitAsync();
                userID = user.ID;
                Console.WriteLine("添加结束");
                await WriteUserInfosByReadDB(serviceProvider, users => users.Count > 0, "添加后从读库查询");
                await WriteUserInfosByMainDB(serviceProvider, "添加后从主库查询");
            }
            #endregion
            #region 修改
            {
                Console.WriteLine("开始修改.....");
                var unitOfWork = serviceProvider.GetService<IUserUnitOfWork>();
                var userRepository = serviceProvider.GetService<IUserRepository>();
                User userFromDB = await userRepository.FirstOrDefaultAsync(userID);
                userFromDB.Name = "Materal";
                userFromDB.UpdateTime = DateTime.Now;
                unitOfWork.RegisterEdit(userFromDB);
                await unitOfWork.CommitAsync();
                Console.WriteLine("修改结束");
                await WriteUserInfosByReadDB(serviceProvider, users => users.Count(m => m.Name == "Materal") > 0, "修改后从读库查询");
                await WriteUserInfosByMainDB(serviceProvider, "修改后从主库查询");
            }
            #endregion
            #region 删除
            {
                Console.WriteLine("开始删除.....");
                var unitOfWork = serviceProvider.GetService<IUserUnitOfWork>();
                var userRepository = serviceProvider.GetService<IUserRepository>();
                User userFromDB = await userRepository.FirstOrDefaultAsync(userID);
                unitOfWork.RegisterDelete(userFromDB);
                await unitOfWork.CommitAsync();
                Console.WriteLine("删除结束");
                await WriteUserInfosByReadDB(serviceProvider, users => users.Count == 0, "删除后从读库查询");
                await WriteUserInfosByMainDB(serviceProvider, "删除后从主库查询");
            }
            #endregion
        }
        /// <summary>
        /// 从读库展示用户信息
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="isOK"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static async Task WriteUserInfosByReadDB(IServiceProvider serviceProvider, Func<List<User>, bool> isOK, string message = "从读库查询")
        {
            Console.WriteLine(message);
            for (var i = 0; i < 100; i++)
            {
                var userRepository = serviceProvider.GetService<IUserRepository>();
                List<User> userFromDbs = await userRepository.FindFromSubordinateAsync(m => true);
                if (!isOK.Invoke(userFromDbs))
                {
                    await Task.Delay(500);
                    continue;
                }
                foreach (User userFromDB in userFromDbs)
                {
                    WriteUserInfo(userFromDB);
                }
                Console.WriteLine($"同步耗时:{i * 500}ms");
                break;
            }
        }
        /// <summary>
        /// 从主库展示用户信息
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static async Task WriteUserInfosByMainDB(IServiceProvider serviceProvider, string message = "从主库查询")
        {
            Console.WriteLine(message);
            var userRepository = serviceProvider.GetService<IUserRepository>();
            List<User> userFromDbs = await userRepository.FindAsync(m => true);
            foreach (User userFromDB in userFromDbs)
            {
                WriteUserInfo(userFromDB);
            }
        }
        /// <summary>
        /// 展示用户信息
        /// </summary>
        /// <param name="user"></param>
        private static void WriteUserInfo(User user)
        {
            Console.WriteLine($"唯一标识:{user.ID},账号:{user.Account},名称:{user.Name}");
        }
    }
}
