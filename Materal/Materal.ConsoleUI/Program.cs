using System;
using Materal.FileHelper;
using System.Drawing;
using Materal.ConvertHelper;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            //Image result = ImageFileManager.GetThumbnailImage(@"D:\Test\TestImage.jpg", 0.4f);
            //for (var i = 0; i <= 100; i++)
            //{
            //    ImageFileManager.Compress(result, $@"D:\Test\TestThumbnail_p_0.4[{i}].jpg", i);
            //}
            string inputString = Console.ReadLine();
            int inputInt = inputString.ConvertTo<int>();
            Console.WriteLine($"输入的值是:{inputInt}");
        }
    }
}
