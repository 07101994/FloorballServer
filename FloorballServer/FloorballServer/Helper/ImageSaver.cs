using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FloorballServer.Helper
{
    public class ImageSaver
    {

        private static readonly string ImagePath = "~/TeamImages";

        public static void SaveImage(byte[] Image, string name)
        {
            string directoryPath = GetImagesPath();

            using (var fs = new BinaryWriter(new FileStream(directoryPath + "/" + name, FileMode.Append)))
            {
                fs.Write(Image);
            }
        }

        private static string GetImagesPath()
        {
            string directoryPath = HttpContext.Current.Server.MapPa‌​th(ImagePath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            return directoryPath;
        }
    }
}