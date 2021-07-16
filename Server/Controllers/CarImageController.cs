using CarMarket.Core.Image.Domain;
using CarMarket.Core.Image.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace CarMarket.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarImageController : ControllerBase
    {
        private ICarImageService _carImageService;

        [HttpPost]
        public IActionResult UploadImage()
        {
            foreach (var file in Request.Form.Files)
            {
                var image = new CarImage();
                image.ImageTitle = file.FileName;

                MemoryStream ms = new();
                file.CopyTo(ms);
                image.ImageData = ms.ToArray();

                ms.Close();
                ms.Dispose();

                //db.Images.Add(img);
                //db.SaveChanges();
            }

            //return View("Index");

            throw new NotImplementedException();
        }
    }
}
