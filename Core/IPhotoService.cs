using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using vcar.Core.Models;

namespace vcar.Core
{
    public interface IPhotoService
    {
        Task<Photo> UploadPhoto(Car car, IFormFile File);
    }
}
