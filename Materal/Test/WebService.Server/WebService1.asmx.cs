﻿#nullable enable
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Services;
using System.Web.Services;

namespace WebService.Server
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://WebService.Server/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld() => "Hello World";
        [WebMethod]
        public UserInfo GetUser() => new() { Name = "Materal", Age = 18 };
        [WebMethod]
        public List<UserInfo> GetUsers() => [new UserInfo { Name = "Materal", Age = 18 }, new UserInfo { Name = "Materal2", Age = 28 }];
        [WebMethod]
        public UserInfo GetUserByNameAndAge(string name, int age) => new() { Name = name, Age = age };
        [WebMethod]
        public UserInfo GetUserByUserInfo(UserInfo user) => user;
    }
    public class UserInfo
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int? TestNull { get; set; }
        public ScoreInfo? ScoreValue { get; set; }
        public List<int> ArrayValues { get; set; } = [];
        public List<ScoreInfo> Scores { get; set; } = [];
    }
    public class ScoreInfo
    {
        public string Name { get; set; } = string.Empty;
        public int Score { get; set; }
        public int? TestNull { get; set; }
    }
}
