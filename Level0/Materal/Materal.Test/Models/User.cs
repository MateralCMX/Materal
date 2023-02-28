namespace Materal.Test.Models
{
    public class User
    {
        public string Name { get; set; } = string.Empty;

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
}
