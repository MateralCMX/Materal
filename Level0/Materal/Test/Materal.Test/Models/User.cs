using Materal.Utils.Model;

namespace Materal.Test.Models
{
    public class User : FilterModel
    {
        public string Name { get; set; } = string.Empty;
        public DateTime CreateTime { get; set;} = DateTime.Now;

        public User()
        {
        }

        public User(string name)
        {
            Name = name;
        }
    }
    public class Student : User
    {
        public int Age { get; set; }
        public Student() : base()
        {
        }
        public Student(string name, int age) : base(name)
        {
            Age = age;
        }
    }
    public class QueryUserModel : PageRequestModel
    {
        [Contains]
        public string? Name { get; set; }
        [GreaterThanOrEqual("CreateTime")]
        public DateTime? MinCreateTime { get; set; }
        [LessThanOrEqual("CreateTime")]
        public DateTime? MaxCreateTime { get; set; }
    }
}
