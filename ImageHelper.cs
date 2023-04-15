using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Point = System.Drawing.Point;

namespace molharFoto
{

    // https://stackoverflow.com/questions/27376937/adding-transparent-watermark-pngimage-to-another-image

    public static class ImageHelper
    {
        #region Enums

        public enum ImageType
        {
            Bitmap = 0,

            PNG = 1
        }

        #endregion

        #region Public Methods and Operators

        public static Bitmap BitmapSourceToBitmap(BitmapSource bitmapsource, ImageType type = ImageType.Bitmap)
        {
            Bitmap bitmap;
            using (var outStream = new MemoryStream())
            {
                BitmapEncoder enc = null;
                if (type == ImageType.Bitmap)
                {
                    enc = new BmpBitmapEncoder();
                }
                else
                {
                    enc = new PngBitmapEncoder();
                }

                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new Bitmap(outStream);


            }

            return bitmap;
        }

        public static Image BitmapSourceToImage(BitmapSource image)
        {
            var ms = new MemoryStream();
            var encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(ms);
            ms.Flush();
            return Image.FromStream(ms);
        }

        public static BitmapSource BitmapToBitmapSource(Bitmap source)
        {
            return Imaging.CreateBitmapSourceFromHBitmap(
                source.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        public static Bitmap SetBitmapResolution(Bitmap bitmap, float dpiX, float dpiY)
        {
            bitmap.SetResolution(dpiX, dpiY);
            return bitmap;
        }

        public static Bitmap Superimpose(Bitmap largeBmp, Bitmap smallBmp)
        {
            Graphics g = Graphics.FromImage(largeBmp);
            g.CompositingMode = CompositingMode.SourceOver;

            // smallBmp.MakeTransparent();

            // int x = largeBmp.Width - smallBmp.Width - margin;
            int x = (largeBmp.Width - smallBmp.Width) / 2;
            // int y = largeBmp.Height - smallBmp.Height - margin;
            int y = (largeBmp.Height - smallBmp.Height) / 2;
            g.DrawImage(smallBmp, new Point(x, y));

            return largeBmp;
        }

        //public static Bitmap Superimpose(Bitmap largeBmp, Bitmap smallBmp)
        //{
        //    Graphics g = Graphics.FromImage(largeBmp);
        //    g.CompositingMode = CompositingMode.SourceOver;

        //    // smallBmp.MakeTransparent();

        //    Image smallImage = ScaleImage(smallBmp, largeBmp.Width / 4, largeBmp.Height / 4);

        //    int x = (largeBmp.Width - smallImage.Width) / 2;
        //    int y = (largeBmp.Height - smallImage.Height) / 2;

        //    g.DrawImage(smallImage, new Point(x, y));

        //    return largeBmp;
        //}

        public static Bitmap ScaleImage(Bitmap bitmap, int maxWidth, int maxHeight)
        {

            //Image image = new Graphics.from
            var ratioX = (double)maxWidth / bitmap.Width;
            var ratioY = (double)maxHeight / bitmap.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(bitmap.Width * ratio);
            var newHeight = (int)(bitmap.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(bitmap, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }
        #endregion
    }
}
