// -----------------------------------------------------------------------
//  <copyright file="BitmapExtensions.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2017 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2017-07-18 10:38</last-date>
// -----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;


namespace OSharp.Utility.Extensions
{
    /// <summary>
    /// 图像扩展辅助操作
    /// </summary>
    public static class BitmapExtensions
    {
        /// <summary>
        /// 使图像绕中心点旋转一定角度
        /// </summary>
        /// <param name="bmp">待处理的图像</param>
        /// <param name="angle">旋转的角度，正值为逆时针方向</param>
        /// <returns> 旋转后的图像 </returns>
        public static Bitmap Rotate(this Bitmap bmp, int angle)
        {
            angle = angle % 360;

            //弧度转换
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);

            //原图的宽和高
            int w1 = bmp.Width;
            int h1 = bmp.Height;
            //旋转后的宽和高
            int w2 = (int)(Math.Max(Math.Abs(w1 * cos - h1 * sin), Math.Abs(w1 * cos + h1 * sin)));
            int h2 = (int)(Math.Max(Math.Abs(w1 * sin - h1 * cos), Math.Abs(w1 * sin + h1 * cos)));

            Bitmap newBmp = new Bitmap(w2, h2);
            using (Graphics graphics = Graphics.FromImage(newBmp))
            {
                //目标位图
                graphics.InterpolationMode = InterpolationMode.Bilinear;
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                //计算偏移量
                Point offset = new Point((w2 - w1) / 2, (h2 - h1) / 2);

                //构造图像显示区域：使原始图像与目标图像中心点一致
                Rectangle rect = new Rectangle(offset.X, offset.Y, w1, h1);
                Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

                graphics.TranslateTransform(center.X, center.Y);
                graphics.RotateTransform(360 - angle);

                //恢复图像在水平和垂直方向的平移
                graphics.TranslateTransform(-center.X, -center.Y);
                graphics.DrawImage(bmp, rect);

                //重置绘图的所有变换
                graphics.ResetTransform();
                graphics.Save();
                graphics.Dispose();
                return newBmp;
            }
        }

        /// <summary>
        /// 按指定宽度与高度缩放图像
        /// </summary>
        /// <param name="bmp">待处理的图像</param>
        /// <param name="width">缩放后的宽度</param>
        /// <param name="height">缩放后的高度</param>
        /// <param name="model">图像质量模式</param>
        /// <returns> 缩放后的图像 </returns>
        public static Bitmap Zoom(this Bitmap bmp, int width, int height, InterpolationMode model = InterpolationMode.Default)
        {
            Bitmap newBmp = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(newBmp))
            {
                graphics.InterpolationMode = model;
                graphics.DrawImage(bmp, new Rectangle(0, 0, width, height), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                return newBmp;
            }
        }

        /// <summary>
        /// 按指定百分比缩放图像
        /// </summary>
        /// <param name="bmp">待处理的图像</param>
        /// <param name="percent">缩放百分比（小数）</param>
        /// <param name="model">图像质量模式</param>
        /// <returns> 缩放后的图像 </returns>
        public static Bitmap Zoom(this Bitmap bmp, double percent, InterpolationMode model = InterpolationMode.Default)
        {
            int width = (int)(bmp.Width * percent);
            int height = (int)(bmp.Height * percent);
            return Zoom(bmp, width, height, model);
        }

        /// <summary>
        /// 图像灰度化，逐点方式
        /// </summary>
        /// <param name="bmp">待处理的图像</param>
        /// <returns> 灰度化后的图像 </returns>
        public static Bitmap GrayByPixels(this Bitmap bmp)
        {
            Bitmap newBmp = new Bitmap(bmp.Width, bmp.Height);
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color pixel = bmp.GetPixel(i, j);
                    byte gray = GetGrayColorValue(pixel);
                    newBmp.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            return newBmp;
        }

        ///// <summary>
        ///// 图像灰度化，逐行方式
        ///// </summary>
        ///// <param name="bmp">待处理的图像</param>
        ///// <returns></returns>
        //public static Bitmap GrayByLine(this Bitmap bmp)
        //{
        //    Rectangle rectangle = new Rectangle(0, 0, bmp.Width, bmp.Height);
        //    BitmapData data = bmp.LockBits(rectangle, ImageLockMode.ReadWrite, bmp.PixelFormat);

        //    IntPtr scan0 = data.Scan0;
        //    int length = bmp.Width * bmp.Height;
        //    int[] pixels = new int[length];
        //    Marshal.Copy(scan0, pixels, 0, length);

        //    //对图片进行处理
        //    for (int i = 0; i < length; i++)
        //    {
        //        int gray = GetGrayColorValue(Color.FromArgb(pixels[i]));
        //        pixels[i] = Color.FromArgb(gray, gray, gray).ToArgb();
        //    }
        //    bmp.UnlockBits(data);
        //    return bmp;
        //}

        /// <summary>
        /// 去掉杂点，周边有效点数的方式(适合杂点/杂线粗为1)
        /// </summary>
        /// <param name="bmp">等处理图像</param>
        /// <param name="gray">临界灰度值，大于此值的点将视为无效点</param>
        /// <param name="maxNearPoints">周边最大有效点数，有效点数小于此值，当前点视为杂点</param>
        /// <returns></returns>
        public static Bitmap ClearNoise(this Bitmap bmp, int gray, int maxNearPoints)
        {
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color piexl = bmp.GetPixel(x, y);
                    if (piexl.R >= gray)
                    {
                        bmp.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        int nearDots = 0;
                        //判断周围8个点是否全为空
                        if (x == 0 || x == bmp.Width - 1 || y == 0 || y == bmp.Height - 1)
                        {
                            //边框全去掉
                            bmp.SetPixel(x, y, Color.White);
                            continue;
                        }
                        if (bmp.GetPixel(x - 1, y - 1).R < gray) nearDots++;
                        if (bmp.GetPixel(x, y - 1).R < gray) nearDots++;
                        if (bmp.GetPixel(x + 1, y - 1).R < gray) nearDots++;
                        if (bmp.GetPixel(x - 1, y).R < gray) nearDots++;
                        if (bmp.GetPixel(x + 1, y).R < gray) nearDots++;
                        if (bmp.GetPixel(x - 1, y + 1).R < gray) nearDots++;
                        if (bmp.GetPixel(x, y + 1).R < gray) nearDots++;
                        if (bmp.GetPixel(x + 1, y + 1).R < gray) nearDots++;
                        if (nearDots < maxNearPoints)
                        {
                            bmp.SetPixel(x, y, Color.White);
                        }
                    }
                }
            }
            return bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), bmp.PixelFormat);
        }

        /// <summary>
        /// 调整图像亮度
        /// </summary>
        /// <param name="bmp">待处理的图像</param>
        /// <param name="value">调整的亮度值，取值为[-255, 255]</param>
        /// <returns> 调整亮度后的图像 </returns>
        public static Bitmap Brightness(this Bitmap bmp, int value)
        {
            value = value < -255 ? -255 : value;
            value = value > 255 ? 255 : value;
            int width = bmp.Width, height = bmp.Height;
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* p = (byte*)bmpData.Scan0;
                int offset = bmpData.Stride - width * 3;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // 处理指定位置像素的亮度
                        for (int i = 0; i < 3; i++)
                        {
                            int pix = p[i] + value;
                            if (value < 0)
                            {
                                p[i] = (byte)Math.Max(0, pix);
                            }
                            if (value > 0)
                            {
                                p[i] = (byte)Math.Min(255, pix);
                            }
                        } // i
                        p += 3;
                    } // x
                    p += offset;
                } // y
            }

            bmp.UnlockBits(bmpData);
            return bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), bmp.PixelFormat);
        }

        /// <summary>
        /// 调整图像对比度
        /// </summary>
        /// <param name="bmp">待处理的图像</param>
        /// <param name="value">调整的对比度，取值为[-100, 100]</param>
        /// <returns> 调整对比度后的图像 </returns>
        public static Bitmap Contrast(this Bitmap bmp, int value)
        {
            value = value < -100 ? -100 : value;
            value = value > 100 ? 100 : value;
            double contrast = (100.0 + value) / 100.0;
            contrast *= contrast;
            int width = bmp.Width;
            int height = bmp.Height;
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* p = (byte*)bmpData.Scan0;
                int offset = bmpData.Stride - width * 3;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // 处理指定位置像素的对比度
                        for (int i = 0; i < 3; i++)
                        {
                            double pixel = ((p[i] / 255.0 - 0.5) * contrast + 0.5) * 255;
                            pixel = pixel < 0 ? 0 : pixel;
                            pixel = pixel > 255 ? 255 : pixel;
                            p[i] = (byte)pixel;
                        } // i
                        p += 3;
                    } // x
                    p += offset;
                } // y
            }
            bmp.UnlockBits(bmpData);
            return bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), bmp.PixelFormat);
        }

        /// <summary>
        /// Gamma校正
        /// </summary>
        /// <param name="bmp">待处理的图像</param>
        /// <param name="value">Gamma值</param>
        /// <returns> Gamma校正后的图像 </returns>
        public static Bitmap Gamma(this Bitmap bmp, float value)
        {
            if (Equals(value, 1.0000f))
            {
                return bmp;
            }
            Bitmap newBmp = new Bitmap(bmp.Width, bmp.Height);
            using (Graphics graphics = Graphics.FromImage(newBmp))
            {
                ImageAttributes attribtues = new ImageAttributes();
                attribtues.SetGamma(value, ColorAdjustType.Bitmap);
                graphics.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attribtues);
                return newBmp;
            }
        }

        /// <summary>
        /// 在图片上打印文字
        /// </summary>
        /// <param name="bmp">待处理的图像</param>
        /// <param name="text">要打印的文字</param>
        /// <param name="font">字体信息</param>
        /// <param name="color">文字颜色</param>
        /// <param name="x">文字位置横坐标</param>
        /// <param name="y">文字位置纵坐标</param>
        /// <returns> 打印文字后的图像 </returns>
        public static Bitmap SetText(this Bitmap bmp, string text, Font font, Color color, int x, int y)
        {
            Bitmap newBmp = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), bmp.PixelFormat);
            using (Graphics graphics = Graphics.FromImage(newBmp))
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                SolidBrush brush = new SolidBrush(color);
                graphics.DrawString(text, font, brush, new PointF(x, y));
                return newBmp;
            }
        }

        /// <summary>
        /// 去除图片边框
        /// </summary>
        /// <param name="bmp">待处理的图像</param>
        /// <param name="borderWidth">边框宽度</param>
        /// <returns></returns>
        public static Bitmap ClearBorder(this Bitmap bmp, int borderWidth)
        {
            int width = bmp.Width, height = bmp.Height;
            Bitmap newBmp = bmp.Clone(new Rectangle(0, 0, width, height), bmp.PixelFormat);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i < borderWidth || j < borderWidth || j > width - 1 - borderWidth || i > height - 1 - borderWidth)
                    {
                        newBmp.SetPixel(j, i, Color.White);
                    }
                }
            }
            return newBmp;
        }

        /// <summary>
        /// 底片效果
        /// </summary>
        /// <param name="bmp">待处理的图像</param>
        /// <returns> 底片效果的图像 </returns>
        public static Bitmap Plate(this Bitmap bmp)
        {
            int width = bmp.Width, height = bmp.Height;
            Bitmap newBmp = new Bitmap(width, height);
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Color pixel = bmp.GetPixel(i, j);
                    int r = 255 - pixel.R;
                    int g = 255 - pixel.G;
                    int b = 255 - pixel.B;
                    newBmp.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }
            return newBmp;
        }

        /// <summary>
        /// 浮雕效果
        /// </summary>
        /// <param name="bmp">待处理的图像</param>
        /// <returns> 浮雕效果的图像 </returns>
        public static Bitmap Emboss(this Bitmap bmp)
        {
            int width = bmp.Width, height = bmp.Height;
            Bitmap newBmp = new Bitmap(width, height);
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Color pixel1 = bmp.GetPixel(i, j);
                    Color pixel2 = bmp.GetPixel(i + 1, j + 1);
                    int r = Math.Abs(pixel1.R - pixel2.R + 128);
                    int g = Math.Abs(pixel1.G - pixel2.G + 128);
                    int b = Math.Abs(pixel1.B - pixel2.B + 128);
                    r = r > 255 ? 255 : r;
                    r = r < 0 ? 0 : r;
                    g = g > 255 ? 255 : g;
                    g = g < 0 ? 0 : g;
                    b = b > 255 ? 255 : b;
                    b = b < 0 ? 0 : b;
                    newBmp.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }
            return newBmp;
        }

        /// <summary>
        /// 柔化效果
        /// </summary>
        /// <param name="bmp">待处理的图像</param>
        /// <returns> 柔化效果的图像 </returns>
        public static Bitmap Soften(this Bitmap bmp)
        {
            int width = bmp.Width, height = bmp.Height;
            Bitmap newBmp = new Bitmap(width, height);
            //高斯模板
            int[] gauss = { 1, 2, 1, 2, 4, 2, 1, 2, 1 };
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    int index = 0;
                    int r = 0, g = 0, b = 0;
                    for (int col = -1; col <= 1; col++)
                    {
                        for (int row = -1; row <= 1; row++)
                        {
                            Color pixel = bmp.GetPixel(i + row, j + col);
                            r += pixel.R * gauss[index];
                            g += pixel.G * gauss[index];
                            b += pixel.B * gauss[index];
                            index++;
                        }
                    }
                    r /= 16;
                    g /= 16;
                    b /= 16;
                    //处理颜色值溢出
                    r = r > 255 ? 255 : r;
                    r = r < 0 ? 0 : r;
                    g = g > 255 ? 255 : g;
                    g = g < 0 ? 0 : g;
                    b = b > 255 ? 255 : b;
                    b = b < 0 ? 0 : b;
                    newBmp.SetPixel(i - 1, j - 1, Color.FromArgb(r, g, b));
                }
            }
            return newBmp;
        }

        /// <summary>
        /// 锐化效果
        /// </summary>
        /// <param name="bmp">待处理的图像</param>
        /// <returns> 锐化效果的图像 </returns>
        public static Bitmap Sharpen(this Bitmap bmp)
        {
            int width = bmp.Width, height = bmp.Height;
            Bitmap newBmp = new Bitmap(width, height);
            //拉普拉斯模板
            int[] laplacian = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    int index = 0;
                    int r = 0, g = 0, b = 0;
                    for (int col = -1; col <= 1; col++)
                    {
                        for (int row = -1; row <= 1; row++)
                        {
                            Color pixel = bmp.GetPixel(i + row, j + col);
                            r += pixel.R * laplacian[index];
                            g += pixel.G * laplacian[index];
                            b += pixel.B * laplacian[index];
                            index++;
                        }
                    }
                    r /= 16;
                    g /= 16;
                    b /= 16;
                    //处理颜色值溢出
                    r = r > 255 ? 255 : r;
                    r = r < 0 ? 0 : r;
                    g = g > 255 ? 255 : g;
                    g = g < 0 ? 0 : g;
                    b = b > 255 ? 255 : b;
                    b = b < 0 ? 0 : b;
                    newBmp.SetPixel(i - 1, j - 1, Color.FromArgb(r, g, b));
                }
            }
            return newBmp;
        }

        /// <summary>
        /// 雾化效果
        /// </summary>
        /// <param name="bmp">待处理的图像</param>
        /// <returns> 雾化效果的图像 </returns>
        public static Bitmap Atomizing(this Bitmap bmp)
        {
            int width = bmp.Width, height = bmp.Height;
            Bitmap newBmp = new Bitmap(width, height);
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Random rnd = new Random();
                    int k = rnd.Next(123456);
                    //像素块大小
                    int dx = i + k % 19;
                    int dy = j + k % 19;
                    if (dx >= width)
                    {
                        dx = width - 1;
                    }
                    if (dy >= height)
                    {
                        dy = height - 1;
                    }
                    Color pixel = bmp.GetPixel(dx, dy);
                    newBmp.SetPixel(i, j, pixel);
                }
            }
            return newBmp;
        }

        /// <summary>
        /// 二值化效果
        /// </summary>
        public static Bitmap Binaryzation(this Bitmap bmp)
        {
            int width = bmp.Width, height = bmp.Height;
            Bitmap newBmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData data = newBmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed);
            for (int j = 0; j < height; j++)
            {
                byte[] scan = new byte[(width + 7) / 8];
                for (int i = 0; i < width; i++)
                {
                    Color pixel = bmp.GetPixel(i, j);
                    if (pixel.GetBrightness() >= 0.5)
                    {
                        scan[i / 8] |= (byte)(0x80 >> (i % 8));
                    }
                }
                Marshal.Copy(scan, 0, (IntPtr)((int)data.Scan0 + data.Stride * j), scan.Length);
            }
            newBmp.UnlockBits(data);
            return newBmp;
        }

        /// <summary>
        /// 固定阈值的二值化
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="threshold"></param>
        /// <returns> </returns>
        public static Bitmap Binaryzation(this Bitmap bmp, byte threshold)
        {
            int widht = bmp.Width, height = bmp.Height;
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, widht, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            unsafe
            {
                //将原始图片变成灰度二位数组
                byte* p = (byte*)data.Scan0;
                byte[,] source = new byte[widht, height];
                int offset = data.Stride - widht * 3;
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < widht; i++)
                    {
                        source[i, j] = (byte)((p[0] + p[1] + p[2]) / 3);
                        p += 3;
                    }
                    p += offset;
                }
                bmp.UnlockBits(data);
                //将灰度二位数组转换为二值图像
                Bitmap newBmp = new Bitmap(widht, height, PixelFormat.Format24bppRgb);
                BitmapData newData = newBmp.LockBits(new Rectangle(0, 0, widht, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                p = (byte*)newData.Scan0;
                offset = newData.Stride - widht * 3;
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < widht; i++)
                    {
                        p[0] = p[1] = p[2] = GetAverageColor(source, i, j, widht, height) > threshold ? (byte)255 : (byte)0;
                        p += 3;
                    }
                    p += offset;
                }
                newBmp.UnlockBits(newData);
                return newBmp;
            }
        }

        /// <summary>
        /// OTSU阈值法二值化
        /// </summary>
        public static Bitmap OtsuThreshold(this Bitmap bmp)
        {
            int width = bmp.Width, height = bmp.Height;
            byte threshold = 0;
            int[] hist = new int[256];

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* p = (byte*)data.Scan0;
                int offset = data.Stride - width * 4;
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        hist[p[0]]++;
                        p += 4;
                    }
                    p += offset;
                }
                bmp.UnlockBits(data);
            }

            double allSum = 0, smallSum = 0;
            int allPixelNumber = 0, smallPixelNumber = 0;
            //计算灰度为I的像素出现的概率
            for (int i = 0; i < 256; i++)
            {
                allSum += i * hist[i];
                allPixelNumber += hist[i];
            }
            double maxValue = -1.0;
            for (int i = 0; i < 256; i++)
            {
                smallPixelNumber += hist[i];
                int bigPixelNumber = allPixelNumber - smallPixelNumber;
                if (bigPixelNumber == 0)
                {
                    break;
                }
                smallSum += i * hist[i];
                double bigSum = allSum - smallSum;
                double smallProbability = smallSum / smallPixelNumber;
                double bigProbability = bigSum / bigPixelNumber;
                double probability = smallPixelNumber * smallProbability + bigPixelNumber * bigProbability * bigProbability;
                if (probability > maxValue)
                {
                    maxValue = probability;
                    threshold = (byte)i;
                }
            }
            return Threshoding(bmp, threshold);
        }

        /// <summary>
        /// 获取有效图形并调整为可平均分割的大小
        /// </summary>
        /// <param name="bmp">待处理的图片</param>
        /// <param name="gray">灰度阈值</param>
        /// <param name="charCount">字符数量</param>
        /// <returns></returns>
        public static Bitmap ToValid(this Bitmap bmp, int gray, int charCount)
        {
            int posx1 = bmp.Width; int posy1 = bmp.Height;
            int posx2 = 0; int posy2 = 0;
            for (int i = 0; i < bmp.Height; i++)      //找有效区
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    int pixelValue = bmp.GetPixel(j, i).R;
                    if (pixelValue < gray)     //根据灰度值
                    {
                        if (posx1 > j) posx1 = j;
                        if (posy1 > i) posy1 = i;

                        if (posx2 < j) posx2 = j;
                        if (posy2 < i) posy2 = i;
                    }
                }
            }
            // 确保能整除
            int span = charCount - (posx2 - posx1 + 1) % charCount;   //可整除的差额数
            if (span < charCount)
            {
                int leftSpan = span / 2;    //分配到左边的空列 ，如span为单数,则右边比左边大1
                if (posx1 > leftSpan)
                    posx1 = posx1 - leftSpan;
                if (posx2 + span - leftSpan < bmp.Width)
                    posx2 = posx2 + span - leftSpan;
            }
            //复制新图
            Rectangle cloneRect = new Rectangle(posx1, posy1, posx2 - posx1 + 1, posy2 - posy1 + 1);
            Bitmap newBmp = bmp.Clone(cloneRect, bmp.PixelFormat);
            return newBmp;
        }

        /// <summary>
        /// 去除空白边界获取有效的图形
        /// </summary>
        /// <param name="bmp">待处理的图片</param>
        /// <param name="gray">灰度阈值</param>
        /// <returns></returns>
        public static Bitmap ToValid(this Bitmap bmp, int gray)
        {
            int posx1 = bmp.Width; int posy1 = bmp.Height;
            int posx2 = 0; int posy2 = 0;
            for (int i = 0; i < bmp.Height; i++)      //找有效区
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    int pixelValue = bmp.GetPixel(j, i).R;
                    if (pixelValue < gray)     //根据灰度值
                    {
                        if (posx1 > j) posx1 = j;
                        if (posy1 > i) posy1 = i;

                        if (posx2 < j) posx2 = j;
                        if (posy2 < i) posy2 = i;
                    }
                }
            }
            //复制新图
            Rectangle cloneRect = new Rectangle(posx1, posy1, posx2 - posx1 + 1, posy2 - posy1 + 1);
            return bmp.Clone(cloneRect, bmp.PixelFormat);
        }

        /// <summary>
        /// 平均分割图片
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="rowNum">水平上分割数</param>
        /// <param name="colNum">垂直上分割数</param>
        /// <returns>分割好的图片数组</returns>
        public static Bitmap[] GetSplitPics(this Bitmap bmp, int rowNum, int colNum)
        {
            if (rowNum == 0 || colNum == 0)
                return null;
            int singW = bmp.Width / rowNum;
            int singH = bmp.Height / colNum;
            Bitmap[] picArray = new Bitmap[rowNum * colNum];

            for (int i = 0; i < colNum; i++)
            {
                for (int j = 0; j < rowNum; j++)
                {
                    Rectangle cloneRect = new Rectangle(j * singW, i * singH, singW, singH);
                    picArray[i * rowNum + j] = bmp.Clone(cloneRect, bmp.PixelFormat);//复制小块图
                }
            }
            return picArray;
        }

        /// <summary>
        /// 固定阈值的二值化
        /// </summary>
        /// <param name="bmp">待处理的图片</param>
        /// <param name="threshold">灰度阈值</param>
        /// <returns> </returns>
        public static Bitmap Threshoding(this Bitmap bmp, byte threshold)
        {
            int width = bmp.Width, height = bmp.Height;
            Bitmap newBmp = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), bmp.PixelFormat);
            BitmapData data = newBmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* p = (byte*)data.Scan0;
                int offset = data.Stride - width * 4;
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        byte r = p[2];
                        byte g = p[1];
                        byte b = p[0];
                        byte gray = (byte)((r * 19595 + g * 38469 + b * 7472) >> 16);
                        if (gray >= threshold)
                        {
                            p[0] = p[1] = p[2] = 255;
                        }
                        else
                        {
                            p[0] = p[1] = p[2] = 0;
                        }
                        p += 4;
                    }
                    p += offset;
                }
                newBmp.UnlockBits(data);
                return newBmp;
            }
        }

        /// <summary>
        /// 获取灰度图片的点阵描述字符串，1表示黑点，0表示空白
        /// </summary>
        /// <param name="bmp">待处理的图片</param>
        /// <param name="gray">灰度临界值</param>
        /// <param name="lineBreak">是否换行，默认false</param>
        /// <returns></returns>
        public static string ToCodeString(this Bitmap bmp, int gray, bool lineBreak = false)
        {
            string code = string.Empty;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color piexl = bmp.GetPixel(x, y);
                    if (piexl.R < gray)
                    {
                        code += "1";
                    }
                    else
                    {
                        code += "0";
                    }
                }
                if (lineBreak)
                {
                    code += "\r\n";
                }
            }
            return code;
        }

        private static byte GetAverageColor(byte[,] source, int x, int y, int w, int h)
        {
            int result = source[x, y]
                + (x == 0 ? 255 : source[x - 1, y])
                + (x == 0 || y == 0 ? 255 : source[x - 1, y - 1])
                + (x == 0 || y == h - 1 ? 255 : source[x - 1, y + 1])
                + (y == 0 ? 255 : source[x, y - 1])
                + (y == h - 1 ? 255 : source[x, y + 1])
                + (x == w - 1 ? 255 : source[x + 1, y])
                + (x == w - 1 || y == 0 ? 255 : source[x + 1, y - 1])
                + (x == w - 1 || y == h - 1 ? 255 : source[x + 1, y + 1]);
            return (byte)(result / 9);
        }

        private static byte GetGrayColorValue(Color pixel)
        {
            byte r = pixel.R, g = pixel.G, b = pixel.B;
            if (r + b + g != 0)
            {
                //Gray = 0.299*R + 0.587*G + 0.114*B 灰度计算公式
                return (byte)((r * 19595 + g * 38469 + b * 7472) >> 16);
            }
            //否则，返回白色
            return 255;
        }
    }
}