using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace molharFoto
{
    class Program
    {

        //static string logoPath;

        static void Main(string[] args)
        {

            Console.WriteLine(@"Place Watermark on Your Images");
            Console.WriteLine(@"by Dante Souto (dantesouto@gmail.com)");
            Console.WriteLine(@"v:{0}", "1.0");
            Console.WriteLine("");

            for (int i = 0;i< args.Length; i++)
            {
                string original = args[i];

                try
                {
                    string tmpFile = Path.GetFileName(original);
                    Console.Write("{0}/{1} - Processing '{2}', please wait...", (i + 1).ToString("D2"), args.Length.ToString("D2"), tmpFile);

                    string result = ProcessarImagem(original);

                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(new String(' ', Console.BufferWidth));
                    Console.SetCursorPosition(0, Console.CursorTop - 1);

                    Console.WriteLine("{0}/{1} - '{2}' saved as '\\{3}'.", (i + 1).ToString("D2"), args.Length.ToString("D2"), tmpFile, Path.Combine(ConfigHelper.OutputFolder, Path.GetFileName(result)));

                } catch(Exception ex)
                {
                    Console.WriteLine("Error: '{0}'.\n{1}", original, ex.ToString());
                }

            }

            Console.WriteLine("Ready. Press any key to quit!");
            Console.ReadKey();

        }

        static string ProcessarImagem(string original)
        {
            BitmapSource bitmap = new BitmapImage(new Uri(original, UriKind.Absolute));
            BitmapSource logobitmap = new BitmapImage(new Uri(ConfigHelper.WatermarkFullPath, UriKind.Absolute));
            Bitmap mainImgeBitmap = ImageHelper.BitmapSourceToBitmap(bitmap);
            mainImgeBitmap = ImageHelper.ScaleImage(mainImgeBitmap,ConfigHelper.MaxWidth, ConfigHelper.MaxHeight);
            Bitmap logoImageBitmap = ImageHelper.BitmapSourceToBitmap(logobitmap, ImageHelper.ImageType.PNG);

            double newRes = ConfigHelper.WatermarkScale;    // 0.20;
            int newW = (int)(mainImgeBitmap.Width * newRes); 
            int newH = (int)(mainImgeBitmap.Height * newRes);
            logoImageBitmap = ImageHelper.ScaleImage(logoImageBitmap, newW, newH);
            logoImageBitmap = ImageHelper.SetBitmapResolution(
                logoImageBitmap
                , (float)bitmap.DpiX
                , (float)bitmap.DpiY
                );
            bitmap = ImageHelper.BitmapToBitmapSource(ImageHelper.Superimpose(mainImgeBitmap, logoImageBitmap));

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            string resultado;

            resultado = ConfigHelper.GetFullOutputFileName(original, ".png");
            //string dir = Path.GetDirectoryName(original);
            //string file = Path.GetFileNameWithoutExtension(original);
            //resultado = Path.Combine(dir, file + "_wm.png");

            int fileCount = 0;
            while (true)
            {
                if (!File.Exists(resultado))
                {
                    break;
                }
                fileCount++;
                resultado = ConfigHelper.GetFullOutputFileName(original, @"(" + fileCount.ToString() + @").png");
                //resultado = Path.Combine(dir, file + "_wm_" + fileCount.ToString() + ".png");
            }

            var fs = new FileStream(resultado, FileMode.Create);
            encoder.Save(fs);
            fs.Close();

            return resultado;
        }
    }
}
