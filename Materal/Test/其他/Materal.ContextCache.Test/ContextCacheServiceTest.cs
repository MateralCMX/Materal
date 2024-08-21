using Materal.ContextCache.SqlitePersistence;

namespace Materal.ContextCache.Test
{
    [TestClass]
    public class ContextCacheServiceTest : BaseTest
    {
        public override void AddServices(IServiceCollection services)
        {
            //services.AddContextCacheByLocalFile();
            services.AddContextCacheBySqlite();
        }
        [TestMethod]
        public async Task TestSaveAsync()
        {
            IContextCacheService service = Services.GetRequiredService<IContextCacheService>();
            TestContext context = new() { NowIndex = 1, TargetIndex = 100 };
            IContextCache contextCache = service.BeginContextCache<TestRestorer, TestContext>(context);
            TestRestorer.Handler(contextCache, context);
            await Task.Delay(1000);
        }
        [TestMethod]
        public async Task TestRenewAsync()
        {
            IContextCacheService service = Services.GetRequiredService<IContextCacheService>();
            TestContext context = new() { NowIndex = 1, TargetIndex = 100 };
            service.BeginContextCache<TestRestorer, TestContext>(context);
            await service.RenewAsync();
            await Task.Delay(1000);
        }
    }
    public class TestContext
    {
        public int NowIndex { get; set; }
        public int TargetIndex { get; set; }
    }
    public class TestRestorer : IContextRestorer
    {
        public static void Handler(IContextCache contextCache, TestContext context)
        {
            while (context.NowIndex < context.TargetIndex)
            {
                Handler(context);
                //contextCache.NextStep();
                contextCache.NextStepAsync();
            }
            //contextCache.End();
            contextCache.EndAsync();
        }
        public static void Handler(TestContext context) => context.NowIndex++;
        public async Task RenewAsync(IContextCache contextCache)
        {
            if (contextCache.Context is not TestContext context) return;
            Handler(contextCache, context);
            await Task.CompletedTask;
        }
    }
}
