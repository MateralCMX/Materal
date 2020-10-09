using System;
using System.Threading.Tasks;

namespace Materal.RedisHelper.Example
{
    public class Program
    {
        public static void Main()
        {
            var config = new RedisConfigModel
            {
                Host = "127.0.0.1",
                Port = "6379"
            };
            var redisManager = new RedisManager(config);
            ConsoleKeyInfo inputKey;
            do
            {
                Console.WriteLine("请选择一个演示：");
                Console.WriteLine("1.阻塞锁");
                Console.WriteLine("2.非阻塞锁");
                inputKey = Console.ReadKey();
                Console.WriteLine();
            } while (inputKey.Key != ConsoleKey.D1 && inputKey.Key != ConsoleKey.D2);
            Parallel.For(0, 10, async index =>
            {
                switch (inputKey.Key)
                {
                    case ConsoleKey.D1:
                        await ShowBlockingLockAsync(redisManager, index);
                        break;
                    case ConsoleKey.D2:
                        await ShowNonBlockingLockAsync(redisManager, index);
                        break;
                }
            });
            Console.ReadKey();
        }
        /// <summary>
        /// 演示阻塞锁
        /// </summary>
        /// <param name="redisManager"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static async Task ShowBlockingLockAsync(RedisManager redisManager, int index)
        {
            Console.WriteLine($"{index} Start");
            await using RedisLock redisLock = await redisManager.GetBlockingLockAsync("LockKey", 10000);
            await Task.Delay(1000);
            Console.WriteLine($"{index} End");
        }
        /// <summary>
        /// 演示阻塞锁
        /// </summary>
        /// <param name="redisManager"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static async Task ShowNonBlockingLockAsync(RedisManager redisManager, int index)
        {
            Console.WriteLine($"{index} Start");
            await using RedisLock redisLock = await redisManager.GetNonBlockingLockAsync("LockKey", 10000);
            if (redisLock == null) return;
            Console.WriteLine($"{index} End");
        }
    }
}
