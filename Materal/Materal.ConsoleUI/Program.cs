using System;
using System.Globalization;
using Materal.StringHelper;

namespace Materal.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an array of all supported standard date and time format specifiers.
            string[] formats = {"d", "D", "f", "F", "g", "G", "m", "o", "r",
                "s", "t", "T", "u", "U", "Y","yyyy/MM/dd HH:mm:ss"};
            // Create an array of four cultures.                                 
            CultureInfo[] cultures = {CultureInfo.CreateSpecificCulture("de-DE"),
                CultureInfo.CreateSpecificCulture("en-US"),
                CultureInfo.CreateSpecificCulture("es-ES"),
                CultureInfo.CreateSpecificCulture("fr-FR"),
                CultureInfo.CreateSpecificCulture("zh-CN")};
            // Define date to be displayed.
            DateTime dateToDisplay = new DateTime(2008, 10, 1, 17, 4, 32);

            // Iterate each standard format specifier.
            foreach (string formatSpecifier in formats)
            {
                foreach (CultureInfo culture in cultures)
                    Console.WriteLine("{0} Format Specifier {1, 10} Culture {2, 40}",
                        formatSpecifier, culture.Name,
                        dateToDisplay.ToString(formatSpecifier, culture));
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
