using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAnnotations;
using vcar.Core.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace vcar.Core
{
    public interface IPhotoRepository
    {
        Task<ICollection<Photo>> GetPhotos(int carId);
        void RemovePhoto(Photo photo);
    }

}
