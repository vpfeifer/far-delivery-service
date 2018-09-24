using System.Collections.Generic;
using System.Linq;
using DeliveryService.Core.Entities;

namespace DeliveryService.Core.Extensions
{
    public static class DeliveryExtension
    {
        public static Delivery MinTimeDelivery(this IEnumerable<Delivery> deliveries)
        {
            return deliveries.OrderBy(r => r.TotalTime)
                        .ThenBy(r => r.TotalCost)
                        .FirstOrDefault();
        }

        public static Delivery Clone(this Delivery delivery)
        {
            var clone = new Delivery();
            clone.Routes = new List<Route>(delivery.Routes);
            clone.TotalCost = delivery.TotalCost;
            clone.TotalTime = delivery.TotalTime;
            return clone;
        }

        public static void AddRoute(this Delivery delivery, Route route)
        {
            delivery.Routes.Add(route);
            delivery.TotalCost += route.Cost;
            delivery.TotalTime += route.Time;
        }
    }
}