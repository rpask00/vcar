using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace vcar.Core
{
    public interface IFileStorage
    {
        Task<string> saveFile(IFormFile File, string uploadsFolderPath);
    }
}
