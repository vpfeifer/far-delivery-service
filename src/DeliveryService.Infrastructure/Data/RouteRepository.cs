using System.Collections.Generic;
using System.Threading.Tasks;
using DeliveryService.Core.Entities;
using DeliveryService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Infrastructure.Data
{
    public class RouteRepository : BaseRepository<Route>
    {
        private readonly DeliveryContext _context;

        public RouteRepository(DeliveryContext context)
            : base(context)
        {
            _context = context;
        }

        internal override DbSet<Route> EntitySet => _context.Routes;
    }
}