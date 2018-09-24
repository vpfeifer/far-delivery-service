using System.Collections.Generic;
using System.Threading.Tasks;
using DeliveryService.Core.Entities;
using DeliveryService.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DeliveryService.Infrastructure.Data
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : Entity
    {
        private readonly DeliveryContext _context;

        public BaseRepository(DeliveryContext context)
        {
            _context = context;
        }

        internal abstract DbSet<T> EntitySet { get; }
        public async Task<T> CreateAsync(T item)
        {
            await EntitySet.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task DeleteAsync(T item)
        {
            EntitySet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await EntitySet.ToListAsync();
        }

        public async Task<T> GetAsync(long id)
        {
            return await EntitySet.FindAsync(id);
        }

        public async Task UpdateAsync(T item)
        {
            EntitySet.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}