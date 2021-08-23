using AutoFixture;
using CarMarket.BusinessLogic.Car.Service;
using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Repository;
using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Repository;
using CarMarket.Core.User.Service;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace CarMarket.BusinessLogic.Tests
{
    public class CarServiceTests
    {
        private readonly Mock<ICarRepository> _carRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<UserManager<UserModel>> _userManagerMock;
        private readonly CarService _carService;

        public CarServiceTests()
        {
            _carRepositoryMock = new Mock<ICarRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userServiceMock = new Mock<IUserService>(_userRepositoryMock.Object, _userManagerMock.Object);
            _carService = new CarService(_carRepositoryMock.Object, _userServiceMock.Object);
        }

        [Fact]
        public async void Create_ShouldCreateCar()
        {
            // arrange
            var car = new Fixture().Create<CarModel>();

            // act
            var result = await _carService.CreateAsync(car);

            // assert
            Assert.NotNull(result);
        }
    }
}
