using System.Threading;
using System.Threading.Tasks;

namespace CarMarket.UI.Services.CarValuer
{
    public interface IHttpCarValuerService
    {
        public Task<CostPerYearResult> AddAsync(CarViewModel model, CancellationToken token);

        public Task<CostPerYearResult> GetAsync(long carId, CancellationToken cancellationToken);
    }
}
