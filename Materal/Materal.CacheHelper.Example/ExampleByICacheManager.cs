using Materal.DateTimeHelper;
using System;
using System.Collections.Generic;

namespace Materal.CacheHelper.Example
{
    public class ExampleByICacheManager
    {
        #region SetBySliding
        public void SetBySlidingExample1()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            const double saveHours = 1;
            cacheManager.SetBySliding(cacheKey, inputString, saveHours);
            string cacheSaveString = cacheManager.Get<string>(cacheKey);
            Console.WriteLine(cacheSaveString);
        }
        public void SetBySlidingExample2()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            const double saveTime = 1;
            cacheManager.SetBySliding(cacheKey, inputString, saveTime, DateTimeTypeEnum.Day);
            string cacheSaveString = cacheManager.Get<string>(cacheKey);
            Console.WriteLine(cacheSaveString);
        }
        public void SetBySlidingExample3()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            DateTime saveTime = DateTime.Now.AddHours(1);
            cacheManager.SetBySliding(cacheKey, inputString, saveTime);
            string cacheSaveString = cacheManager.Get<string>(cacheKey);
            Console.WriteLine(cacheSaveString);
        }
        public void SetBySlidingExample4()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            var saveTimeSpan = new TimeSpan(4, 0, 0);
            cacheManager.SetBySliding(cacheKey, inputString, saveTimeSpan);
            string cacheSaveString = cacheManager.Get<string>(cacheKey);
            Console.WriteLine(cacheSaveString);
        }
        #endregion
        #region SetByAbsolute
        public void SetByAbsoluteExample1()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            const double saveHours = 1;
            cacheManager.SetByAbsolute(cacheKey, inputString, saveHours);
            string cacheSaveString = cacheManager.Get<string>(cacheKey);
            Console.WriteLine(cacheSaveString);
            Console.ReadKey();
        }
        public void SetByAbsoluteExample2()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            const double saveTime = 1;
            cacheManager.SetByAbsolute(cacheKey, inputString, saveTime, DateTimeTypeEnum.Day);
            string cacheSaveString = cacheManager.Get<string>(cacheKey);
            Console.WriteLine(cacheSaveString);
            Console.ReadKey();
        }
        public void SetByAbsoluteExample3()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            DateTime saveTime = DateTime.Now.AddHours(1);
            cacheManager.SetByAbsolute(cacheKey, inputString, saveTime);
            string cacheSaveString = cacheManager.Get<string>(cacheKey);
            Console.WriteLine(cacheSaveString);
            Console.ReadKey();
        }
        public void SetByAbsoluteExample4()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            var saveTimeSpan = new TimeSpan(4, 0, 0);
            cacheManager.SetByAbsolute(cacheKey, inputString, saveTimeSpan);
            string cacheSaveString = cacheManager.Get<string>(cacheKey);
            Console.WriteLine(cacheSaveString);
            Console.ReadKey();
        }
        #endregion
        #region Get
        public object GetExample1()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            const double saveHours = 1;
            cacheManager.SetByAbsolute(cacheKey, inputString, saveHours);
            object cacheSaveObject = cacheManager.Get(cacheKey);
            return cacheSaveObject;
        }
        public string GetExample2()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            const double saveHours = 1;
            cacheManager.SetByAbsolute(cacheKey, inputString, saveHours);
            string cacheSaveString = cacheManager.Get<string>(cacheKey);
            return cacheSaveString;
        }
        #endregion
        #region Remove
        public void RemoveExample1()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            const double saveHours = 1;
            cacheManager.SetByAbsolute(cacheKey, inputString, saveHours);
            cacheManager.Remove(cacheKey);
        }
        #endregion
        #region Clear
        public void ClearExample1()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            const double saveHours = 1;
            cacheManager.SetByAbsolute(cacheKey, inputString, saveHours);
            cacheManager.Clear();
        }
        #endregion
        #region GetCacheKeys
        public List<string> GetCacheKeysExample1()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            string inputString = Console.ReadLine();
            const string cacheKey = "MyKey";
            const double saveHours = 1;
            cacheManager.SetByAbsolute(cacheKey, inputString, saveHours);
            List<string> allKeys = cacheManager.GetCacheKeys();
            return allKeys;
        }
        #endregion
    }
}
