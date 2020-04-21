using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Materal.ConvertHelper;
using Materal.NetworkHelper;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static async Task Main()
        {

            var heads = new Dictionary<string, string>
            {
                ["Content-Type"] = "application/json",
                ["Authorization"] =
                    "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOlsiV2ViQVBJIiwiV2ViQVBJIl0sImlzcyI6IkludGVncmF0ZWRQbGF0Zm9ybSIsIlVzZXJJRCI6IjU2MzI0MDlmLTc3ODEtNDNlZS1hMGI5LWU1YzRlMDJiOTdjMyIsIm5iZiI6MTU4NzQzMTIyMywiZXhwIjoxNTg3NDQ5MjIzLCJpYXQiOjE1ODc0MzEyMjN9.Ku-mwZVmatS0Gc9hNy-c4k9dm8xNkMJfzmCVQD8iahY"
            };
            const string url1 =
                "http://116.55.251.31:8921/api/ListingCommodity/CheckPortfolioIsPutaway";
            const string url2 =
                "http://116.55.251.31:8900/CommerceBourseAPI/ListingCommodity/CheckPortfolioIsPutaway";
            var data = new Dictionary<string, string>
            {
                ["merchandiseID"] = "12569a10-c4a5-468d-81d2-33212692d6db"
            };
            try
            {
                var resutl = await HttpManager.SendGetAsync(url1, data, heads, Encoding.UTF8);
                var resutl2 = await HttpManager.SendGetAsync(url2, data, heads, Encoding.UTF8);
            }
            catch (MateralHttpException ex)
            {
                Console.WriteLine(ex);
            }
            catch (MateralNetworkException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
