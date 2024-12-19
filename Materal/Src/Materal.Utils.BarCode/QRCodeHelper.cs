using SkiaSharp;
using System.Drawing;
using ZXing;
using ZXing.Common;
using ZXing.SkiaSharp;

namespace Materal.Utils.BarCode
{
    /// <summary>
    /// 二维码帮助类
    /// </summary>
    public static class QRCodeHelper
    {
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="content"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static SKBitmap CreateQRCode(string content, int size = 300) => CreateQRCode(content, size, size);
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="content"></param>
        /// <param name="heigth"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static SKBitmap CreateQRCode(string content, int heigth, int width) => CreateQRCode(content, new EncodingOptions
        {
            Height = heigth,
            Width = width
        });
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="content"></param>
        /// <param name="encodingOptions"></param>
        /// <returns></returns>
        public static SKBitmap CreateQRCode(string content, EncodingOptions encodingOptions)
        {
            BarcodeWriter writer = new()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = encodingOptions
            };
            SKBitmap result = writer.Write(content);
            return result;
        }
        /// <summary>
        /// 读取二维码
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        /// <exception cref="UtilException"></exception>
        public static string ReadQRCode(SKBitmap bitmap)
        {
            string result = BarCodeHelper.ReadBarCode(bitmap, out BarcodeFormat barcodeFormat);
            if (barcodeFormat != BarcodeFormat.QR_CODE) throw new UtilException("图片不是二维码");
            return result;
        }
        /// <summary>
        /// 更改二维码图片
        /// </summary>
        /// <param name="qrCode"></param>
        /// <param name="drawAction"></param>
        /// <param name="setMarkPaint"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static SKBitmap ChangeQRCodeImage(SKBitmap qrCode, Action<SKCanvas, SKPaint, Point, Size> drawAction, Action<SKPaint, Point>? setMarkPaint = null, SKColor? background = null)
        {
            Point leftTopPoint = GetQRCodeLeftTopPoint(qrCode);
            Size pointSize = GetQRCodePointSize(qrCode, leftTopPoint);
            QRCodeMark leftTopMark = GetQRCodeLeftTopMark(qrCode, pointSize);
            QRCodeMark rightTopMark = GetQrCodeRigghtTopMark(qrCode, pointSize);
            QRCodeMark leftBottomMark = GetQRCodeLeftBottomMark(qrCode, pointSize);
            SKBitmap result = new(qrCode.Width, qrCode.Height);
            SKColor oldBackgroundColor = qrCode.GetPixel(0, 0);
            using SKCanvas canvas = new(result);
            canvas.Clear(background ?? oldBackgroundColor);
            using SKPaint paint = new();
            for (int x = leftTopPoint.X; x < qrCode.Width - leftTopPoint.X; x += pointSize.Width)
            {
                for (int y = leftTopPoint.Y; y < qrCode.Height - leftTopPoint.Y; y += pointSize.Height)
                {
                    SKColor nowColor = qrCode.GetPixel(x, y);
                    if (nowColor == oldBackgroundColor) continue;
                    paint.Color = nowColor;
                    Point point = new(x, y);
                    Point tempLeftTopPoint = new(x, y);
                    Point tempRightBottomPoint = new(x + pointSize.Width, y + pointSize.Height);
                    float centerX = (tempLeftTopPoint.X + tempRightBottomPoint.X) / 2;
                    float centerY = (tempLeftTopPoint.Y + tempRightBottomPoint.Y) / 2;
                    if (!PointInMarker(point, leftTopMark, rightTopMark, leftBottomMark))
                    {
                        drawAction(canvas, paint, new Point((int)centerX, (int)centerY), new Size(pointSize.Width, pointSize.Height));
                    }
                    else
                    {
                        setMarkPaint?.Invoke(paint, new Point((int)centerX, (int)centerY));
                        canvas.DrawRect(tempLeftTopPoint.X, tempLeftTopPoint.Y, pointSize.Width, pointSize.Height, paint);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 添加Logo
        /// </summary>
        /// <param name="qrCode"></param>
        /// <param name="logo"></param>
        /// <param name="logoSize"></param>
        /// <returns></returns>
        public static SKBitmap AddLogo(SKBitmap qrCode, SKBitmap logo, float logoSize)
        {
            SKBitmap result = new(qrCode.Width, qrCode.Height);
            using SKCanvas canvas = new(result);
            canvas.Clear(SKColors.White);
            using SKPaint paint = new();
            paint.IsAntialias = true;
            canvas.DrawBitmap(qrCode, 0, 0, paint);
            float centerX = (qrCode.Width - logoSize) / 2;
            float centerY = (qrCode.Height - logoSize) / 2;
            SKRect rect = new(centerX, centerY, centerX + logoSize, centerY + logoSize);
            canvas.DrawBitmap(logo, rect, paint);
            return result;
        }
        /// <summary>
        /// 获得二维码左上角点
        /// </summary>
        /// <param name="qrCode"></param>
        /// <returns></returns>
        /// <exception cref="UtilException"></exception>
        private static Point GetQRCodeLeftTopPoint(SKBitmap qrCode)
        {
            for (int x = 0; x < qrCode.Width; x++)
            {
                for (int y = 0; y < qrCode.Height; y++)
                {
                    if (qrCode.GetPixel(x, y) == SKColors.White) continue;
                    return new Point(x, y);
                }
            }
            throw new UtilException("未找到左上角点");
        }
        /// <summary>
        /// 获得二维码左下角点
        /// </summary>
        /// <param name="qrCode"></param>
        /// <returns></returns>
        /// <exception cref="UtilException"></exception>
        private static Point GetQRCodeLeftBottomPoint(SKBitmap qrCode)
        {
            for (int x = 0; x < qrCode.Width; x++)
            {
                for (int y = qrCode.Height - 1; y >= 0; y--)
                {
                    if (qrCode.GetPixel(x, y) == SKColors.White) continue;
                    return new Point(x, y);
                }
            }
            throw new UtilException("未找到左下角点");
        }
        /// <summary>
        /// 获得二维码右上角点
        /// </summary>
        /// <param name="qrCode"></param>
        /// <returns></returns>
        /// <exception cref="UtilException"></exception>
        private static Point GetQRCodeRightTopPoint(SKBitmap qrCode)
        {
            for (int x = qrCode.Width - 1; x >= 0; x--)
            {
                for (int y = 0; y < qrCode.Height; y++)
                {
                    if (qrCode.GetPixel(x, y) == SKColors.White) continue;
                    return new Point(x, y);
                }
            }
            throw new UtilException("未找到右上角点");
        }
        /// <summary>
        /// 获得二维码左上标记
        /// </summary>
        /// <param name="qrCode"></param>
        /// <param name="pointSize"></param>
        /// <returns></returns>
        private static QRCodeMark GetQRCodeLeftTopMark(SKBitmap qrCode, Size pointSize)
        {
            Point leftTopPoint = GetQRCodeLeftTopPoint(qrCode);
            return new()
            {
                LeftTopPoint = leftTopPoint,
                RightBottomPoint = new(leftTopPoint.X + pointSize.Width * 7, leftTopPoint.Y + pointSize.Height * 7)
            };
        }
        /// <summary>
        /// 获得二维码右上标记
        /// </summary>
        /// <param name="qrCode"></param>
        /// <param name="pointSize"></param>
        /// <returns></returns>
        private static QRCodeMark GetQrCodeRigghtTopMark(SKBitmap qrCode, Size pointSize)
        {
            Point rightTopPoint = GetQRCodeRightTopPoint(qrCode);
            return new()
            {
                LeftTopPoint = new(rightTopPoint.X - pointSize.Width * 7, rightTopPoint.Y),
                RightBottomPoint = new(rightTopPoint.X, rightTopPoint.Y + pointSize.Height * 7)
            };
        }
        /// <summary>
        /// 获得二维码左下标记
        /// </summary>
        /// <param name="qrCode"></param>
        /// <param name="pointSize"></param>
        /// <returns></returns>
        private static QRCodeMark GetQRCodeLeftBottomMark(SKBitmap qrCode, Size pointSize)
        {
            Point leftBottomPoint = GetQRCodeLeftBottomPoint(qrCode);
            return new()
            {
                LeftTopPoint = new(leftBottomPoint.X, leftBottomPoint.Y - pointSize.Height * 7),
                RightBottomPoint = new(leftBottomPoint.X + pointSize.Width * 7, leftBottomPoint.Y)
            };
        }
        /// <summary>
        /// 获取二维码点大小
        /// </summary>
        /// <param name="qrCode"></param>
        /// <param name="leftTopPoint"></param>
        /// <returns></returns>
        private static Size GetQRCodePointSize(SKBitmap qrCode, Point leftTopPoint)
        {
            SKColor foregroundColor = qrCode.GetPixel(leftTopPoint.X, leftTopPoint.Y);
            int x = leftTopPoint.X;
            int y = leftTopPoint.Y;
            for (int i = 1; i < qrCode.Width; i++)
            {
                if (qrCode.GetPixel(x + i, y + i) == foregroundColor) continue;
                return new Size(i, i);
            }
            throw new UtilException("未找到二维码点大小");
        }
        /// <summary>
        /// 点是否在标记内
        /// </summary>
        /// <param name="point"></param>
        /// <param name="marks"></param>
        /// <returns></returns>
        private static bool PointInMarker(Point point, params QRCodeMark[] marks)
        {
            foreach (QRCodeMark item in marks)
            {
                if (PointInSquare(point, item.LeftTopPoint, item.RightBottomPoint)) return true;
            }
            return false;
        }
        /// <summary>
        /// 点是否在正方形内
        /// </summary>
        /// <param name="point"></param>
        /// <param name="leftTopPoint"></param>
        /// <param name="rightBottomPoint"></param>
        /// <returns></returns>
        private static bool PointInSquare(Point point, Point leftTopPoint, Point rightBottomPoint)
        {
            if (point.X < leftTopPoint.X || point.X > rightBottomPoint.X) return false;
            if (point.Y < leftTopPoint.Y || point.Y > rightBottomPoint.Y) return false;
            return true;
        }
        private class QRCodeMark
        {
            public Point LeftTopPoint { get; set; }
            public Point RightBottomPoint { get; set; }
        }
    }
}
