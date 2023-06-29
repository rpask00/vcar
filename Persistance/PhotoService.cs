using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using vcar.Core;
using vcar.Core.Models;

namespace vcar.Persistance
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IFileStorage _fileStorage;
        private readonly IWebHostEnvironment _host;

        public PhotoService(
            IWebHostEnvironment webHostingEnvironment,
            IUnitOfWork IUnitOfWork,
            IFileStorage fileStorage
        )
        {
            _UnitOfWork = IUnitOfWork;
            _fileStorage = fileStorage;
            _host = webHostingEnvironment;

        }
        public async Task<Photo> UploadPhoto(Car car, IFormFile File)
        {

            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = await _fileStorage.saveFile(File, uploadsFolderPath);

            if (string.IsNullOrEmpty(car.thumbnail))
                car.thumbnail = fileName;

            var Photo = new Photo { Name = fileName };

            car.Photos.Add(Photo);
            await _UnitOfWork.Complete();

            return Photo;
        }
    }
}
