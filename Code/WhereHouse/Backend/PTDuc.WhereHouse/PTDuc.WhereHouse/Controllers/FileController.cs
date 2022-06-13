using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.EntityModels.Models;
using System.IO;

namespace PTDuc.WhereHouse.Controllers
{
    [AllowAnonymous]
    public class FileController:BaseEntityController<DBContext.Models.File>
    {
        IBLFile _blFile;
        private readonly IWebHostEnvironment _env;
        public FileController(IBLFile blFile, IWebHostEnvironment env) : base(blFile)
        {
            _blFile = blFile;
            _env = env;
        }
        [HttpPost("UploadFile")]
        public IActionResult UploadFile([FromForm]FileUpload fileUpload)
        {
            var res = new ServiceResult();
            try
            {
                if (fileUpload != null)
                {
                    if (fileUpload.file != null)
                    {
                        var folderPath = fileUpload.category != null ? Path.Combine(_env.WebRootPath, "uploads", fileUpload.category)
                            : Path.Combine(_env.WebRootPath, "uploads");
                        res.Data = _blFile.UploadFile(fileUpload, folderPath);
                        return Ok(res);
                    }
                    else {
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

        [HttpDelete("DeleteFile")]
        public IActionResult DeleteFile(FileUpload fileUpload)
        {
            var res = new ServiceResult();
            try
            {
                if (fileUpload != null)
                {
                    var folderPath = fileUpload.category != null ? Path.Combine(_env.WebRootPath, "uploads", fileUpload.category)
                            : Path.Combine(_env.WebRootPath, "uploads");
                    res = _blFile.DeleteFile(fileUpload, folderPath);
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
