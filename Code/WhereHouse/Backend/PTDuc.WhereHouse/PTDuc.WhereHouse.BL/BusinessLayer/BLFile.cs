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
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{

    public class BLFile : BLBase<DBContext.Models.File, FileDTO>, IBLFile
    {
        IDLFile _dlFile;
        public BLFile(IDLFile dlFile, IMapper mapper) : base(dlFile, mapper)
        {
            _dlFile = dlFile;
        }

        public ServiceResult DeleteFile(FileUpload fileUpload, string webRootPath)
        {
            var res = new ServiceResult();
            try
            {
                var webFilePath = Path.Combine(webRootPath, fileUpload.filePath);
                if (!System.IO.File.Exists(webFilePath))
                {
                    res.StatusCode = (int)Enumeration.ResultCode.NotExistFile;
                    res.Data = false;
                }
                else
                {
                    System.IO.File.Delete(webFilePath);
                    res.Data = true;
                    var insertSuccess = this.Delete(fileUpload.FileId.Value.ToString());
                    if (insertSuccess)
                    {
                        res.Data = true;
                    }
                    else
                    {
                        res.Data = false;
                    }
                }
            }
            catch (Exception e)
            {
                res.Data = e;
                res.StatusCode = (int)Enumeration.ResultCode.Failed;
            }
            return res;
        }

        public ServiceResult UploadFile(FileUpload fileUpload, string webRootPath, string folderPath)
        {
            var res = new ServiceResult();
            var webFolderPath = Path.Combine(webRootPath, folderPath);
            if (!Directory.Exists(webFolderPath))
                Directory.CreateDirectory(webFolderPath);
            var file = fileUpload.file;
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(folderPath, fileName);
            var webFilePath = Path.Combine(webRootPath, filePath);
            using (var strem = System.IO.File.Create(webFilePath))
            {
                file.CopyTo(strem);
            }
            var fileAdd = new FileDTO { FileId = Guid.NewGuid(), FileName = fileName, FilePath = filePath };
            var success = this.Insert(fileAdd);
            if (success)
            {
                res.Data = fileAdd;
            }
            else
            {
                res.StatusCode = (int)Enumeration.ResultCode.Failed;
            }
            return res;
        }
    }
}
