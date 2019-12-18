using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Materal.NetworkHelper;
using Materal.WindowsHelper;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            //const string path = @"D:\Test\Demo\9c6f1074-fb70-4742-a815-22cd7e934fb7";
            //var cmdManager = new CmdManager();
            //await cmdManager.RunCmdCommandsAsync($"rar a -r -ep1 {path}.rar {path}");
            string url = "http://116.55.251.31/UploadFileAPI/UploadFile/GetThumbnail";
            var data = new
            {
                ID = Guid.Parse("31410d75-1f0e-43a9-9bc3-322fb56a85b1"),
                Width = 500,
                Height = 300,
                Quality = 40
            };
            var headers = new Dictionary<string, string>
            {
                {"Accept","*" },
                {"Accept-Language","zh-CN,zh;q=0.9,ja;q=0.8" },
                {"Authorization","Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOlsiV2ViQVBJIiwiV2ViQVBJIl0sImlzcyI6IkludGVncmF0ZWRQbGF0Zm9ybSIsIlVzZXJJRCI6ImViNzU1M2ZmLWVhNmEtNDRiNC05NjExLTBkYmFmOTM4Y2FmNCIsIm5iZiI6MTU3NjIyNjk1MCwiZXhwIjoxNTc2MjQ0OTUwLCJpYXQiOjE1NzYyMjY5NTB9.s6hgIycN-dGiBabzh-k2NFpbWxMBC8ZVLZPRdP5ZT1U" },
                {"Content-Type","application/json" }
            };
            try
            {
                var stopwatch = new Stopwatch();
                var tasks = new List<Task>();
                stopwatch.Start();
                Parallel.For(0, 50, i =>
                {
                    Task task = Task.Run(async () =>
                    {
                        byte[] result = await HttpManager.SendByteAsync(url, HttpMethodType.Post, data, headers);
                    });
                    tasks.Add(task);
                });
                Task.WaitAll(tasks.ToArray());
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }
            catch (MateralHttpException materalHttpException)
            {
                Console.WriteLine(Convert.ToInt32(materalHttpException.StatusCode));
            }
        }
    }
}
