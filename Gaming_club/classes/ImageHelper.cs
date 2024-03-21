using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Gaming_club.classes
{
    public class ImageHelper
    {
        public static string SaveImage(string imagePath, ImageSource imageSource, string baseDirectory, string directoryName, string fileNamePrefix)
        {
            string imagesDirectory = Path.Combine(baseDirectory, directoryName);

            if (!Directory.Exists(imagesDirectory))
            {
                Directory.CreateDirectory(imagesDirectory);
            }

            string fileName = $"{fileNamePrefix}_{DateTime.Now.Ticks}_game_image.jpg";
            string filePath = Path.Combine(imagesDirectory, fileName);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapImage)imageSource));
                encoder.Save(memoryStream);

                File.WriteAllBytes(filePath, memoryStream.ToArray());
            }
            return filePath;
        }
    }
}