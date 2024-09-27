using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Materal.TTA.Test
{
    public class TestService(TestDBContext testDBContext) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            User? dbUser = await testDBContext.User.FirstOrDefaultAsync(m => true);
            if (dbUser is null) return;
            User? dbUser2 = await testDBContext.User.FirstOrDefaultAsync(m => m.ID.Equals(dbUser.ID));
        }

        public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
    }
}
