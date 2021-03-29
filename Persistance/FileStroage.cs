using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using vcar.Core;

namespace vcar.Persistance
{
    public class FileStorage : IFileStorage
    {
        public async Task<string> saveFile(IFormFile File, string uploadsFolderPath)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(File.FileName);

            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await File.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}