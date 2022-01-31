using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAnnotations;
using vcar.Core.Models;
using System.Collections.Generic;
using vcar.Core;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace vcar.Persistance
{
    public class PhotoRepository : IPhotoRepository
    {

        private readonly IWebHostEnvironment _host;
        private VcarContext _context;
        public PhotoRepository(VcarContext context, IWebHostEnvironment webHostingEnvironment)
        {
            _context = context;
            _host = webHostingEnvironment;
        }

        public async Task<ICollection<Photo>> GetPhotos(int carId)
        {

            // var car = await _context.Cars.Include(c => c.Photos).SingleAsync(c => c.Id == carId);
            // return car.Photos;
            return await _context.Photos.Where(p => p.CarId == carId).ToListAsync();
        }

        public void RemovePhoto(Photo photo)
        {
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            var filePath = Path.Combine(uploadsFolderPath, photo.Name);

            if ((File.Exists(filePath)))
                File.Delete(filePath);

            _context.Photos.Remove(photo);
        }


    }


}

