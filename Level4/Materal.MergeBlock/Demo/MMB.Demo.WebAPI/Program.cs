using Materal.MergeBlock;

namespace MMB.Demo.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await MergeBlockProgram.RunAsync(args);
            //var builder = WebApplication.CreateBuilder(args);

            //// Add services to the container.

            //builder.Services.AddControllers();

            //var app = builder.Build();

            //// Configure the HTTP request pipeline.

            //app.UseAuthorization();


            //app.MapControllers();

            //app.Run();
        }
    }
}
