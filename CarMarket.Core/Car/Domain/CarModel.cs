using CarMarket.Core.Image.Domain;
using CarMarket.Core.User.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarMarket.Core.Car.Domain
{
    public class CarModel
    {
        public long Id { get; set; }

        public CarType CarType { get; set; }

        public ICollection<ImageModel> CarImages { get; set; }

        [Required]
        public UserModel Owner { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(25, int.MaxValue, ErrorMessage = "Unreliable price")]
        public int Price { get; set; }

        [Required]
        public Model Model { get; set; }

        [Required]
        public DataType Year { get; set; }
    }
}