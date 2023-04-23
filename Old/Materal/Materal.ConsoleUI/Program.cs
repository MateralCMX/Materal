using Materal.Model;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            ClassA classA = new()
            {
                IDs = new()
                {
                    1, 2, 3, 4, 5, 6
                },
                Name2 = "T"
            };
            List<ClassA> classAs = new()
            {
                new ClassA{ ID = 0, Name="Test0" },
                new ClassA{ ID = 1, Name="Test1" },
                new ClassA{ ID = 2, Name="Test2" }
            };
            Func<ClassA, bool> a = classA.GetSearchDelegate<ClassA>();
            List<ClassA> b = classAs.Where(a).ToList();
        }

    }
    public class ClassA : FilterModel
    {
        public int ID { get; set; }
        [Contains("ID")]
        public List<int> IDs { get; set; } = new();
        public string Name { get; set; } = string.Empty;
        [Contains("Name")]
        public string Name2 { get; set; } = string.Empty;
    }
}
