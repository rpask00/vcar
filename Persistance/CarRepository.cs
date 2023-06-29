using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAnnotations;
using vcar.Core.Models;
using System.Collections.Generic;
using vcar.Core;
using System.Linq;
using System;
using System.Linq.Expressions;
using vcar.Extensions;


namespace vcar.Persistance
{
    public class CarRepository : ICarRepository
    {

        private VcarContext _context;
        public CarRepository(VcarContext context)
        {
            _context = context;
        }
        public async Task<QueryResult<Car>> GetAll(CarQuery carQuery, bool loadExternal = false)
        {
            var result = new QueryResult<Car>();
            List<Car> cars;

            if (!loadExternal)
            {
                cars = await _context.Cars.ToListAsync();
                result.Items = await _context.Cars.ToListAsync();
                result.Size = cars.Count;
                return result;
            }

            var query = _context.Cars
            .Include(c => c.Model)
                .ThenInclude(m => m.Make)
            .Include(c => c.Features)
                .ThenInclude(f => f.Feature)
            .AsQueryable();

            query = query.ApplyFiltering(carQuery);

            if (!string.IsNullOrEmpty(carQuery.sortBy))
            {
                var SortingDict = new Dictionary<string, Expression<Func<Car, object>>>
                {
                    ["Name"] = c => c.ContactName,
                    ["Price"] = c => c.Price,
                    ["Email"] = c => c.Email,
                    ["Make"] = c => c.Model.Make.Name,
                    ["Model"] = c => c.Model.Name,
                    ["Year"] = c => c.year,
                };

                query = query.ApplyOrdering(carQuery, SortingDict);
            }
            result.Size = query.Count();
            
            query = query.ApplyPaging(carQuery);
            
            result.Items = await query.ToListAsync();

            return result;
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
