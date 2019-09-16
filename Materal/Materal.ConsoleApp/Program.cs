using Materal.StringHelper;
using System.Collections.Generic;

namespace Materal.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IEnumerable<string> path = "党员账号".GetChinesePinYin();
        }
    }
}
