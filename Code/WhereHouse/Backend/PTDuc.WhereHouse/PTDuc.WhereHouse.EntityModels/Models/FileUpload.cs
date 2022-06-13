using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.EntityModels.Models
{
    public class FileUpload
    {
        public Guid? FileId { get; set; }
        public IFormFile file { get; set; }
        public string category { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
    }
}
