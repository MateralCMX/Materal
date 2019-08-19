using System.Collections.Generic;
using System.Linq;
using Materal.StringHelper;
using Materal.StringHelper.Models;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            List<string> result = "陈明旭".GetChinesePinYin(PinYinMode.Abbreviation).ToList();
        }
    }
}
