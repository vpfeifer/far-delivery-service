using System.Collections.Generic;
using System.Threading.Tasks;
using DeliveryService.Core.Entities;
using DeliveryService.Core.Interfaces;
using DeliveryService.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Infrastructure.Data
{
    public class PointRepository : BaseRepository<Point>, IPointRepository
    {
        private readonly DeliveryContext _context;

        public PointRepository(DeliveryContext context)
            : base(context)
        {
            _context = context;
        }

        internal override DbSet<Point> EntitySet => _context.Points;

        public async Task<Point> GetWithRoutesAsync(long id)
        {
            return await EntitySet.Include(p => p.Routes)
                            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}