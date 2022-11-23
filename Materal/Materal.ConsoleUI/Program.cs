using Materal.CacheHelper;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            ICacheManager cacheManager = new MemoryCacheManager();
            cacheManager.SetBySliding("1234", "2234", 1);
        }
    }
}
