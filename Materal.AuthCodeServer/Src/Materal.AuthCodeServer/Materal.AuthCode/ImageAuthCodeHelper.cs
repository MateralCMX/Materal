using Materal.Common;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Materal.AuthCode
{
    public class ImageAuthCodeHelper
    {
        public Image GetAuthCodeImage(string value, string baseMapPath, string[] fontFamilyPaths, Color[] colors)
        {
            if (fontFamilyPaths == null || fontFamilyPaths.Length == 0) throw new MateralException("请至少提供一种字体");
            if (colors == null || colors.Length == 0) throw new MateralException("请至少提供一种颜色");
            if (fontFamilyPaths.Any(m => !m.ToUpper().EndsWith(".TTF"))) throw new MateralException("请使用TTF字体");
            if (fontFamilyPaths.Any(m => !File.Exists(m))) throw new MateralException("字体文件不存在");
            if (!File.Exists(baseMapPath)) throw new MateralException("底图文件不存在");
            Image image = Image.Load(baseMapPath);
            image.Mutate(context =>
            {
                for (var i = 0; i < value.Length; i++)
                {
                    int fontSize = GetFontSize(image);
                    PointF point = GetPoint(image, value, i, fontSize);
                    string textValue = value[i].ToString();
                    DrawText(context, textValue, fontSize, fontFamilyPaths, colors, point);
                }
            });
            return image;
        }

        private void DrawDisturb(IImageProcessingContext context)
        {
        }
        /// <summary>
        /// 获得字体大小
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private int GetFontSize(IImageInfo image)
        {
            var random = new Random();
            int fontSize = random.Next(image.Height / 2, image.Height);
            return fontSize;
        }
        /// <summary>
        /// 获得绘制坐标
        /// </summary>
        /// <param name="image"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        private PointF GetPoint(IImageInfo image, string value, int index, int fontSize)
        {
            int defaultX = image.Width / value.Length;
            int margin = defaultX / value.Length;
            var random = new Random();
            int temp = random.Next(0, 2);
            int x = defaultX * index + margin;
            switch (temp)
            {
                case 0:
                    x += random.Next(0, fontSize / 2);
                    break;
                default:
                    x -= random.Next(0, fontSize / 2);
                    break;
            }
            if (x <= 0) x = 0;
            if (x + fontSize >= image.Width) x = image.Width - fontSize;
            int y = (image.Height - fontSize) / 2;
            temp = random.Next(0, 2);
            switch (temp)
            {
                case 0:
                    y += random.Next(0, fontSize / 2);
                    break;
                default:
                    y -= random.Next(0, fontSize / 2);
                    break;
            }
            if (y <= 0) y = 0;
            if (y + fontSize >= image.Height) y = image.Height - fontSize;
            var point = new PointF(x, y);
            return point;
        }
        /// <summary>
        /// 绘制文字
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontFamilyPaths"></param>
        /// <param name="colors"></param>
        /// <param name="point"></param>
        private void DrawText(IImageProcessingContext context, string value, int fontSize, IReadOnlyList<string> fontFamilyPaths, IReadOnlyList<Color> colors, PointF point)
        {
            var random = new Random();
            var fonts = new FontCollection();
            string fontFamilyPath = fontFamilyPaths[random.Next(0, fontFamilyPaths.Count)];
            FontFamily fontFamily = fonts.Install(fontFamilyPath); //字体的路径
            var font = new Font(fontFamily, fontSize);
            Color color = colors[random.Next(0, colors.Count)];
            context.DrawText(value, font, color, point);
        }
    }
}
