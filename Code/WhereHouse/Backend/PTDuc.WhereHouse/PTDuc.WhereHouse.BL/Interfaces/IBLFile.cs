using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.EntityModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IBLFile: IBLBase<File>
    {
        bool UploadFile(FileUpload fileUpload,string folderPath);
        ServiceResult DeleteFile(FileUpload fileUpload,string folderPath);
    }
}
