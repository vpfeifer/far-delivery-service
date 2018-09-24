using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryService.Core.Entities;
using DeliveryService.Core.Extensions;
using DeliveryService.Core.Interfaces.Repositories;
using DeliveryService.Core.Interfaces.Services;

namespace DeliveryService.Core.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IPointRepository _points;
        public DeliveryService(IPointRepository points)
        {
            _points = points;
        }

        public async Task<Delivery> FindMinTimeRoutesAsync(Point from, Point to)
        {
            var possibleDeliveries = CreatePossibleDeliveries(from);

            while (possibleDeliveries.Any())
            {
                var minTimeDelivery = possibleDeliveries.MinTimeDelivery();

                var lastDeliveryRoute = minTimeDelivery.Routes.LastOrDefault();

                var currentPosition = await _points.GetWithRoutesAsync(lastDeliveryRoute.ToId);

                if (currentPosition.Equals(to))
                {
                    return minTimeDelivery;
                }

                foreach (var route in currentPosition.Routes)
                {
                    var possibleDelivery = minTimeDelivery.Clone();
                    possibleDelivery.AddRoute(route);
                    possibleDeliveries.Add(possibleDelivery);
                }

                possibleDeliveries.Remove(minTimeDelivery);
            }
            return null;
        }

        private ICollection<Delivery> CreatePossibleDeliveries(Point from)
        {
            var possibleDeliveries = new List<Delivery>();

            foreach (var route in from.Routes)
            {
                var possibleDelivery = new Delivery();
                possibleDelivery.AddRoute(route);
                possibleDeliveries.Add(possibleDelivery);
            }

            return possibleDeliveries;
        }
    }
}

