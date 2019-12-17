using System;
using Materal.ConvertHelper;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            var user = new User
            {
                Name = "Materal",
                Remark = new Remark
                {
                    Content = "Remark"
                }
            };
            var user3 = new User1
            {
                Name = "LJB"
            };
            user.CopyProperties(user3, nameof(User.Name));
            var user1 = user.CopyProperties<User1>(nameof(User.Name), nameof(User.Age));//浅拷贝
            var user2 = user.ToBytes().ToObject<User>();//深拷贝
            var user4 = user.Clone();//深拷贝
            var user5 = user.CloneByJson();//深拷贝
            var user6 = user.CloneByReflex();//深拷贝
            var user7 = user.CloneBySerializable();//深拷贝
            var user8 = user.CloneByXml();//深拷贝
            user.Remark.Content = "1";
            Console.WriteLine(user1.Remark.Content);//输出的是1
            Console.WriteLine(user2.Remark.Content);//输出的是Remark
            Console.WriteLine(user3.Remark.Content);//输出的是1
            Console.WriteLine(user4.Remark.Content);//输出的是Remark
            Console.WriteLine(user5.Remark.Content);//输出的是Remark
            Console.WriteLine(user6.Remark.Content);//输出的是Remark
            Console.WriteLine(user7.Remark.Content);//输出的是Remark
            Console.WriteLine(user8.Remark.Content);//输出的是Remark

        }
    }
    [Serializable]
    public class User
    {
        public int? Age { get; set; }

        public string Name { get; set; }

        public Remark Remark { get; set; }
    }

    [Serializable]
    public class User1
    {
        public int? Age { get; set; }

        public string Name { get; set; }

        public Remark Remark { get; set; }
    }

    [Serializable]
    public class Remark
    {
        public string Content { get; set; }
    }
}
