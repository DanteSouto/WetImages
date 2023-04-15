using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace molharFoto
{
    public static class ConfigHelper
    {
        private static bool initialized = false;
        private static int maxWidth;
        private static int maxHeight;
        private static string watermarkFullPath;
        private static string outputFolder;
        private static double watermarkScale;

        public static int MaxWidth
        {
            get 
            {
                if (!initialized)
                {
                    Init();
                }
                return maxWidth;
            }
        }

        public static int MaxHeight
        {
            get
            {
                if(!initialized)
                {
                    Init();
                }
                return maxHeight;
            }
        }

        public static string WatermarkFullPath
        {
            get
            {
                if(!initialized)
                {
                    Init();
                }
                return watermarkFullPath;
            }
        }

        public static string OutputFolder
        {
            get
            {
                if (!initialized)
                {
                    Init();
                }
                return outputFolder;
            }
        }

        public static double WatermarkScale
        {
            get
            {
                if(!initialized)
                {
                    Init();
                }
                return watermarkScale;
            }
        }

        public static string GetFullOutputFileName(string filename, string extencion)
        {
            string dir = Path.Combine(Path.GetDirectoryName(filename), OutputFolder);
            
            try
            {
                bool exists = System.IO.Directory.Exists(dir);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Panic! Error creating output folder!");
                Console.WriteLine("MaxWidth:{0}\nMaxHeight:{1}\nWatermarkFullPath:{2}\nWatermarkScale:{3}\nOutputFolder:{4}", maxWidth, maxHeight, watermarkFullPath, watermarkScale, outputFolder);
                Console.WriteLine(ex.ToString());
                Console.WriteLine(@"Press any key to quit!");
                Console.ReadKey();
                Environment.Exit(1);
            }
            string file = Path.GetFileNameWithoutExtension(filename);
            string ret = Path.Combine(dir, file + extencion);

            return ret;

        }

        public static void Init(string appConfigFile)
        {
            string sConfigFileName = appConfigFile;
            DoInt(sConfigFileName);
        }
        
        public static void Init()
        {
            string sConfigFileName;
            sConfigFileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\wetconfig.xml";
            sConfigFileName = sConfigFileName.Replace("file:", "").Substring(1);
            DoInt(sConfigFileName);
        }

        private static void DoInt(string sConfigFileName)
        {
            initialized = true;
            try
            {
                XmlDocument oXML = new XmlDocument();
                oXML.Load(sConfigFileName);
                try
                {
                    maxWidth = System.Convert.ToInt32(oXML.SelectSingleNode("/config/MaxWidth").InnerText);
                }
                catch
                {
                    maxWidth = 1024;
                }
                try
                {
                    maxHeight = System.Convert.ToInt32(oXML.SelectSingleNode("/config/MaxHeight").InnerText);
                }
                catch
                {
                    maxHeight = 768;
                }

                watermarkFullPath = oXML.SelectSingleNode("/config/WatermarkFullPath").InnerText;
                if(watermarkFullPath == "")
                {
                    string appDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                    watermarkFullPath = Path.Combine(appDir, "watermark.png");
                }
                else if(!Path.IsPathRooted(watermarkFullPath))
                {
                    string appDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                    watermarkFullPath = Path.Combine(appDir, watermarkFullPath);
                }

                try
                {
                    watermarkScale = double.Parse(oXML.SelectSingleNode("/config/WatermarkScale").InnerText, CultureInfo.InvariantCulture);
                    //watermarkScale = System.Convert.ToDouble(oXML.SelectSingleNode("/config/WatermarkScale").InnerText);
                }
                catch
                {
                    watermarkScale = 0.2;
                }
                
                outputFolder = oXML.SelectSingleNode("/config/OutputFolder").InnerText;
                if(outputFolder == "")
                {
                    outputFolder = "WetImages";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Panic! Error reading config file!");
                Console.WriteLine("MaxWidth:{0}\nMaxHeight:{1}\nWatermarkFullPath:{2}\nWatermarkScale:{3}\nOutputFolder:{4}", maxWidth, maxHeight, watermarkFullPath, watermarkScale, outputFolder);
                Console.WriteLine(ex.ToString());
                Console.WriteLine(@"Press any key to quit!");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
    }
}
