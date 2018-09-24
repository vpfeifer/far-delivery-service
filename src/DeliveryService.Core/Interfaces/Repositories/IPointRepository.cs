using System.Threading.Tasks;
using DeliveryService.Core.Entities;

namespace DeliveryService.Core.Interfaces.Repositories
{
    public interface IPointRepository : IRepository<Point>
    {
        Task<Point> GetWithRoutesAsync(long id);
    }
}