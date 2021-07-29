using System.Linq;
using CarMarket.Data.Car.Domain;

namespace CarMarket.Data.Car.Repository.RepositoryExtensions
{
    public static class RepositoryCarExtensions
    {
        public static IQueryable<CarEntity> Search(this IQueryable<CarEntity> cars, string searchTearm)
        {
            if (string.IsNullOrWhiteSpace(searchTearm))
                return cars;

            var lowerCaseSearchTerm = searchTearm.Trim().ToLower();

            return cars.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm));
        }
    }
}
