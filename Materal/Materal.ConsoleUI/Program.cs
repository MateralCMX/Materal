using Materal.NetworkHelper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static async Task Main()
        {
            Encoding encoding = Encoding.UTF8;
            var httpHeaders = new Dictionary<string, string>
            {
                ["Accept"] = "*",
                ["Content-Type"] = "application/json",
                ["Authorization"] = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOlsiV2ViQVBJIiwiV2ViQVBJIl0sImlzcyI6Ik1hdGVyYWwuQVBQIiwiVXNlcklEIjoiMGM1YWMyYmEtZDQ3ZC00ZDc4LTg2Y2QtMTVkNDIzMTM2ZGY1IiwibmJmIjoxNTk5NTQ3ODk4LCJleHAiOjE1OTk1NjU4OTgsImlhdCI6MTU5OTU0Nzg5OH0.T3jCW_Bvx3NYisJEEWqZK5nc1gvec5IFA9C5C_-Hvsg"
            };
            //const string url = "http://192.168.0.101:8702/api/User/Login";
            //var data = new
            //{
            //    Account = "Admin",
            //    Password = "123456"
            //};
            //string jsonResult = await HttpManager.SendPatchAsync(url, data, null, httpHeaders, encoding);
            //const string url = "http://192.168.0.101:8702/api/User/GetMyUserInfo";
            //string jsonResult = await HttpManager.SendGetAsync(url, null, httpHeaders, encoding);
            //const string url = "http://192.168.0.101:8702/api/User/GetUserInfo";
            //var data = new Dictionary<string, string>
            //{
            //    ["id"] = "0c5ac2ba-d47d-4d78-86cd-15d423136df5"
            //};
            //string jsonResult = await HttpManager.SendGetAsync(url, data, httpHeaders, encoding);
            //const string url = "http://192.168.0.101:8702/api/User/AddUser";
            //var data = new
            //{
            //    Account = "TestUser",
            //    Name = "测试用户"
            //};
            //string jsonResult = await HttpManager.SendPostAsync(url, data, null, httpHeaders, encoding);
            //const string url = "http://192.168.0.101:8702/api/User/EditUser";
            //var data = new
            //{
            //    ID = "3911b1a8-ac69-4e82-9ecd-7776e3f72bca",
            //    Account = "TestUser",
            //    Name = "测试用户"
            //};
            //string jsonResult = await HttpManager.SendPutAsync(url, data, null, httpHeaders, encoding);
            const string url = "http://192.168.0.101:8702/api/User/DeleteUser";
            var data = new Dictionary<string, string>
            {
                ["id"] = "3911b1a8-ac69-4e82-9ecd-7776e3f72bca"
            };
            string jsonResult = await HttpManager.SendDeleteAsync(url, null, data, httpHeaders, encoding);
            Console.WriteLine(jsonResult);
        }
    }
}
