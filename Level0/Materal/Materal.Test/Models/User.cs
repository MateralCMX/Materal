namespace Materal.Test.Models
{
    public class User
    {
        public string Name { get; set; }

        public User(string name)
        {
            Name = name;
        }
    }
    public class Student : User
    {
        public int Age { get; set; }
        public Student(string name, int age) : base(name)
        {
            Age = age;
        }
    }
}
