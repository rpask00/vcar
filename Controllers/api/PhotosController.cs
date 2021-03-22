using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAnnotations;
using AutoMapper;
using vcar.Controllers.Resources;
using vcar.Persistance;
using vcar.Core.Models;
using vcar.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Options;

namespace vcar.Controllers.api
{
    [Route("api/cars/{carId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _host;
        private readonly ICarRepository _CarRepository;
        private readonly IPhotoRepository _PhotoRepositroy;
        private readonly PhotoSettings _photoSettings;

        public PhotosController(
            IMapper mapper,
            ICarRepository CarRepository,
            IUnitOfWork UnitOfWork,
            IWebHostEnvironment webHostingEnvironment,
            IOptionsSnapshot<PhotoSettings> photoSettings,
            IPhotoRepository photoRepositroy
        )
        {
            _mapper = mapper;
            _CarRepository = CarRepository;
            _UnitOfWork = UnitOfWork;
            _host = webHostingEnvironment;
            _photoSettings = photoSettings.Value;
            _PhotoRepositroy = photoRepositroy;
        }


        [HttpGet]
        public async Task<IActionResult> GetPhotos(int carId)
        {
            var photos = await _PhotoRepositroy.GetPhotos(carId);
            return Ok(photos.Select(p => _mapper.Map<Photo, PhotoResource>(p)));
        }


        [HttpPost]
        public async Task<IActionResult> Upload(int carId, IFormFile File)
        {
            var car = await _CarRepository.Get(carId, loadExternal: false);

            if (car == null)
                return NotFound();

            if (File == null)
                return BadRequest("File not found");

            if (File.Length == 0)
                return BadRequest("Image too small");

            if (File.Length > _photoSettings.MaxBytes)
                return BadRequest("Image too big");

            if (!_photoSettings.isSuported(File.FileName))
                return BadRequest("File type not supported");


            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(File.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            // var stream = new FileStream(filePath, FileMode.Create);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await File.CopyToAsync(stream);
            }

            var Photo = new Photo
            {
                Name = fileName,
            };

            car.Photos.Add(Photo);
            await _UnitOfWork.Complete();

            return Ok(_mapper.Map<Photo, PhotoResource>(Photo));

        }


    }
}
