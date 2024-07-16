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
            IContextCache contextCache = await service.BeginContextCacheAsync<TestRestorer, TestContext>(context);
            await TestRestorer.HandlerAsync(contextCache, context);
        }
        [TestMethod]
        public async Task TestRenew()
        {
            IContextCacheService service = Services.GetRequiredService<IContextCacheService>();
            TestContext context = new() { NowIndex = 1, TargetIndex = 100 };
            await service.BeginContextCacheAsync<TestRestorer, TestContext>(context);
            await service.RenewAsync();
        }
    }
    public class TestContext
    {
        public int NowIndex { get; set; }
        public int TargetIndex { get; set; }
    }
    public class TestRestorer : IContextRestorer
    {
        public static async Task HandlerAsync(IContextCache contextCache, TestContext context)
        {
            while (context.NowIndex < context.TargetIndex)
            {
                Handler(context);
                await contextCache.NextStepAsync();
            }
            await contextCache.EndAsync();
        }
        public static void Handler(TestContext context) => context.NowIndex++;
        public async Task RenewAsync(IContextCache contextCache)
        {
            if (contextCache.Context is not TestContext context) return;
            await HandlerAsync(contextCache, context);
        }
    }
}
