using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;

using System.Drawing.Drawing2D;
using System.IO;
using BAS.Repository.Model;


namespace BAS.Core.Helper
{
    public class PictureHelper
    {
        public static Bitmap Thumbnail(int width, int height, Image img, bool constant)
        {
            var newWidth = width;
            var newHeight = height;
            if (!constant)
            {
                var ratioX = (double)width / img.Width;
                var ratioY = (double)height / img.Height;
                var ratio = Math.Min(ratioX, ratioY);
                newWidth = (int)(img.Width * ratio);
                newHeight = (int)(img.Height * ratio);
            }
            var newImage = new Bitmap(newWidth, newHeight);
            //        graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //        graphics.DrawImage(img, new Rectangle(0, 0, newWidth, newHeight));
            Graphics.FromImage(newImage).DrawImage(img, 0, 0, newWidth, newHeight);
            return newImage;
        }

        public static void SaveImageToDirectory(Stream s, File_t file)
        {


       
            //JpegBitmapDecoder decoder = new JpegBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            //BitmapSource bitmapSource = decoder.Frames[0];
            //System.Windows.Controls.Image myImage = new System.Windows.Controls.Image();
            //myImage.Source = bitmapSource;


            using (var img = Image.FromStream(s, true, false))
            {
                try
                {
                    if (!CreateFolderIfNeeded(file.STAND_FILE))
                    {
                        throw new Exception("not possible to create folder");
                    }

                    if (!File.Exists(file.STAND_FILE))
                    {
                        using (var imagStand = PictureHelper.Thumbnail(1200, 1200, img, false))
                        {
                            imagStand.Save(file.STAND_FILE, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }
                    if (!File.Exists(file.THUM_FILE))
                    {
                        using (var imagSmall = PictureHelper.Thumbnail(560, 310, img, true))
                        {
                            imagSmall.Save(file.THUM_FILE, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }

                    if (!File.Exists(file.NEWTHUM_FILE))
                    {
                        using (var imagSmall = PictureHelper.Thumbnail(100, 100, img, true))
                        {
                            imagSmall.Save(file.NEWTHUM_FILE, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
        }

        public static void SaveImageToDirectory(byte[] image, File_t file)
        {
            using (var s = new MemoryStream(image))
            {
                SaveImageToDirectory(s, file);
            }
        }




        private static bool CreateFolderIfNeeded(string path)
        {


            path = Path.GetDirectoryName(path);
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }


        public static bool CheckFiles(File_t file)
        {
            return File.Exists(file.THUM_FILE)
                   && File.Exists(file.NEWTHUM_FILE)
                   && File.Exists(file.STAND_FILE);
        }







    }
}
