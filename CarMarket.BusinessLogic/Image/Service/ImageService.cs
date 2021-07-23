using CarMarket.Core.Image.Domain;
using CarMarket.Core.Image.Service;
using System;

namespace CarMarket.BusinessLogic.Image.Service
{
    public class ImageService
    {
        public static string ConvertImageToDisplay(ImageModel image)
        {
            if (image is null) return string.Empty;

            var base64 = Convert.ToBase64String(image.ImageData);

            var result = string.Format("data:image/jpg;base64,{0}", base64);

            return result;
        }
    }
}
