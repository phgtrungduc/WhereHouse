using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTDuc.WhereHouse.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AddressController : Controller
    {
        private string folderPath = "";
        IBLAddress _bLAddress;
        // GET: AddressController
        public AddressController(IWebHostEnvironment env, IBLAddress bLAddress) {
            folderPath = Path.Combine(env.WebRootPath,"Data", "Address");
            _bLAddress = bLAddress;
        }
        [HttpGet("Province")]
        public  IActionResult GetProvinces()
        {
            string filePath = Path.Combine(folderPath, "tinh_tp.json");
            try
            {
                var res = _bLAddress.GetProvinces(filePath);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet("District")]
        public virtual IActionResult GetDistricts()
        {
            string filePath = Path.Combine(folderPath, "quan_huyen.json");
            try
            {
                var res = _bLAddress.GetDistricts(filePath);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Ward")]
        public virtual IActionResult GetWards()
        {
            string filePath = Path.Combine(folderPath, "xa_phuong.json");
            try
            {
                var res = _bLAddress.GetWards(filePath);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetDistrictByParent")]
        public virtual IActionResult GetDistrictsByParent([FromQuery]string parentCode)
        {
            string rootFilePath = Path.Combine(folderPath, "quan-huyen");
            try
            {
                var res = _bLAddress.GetDistrictsByParent(rootFilePath, parentCode);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetWardByParent")]
        public virtual IActionResult GetWardsByParent([FromQuery] string parentCode)
        {
            string rootFilePath = Path.Combine(folderPath, "xa-phuong");
            try
            {
                var res = _bLAddress.GetWardsByParent(rootFilePath, parentCode);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
