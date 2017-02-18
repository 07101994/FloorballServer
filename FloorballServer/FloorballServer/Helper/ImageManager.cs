using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace FloorballServer.Helper
{
    public static class ImageManager
    {

        private static readonly string ImagePath = "~/TeamImages";

        public static void SaveImage(byte[] image, string name)
        {
            string directoryPath = GetImagesPath();

            using (var ms = new MemoryStream(image))
            {
                Image i = Image.FromStream(ms);
                i.Save(Path.Combine(directoryPath, name + i.GetExtension()));
            }
        }

        private static string GetExtension(this Image img)
        {
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                return ".jpg";
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                return ".bmp";
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                return ".png";
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Emf))
                return ".emf";
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Exif))
                return ".exif";
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                return ".gif";
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Icon))
                return ".ico";
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.MemoryBmp))
                return ".bmp";
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
                return ".tiff";
            else
                return ".wmf";
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

        public static byte[] GetImage(string name)
        {
            string imagePath = GetImagesPath();

            return File.ReadAllBytes(Path.Combine(imagePath,name));

        }

    }
}