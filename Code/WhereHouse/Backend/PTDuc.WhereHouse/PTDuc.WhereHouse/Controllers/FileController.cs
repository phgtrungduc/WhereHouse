using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.EntityModels.Models;
using System.IO;
using PTDuc.WhereHouse.EntityModels.DTO;
namespace PTDuc.WhereHouse.Controllers
{
    [AllowAnonymous]
    public class FileController : BaseEntityController<DBContext.Models.File,FileDTO>
    {
        IBLFile _blFile;
        private readonly IWebHostEnvironment _env;
        public FileController(IBLFile blFile, IWebHostEnvironment env) : base(blFile)
        {
            _blFile = blFile;
            _env = env;
        }
        [HttpPost("UploadFile")]
        public IActionResult UploadFile([FromForm] FileUpload fileUpload)
        {
            var res = new ServiceResult();
            try
            {
                if (fileUpload != null)
                {
                    if (fileUpload.file != null)
                    {
                        var folderPath = fileUpload.category != null ? Path.Combine("uploads", fileUpload.category)
                            : "uploads";
                        res = _blFile.UploadFile(fileUpload, _env.WebRootPath, folderPath);
                        return Ok(res);
                    }
                    else
                    {
                        return BadRequest(res);
                    }
                }
                else
                {
                    return BadRequest(res);
                }
            }
            catch (System.Exception e)
            {
                res.Data = e;
                return BadRequest(res);
            }
        }

        [HttpPost("DeleteFile")]
        public IActionResult DeleteFile(FileUpload fileUpload)
        {
            var res = new ServiceResult();
            try
            {
                if (fileUpload != null)
                {
                    res = _blFile.DeleteFile(fileUpload, _env.WebRootPath);
                    return Ok(res);
                }
                else
                {
                    return BadRequest(res);
                }
            }
            catch (System.Exception e)
            {
                res.Data = e;
                return BadRequest(res);
            }
        }
    }
}
