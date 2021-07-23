using CarMarket.Core.Image.Domain;

namespace CarMarket.Core.Image.Service
{
    public interface IImageService
    {
        string ConvertImageToDisplay(ImageModel image);
    }
}
