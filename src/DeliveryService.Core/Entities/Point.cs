using System.Collections.Generic;

namespace DeliveryService.Core.Entities
{
    public class Point : Entity
    {
        public Point()
        {
            Routes = new List<Route>();    
        }

        public string Name { get; set; }

        public ICollection<Route> Routes { get; set; }

        public override string ToString()
        {
            return $"{base.Id} : {this.Name}";
        }

        public override bool Equals(object obj)
        {
            var point = (Point) obj;
            return base.Id == point.Id && this.Name == point.Name;
        }

        public override int GetHashCode()
        {
            return base.Id.GetHashCode() ^ this.Name.GetHashCode();
        }
    }
}