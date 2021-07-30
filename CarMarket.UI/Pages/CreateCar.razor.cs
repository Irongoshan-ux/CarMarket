using CarMarket.Core.Car.Domain;
using CarMarket.Core.Image.Domain;
using CarMarket.UI.HttpRepository;
using CarMarket.UI.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace CarMarket.UI.Pages
{
    public partial class CreateCar
    {
        protected CarModel _car = new CarModel();
        protected SuccessNotification _notification;

        [Inject]
        public ICarHttpRepository CarRepo { get; set; }

        private async Task Create()
        {
            await CarRepo.CreateCar(_car);
            _notification.Show();
        }

        private void AssignImageUrl(string imgUrl) => _car.CarImages.Add(new ImageModel { ImageUrl = imgUrl });
    }
}
