using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IntegratedPlatform.WebAPICore.Filters
{
    public class DeleteTempFileFilter : Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            await next.Invoke();
            if (context.Result is FileStreamResult fileStreamResult && fileStreamResult.FileStream is FileStream fileStream)
            {
                string fileName = fileStream.Name;
                fileStream.Close();
                await fileStream.DisposeAsync();
                File.Delete(fileName);
            }
        }
    }
}
