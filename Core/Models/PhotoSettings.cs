using System.Linq;
using System;
using System.IO;

namespace vcar.Core.Models
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] Allowed_file_types { get; set; }

        public bool isSuported(string type)
        {
            return Allowed_file_types.Any(t => t == Path.GetExtension(type).ToLower());
        }
    }
}