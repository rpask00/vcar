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
    public class PhotoRepository : IPhotoRepository
    {

        private VcarContext _context;
        public PhotoRepository(VcarContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Photo>> GetPhotos(int carId)
        {

            // var car = await _context.Cars.Include(c => c.Photos).SingleAsync(c => c.Id == carId);
            return await _context.Photos.Where(p => p.CarId == carId).ToListAsync();

        }


    }


}
