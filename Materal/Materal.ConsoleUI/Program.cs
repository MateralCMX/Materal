using System;
using System.Drawing;
using System.Drawing.Imaging;
using Materal.ConvertHelper;
using Materal.FileHelper;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            const string url = "http://www.baidu.com";
            var image = new Bitmap(@"D:\Temp\1.jpg");
            using Bitmap qrCode = url.ToQRCode(10, Color.Red, Color.Blue, image);
            qrCode.Save(@"D:\Temp\TTT.png", ImageFormat.Png);
            string base64 = ImageFileManager.GetBase64Image(@"D:\Temp\TTT.png");
            Console.WriteLine(base64);
        }
    }
}
