using System;
using System.Net;
using Android.Graphics;
using Services;

namespace KargozariHamrah.Utils
{
    public class BuildingUtilities
    {
        public static Bitmap GetImageFromUrl(string imageName)
        {
            Bitmap image = null;
            try
            {
                if (!imageName.ToLower().EndsWith(".png") && !imageName.ToLower().EndsWith(".jpg"))
                    imageName += ".jpg";
                var url = Utilities.WebApi + "/BuildingImages/" + imageName;
                using var client = new WebClient();
                var imageBytes = client.DownloadData(url);
                if (imageBytes != null)
                    image = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }

            return image;
        }
    }
}