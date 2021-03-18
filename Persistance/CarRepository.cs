using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAnnotations;
using vcar.Core.Models;
using System.Collections.Generic;
using vcar.Core;

namespace vcar.Persistance
{
    public class CarRepository : ICarRepository
    {

        private VcarContext _context;
        public CarRepository(VcarContext context)
        {
            _context = context;
        }


        public async Task<List<Car>> GetAll(bool loadExternal = false)
        {
            if (!loadExternal)
                return await _context.Cars.ToListAsync();

            return await _context.Cars
            .Include(c => c.Model)
                .ThenInclude(m => m.Make)
            .Include(c => c.Features)
                .ThenInclude(f => f.Feature)
            .ToListAsync();
        }

        public async Task<Car> Get(int id, bool loadExternal = false)
        {
            if (!loadExternal)
                return await _context.Cars.SingleOrDefaultAsync(c => c.Id == id);

            return await _context.Cars
            .Include(c => c.Model)
                .ThenInclude(m => m.Make)
            .Include(c => c.Features)
                .ThenInclude(f => f.Feature)
            .SingleOrDefaultAsync(c => c.Id == id);

        }

        public void Add(Car car)
        {
            _context.Cars.Add(car);
        }

        public void Remove(Car car)
        {
            _context.Cars.Remove(car);

        }
    }
}