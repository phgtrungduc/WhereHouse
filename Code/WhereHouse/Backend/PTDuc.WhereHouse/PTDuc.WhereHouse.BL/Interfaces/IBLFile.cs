using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.EntityModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTDuc.WhereHouse.EntityModels.DTO;
namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IBLFile: IBLBase<File,FileDTO>
    {
        ServiceResult UploadFile(FileUpload fileUpload,string webRootPath,string folderPath);
        ServiceResult DeleteFile(FileUpload fileUpload,string webRootPath);
    }
}
