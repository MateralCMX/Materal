using Materal.AuthCode;
using SixLabors.ImageSharp;

namespace Materal.AuthCodeServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string value = "AB1K";
            string[] fontFamilyPaths =
            {
                @"D:\Temp\STSONG.TTF"
            };
            const string baseMapPath = @"D:\Temp\BaseMap.png";
            Color[] colors =
            {
                Color.White,
                Color.Red,
                Color.Green
            };
            var imageAuthCodeHelper = new ImageAuthCodeHelper();
            for (var i = 0; i < 100; i++)
            {
                using (Image image = imageAuthCodeHelper.GetAuthCodeImage(value, baseMapPath, fontFamilyPaths, colors))
                {
                    image.Save($@"D:\Temp\Result\Test{i}.png");
                }
            }
        }
    }
}
