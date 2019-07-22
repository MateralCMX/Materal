using Materal.FileHelper;
using System.Drawing;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            Image result = ImageFileManager.GetThumbnailImage(@"D:\Test\TestImage.jpg", 0.4f);
            for (var i = 0; i <= 100; i++)
            {
                ImageFileManager.Compress(result, $@"D:\Test\TestThumbnail_p_0.4[{i}].jpg", i);
            }
        }
    }
}
