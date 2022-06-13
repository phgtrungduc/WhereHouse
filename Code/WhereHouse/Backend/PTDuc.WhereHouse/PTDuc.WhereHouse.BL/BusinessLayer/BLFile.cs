using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.EntityModels;
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

        public ServiceResult DeleteFile(FileUpload fileUpload, string folderPath)
        {
            var res = new ServiceResult();
            try
            {
                folderPath = string.IsNullOrEmpty(folderPath) ? "uploads" : folderPath;
                var filePath = string.IsNullOrEmpty(fileUpload.filePath) ? Path.Combine(folderPath,fileUpload.fileName) : fileUpload.filePath;
                if (!System.IO.File.Exists(filePath))
                {
                    res.StatusCode = (int)Enumeration.ResultCode.NotExistFile;
                    res.Data = false;
                }
                else {
                    System.IO.File.Delete(filePath);
                    res.Data = true;
                }
            }
            catch (Exception e)
            {
                res.Data = e;
                res.StatusCode = (int)Enumeration.ResultCode.Failed;
            }
            return res;
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
