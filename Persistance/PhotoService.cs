using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using vcar.Core;
using vcar.Core.Models;

namespace vcar.Persistance
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork UnitOfWork;

        public PhotoService(IUnitOfWork IUnitOfWork)
        {
            this.UnitOfWork = IUnitOfWork;

        }
        public async Task<Photo> UploadPhoto(Car car, IFormFile File, string folderPath, string fileName)
        {

            var filePath = Path.Combine(folderPath, fileName);

            if (string.IsNullOrEmpty(car.thumbnail))
                car.thumbnail = fileName;

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await File.CopyToAsync(stream);
            }

            var Photo = new Photo
            {
                Name = fileName,
            };

            car.Photos.Add(Photo);
            await UnitOfWork.Complete();

            return Photo;
        }
    }
}