using BlazorInputFile;
using CarMarket.BusinessLogic.Image.Service;
using CarMarket.Core.Image.Domain;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CarMarket.Core.Image.Exceptions;

namespace CarMarket.UI.Services.Image
{
    public class ImageUploader
    {
        public static async Task<ImageModel> UploadImage(IFileListEntry[] files)
        {
            var file = files.FirstOrDefault();

            if ((file != null) && (ImageService.IsImage(file.Name)))
            {
                var ms = new MemoryStream();
                await file.Data.CopyToAsync(ms);

                return new ImageModel
                {
                    ImageData = ms.ToArray(),
                    ImageTitle = file.Name
                };
            }
            else throw new FileNotSupportedException($"File {file.Name} is not supported.");
        }
    }
}
