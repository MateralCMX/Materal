using Materal.DateTimeHelper;
using System;

namespace Materal.CacheHelper.Example
{
    public static class ICacheManagerExample
    {
        public static void SetBySlidingExample1()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            const double saveHours = 1;
            cacheManager.SetBySliding(cacheKey, inputString, saveHours);
            string cacheSaveString = cacheManager.Get<string>(cacheKey);
            Console.WriteLine(cacheSaveString);
            Console.ReadKey();
        }
        public static void SetBySlidingExample2()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            const double saveTime = 1;
            cacheManager.SetBySliding(cacheKey, inputString, saveTime, DateTimeTypeEnum.Day);
            string cacheSaveString = cacheManager.Get<string>(cacheKey);
            Console.WriteLine(cacheSaveString);
            Console.ReadKey();
        }
        public static void SetBySlidingExample3()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            DateTime saveTime = DateTime.Now.AddHours(1);
            cacheManager.SetBySliding(cacheKey, inputString, saveTime);
            string cacheSaveString = cacheManager.Get<string>(cacheKey);
            Console.WriteLine(cacheSaveString);
            Console.ReadKey();
        }
        public static void SetBySlidingExample4()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            var saveTimeSpan = new TimeSpan(4, 0, 0);
            cacheManager.SetBySliding(cacheKey, inputString, saveTimeSpan);
            string cacheSaveString = cacheManager.Get<string>(cacheKey);
            Console.WriteLine(cacheSaveString);
            Console.ReadKey();
        }
    }
}
