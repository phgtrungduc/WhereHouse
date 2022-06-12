using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.EntityModels.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    
    public class BLFile : BLBase<PTDuc.WhereHouse.DBContext.Models.File>,IBLFile
    {
        IDLFile _dlFile;
        public BLFile(IDLFile dlFile) : base(dlFile)
        {
            _dlFile = dlFile;
        }

        public bool UploadFile(FileUpload fileUpload,string folderPath)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            var file = fileUpload.file;
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var strem = System.IO.File.Create(filePath))
            {
                file.CopyToAsync(strem);
            }
            return true;
        }
    }
}
