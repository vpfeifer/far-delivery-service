using System.Threading.Tasks;
using DeliveryService.Core.Entities;

namespace DeliveryService.Core.Interfaces.Services
{
    public interface IDeliveryService
    {
        Task<Delivery> FindMinTimeRoutesAsync(Point from, Point to);
    }
}