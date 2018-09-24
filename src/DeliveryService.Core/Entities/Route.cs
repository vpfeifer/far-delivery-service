namespace DeliveryService.Core.Entities
{
    public class Route : Entity
    {
        public long FromId { get; set; }
        public long ToId { get; set; }
        public int Time { get; set; }
        public int Cost { get; set; }

        public override string ToString()
        {
            return $"{base.Id} : {this.FromId} => {this.ToId}";    
        }
    }
}