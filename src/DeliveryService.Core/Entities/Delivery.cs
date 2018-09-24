using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryService.Core.Entities
{
    public class Delivery
    {
        public Delivery()
        {
            Routes = new List<Route>();
        }

        public ICollection<Route> Routes { get; set; }
        public int TotalCost { get; set; }
        public int TotalTime { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Delivery with routes :");
            foreach (var route in Routes)
            {
                stringBuilder.AppendLine($"{route.FromId} => {route.ToId}");
            }
            stringBuilder.AppendLine($"Total Time : {TotalTime}");
            stringBuilder.AppendLine($"Total Cost : {TotalCost}");
            return stringBuilder.ToString();
        }
    }
}